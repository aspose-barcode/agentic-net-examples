using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Simple IBAN validation according to ISO 13616 (mod‑97)
    static bool ValidateIban(string iban)
    {
        if (string.IsNullOrWhiteSpace(iban))
            return false;

        string trimmed = iban.Replace(" ", "").ToUpperInvariant();
        if (trimmed.Length < 15 || trimmed.Length > 34)
            return false;

        // Move first four characters to the end
        string rearranged = trimmed.Substring(4) + trimmed.Substring(0, 4);

        // Convert letters to numbers (A=10, B=11, ..., Z=35)
        System.Numerics.BigInteger total = 0;
        foreach (char ch in rearranged)
        {
            int value;
            if (ch >= '0' && ch <= '9')
                value = ch - '0';
            else if (ch >= 'A' && ch <= 'Z')
                value = ch - 'A' + 10;
            else
                return false; // invalid character

            total = total * 10 + value;
        }

        // Valid IBAN if remainder is 1
        return (total % 97) == 1;
    }

    // Simple BIC validation (SWIFT) – length 8 or 11, letters/digits
    static bool ValidateBic(string bic)
    {
        if (string.IsNullOrWhiteSpace(bic))
            return false;

        string trimmed = bic.Trim().ToUpperInvariant();
        if (trimmed.Length != 8 && trimmed.Length != 11)
            return false;

        foreach (char ch in trimmed)
        {
            if (!((ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9')))
                return false;
        }

        return true;
    }

    // Custom ISO 20022 payment data validation
    // Expected format: "ISO20022|IBAN={iban}|BIC={bic}|Amount={amount}"
    static bool ValidateIso20022(string codeText, out string message)
    {
        message = "Valid";
        if (string.IsNullOrWhiteSpace(codeText))
        {
            message = "Empty code text.";
            return false;
        }

        string[] parts = codeText.Split('|');
        if (parts.Length < 4 || !parts[0].Equals("ISO20022", StringComparison.OrdinalIgnoreCase))
        {
            message = "Header missing or insufficient parts.";
            return false;
        }

        string iban = null, bic = null, amountStr = null;
        foreach (string part in parts)
        {
            if (part.StartsWith("IBAN=", StringComparison.OrdinalIgnoreCase))
                iban = part.Substring(5);
            else if (part.StartsWith("BIC=", StringComparison.OrdinalIgnoreCase))
                bic = part.Substring(4);
            else if (part.StartsWith("Amount=", StringComparison.OrdinalIgnoreCase))
                amountStr = part.Substring(7);
        }

        if (string.IsNullOrEmpty(iban) || !ValidateIban(iban))
        {
            message = "Invalid or missing IBAN.";
            return false;
        }

        if (string.IsNullOrEmpty(bic) || !ValidateBic(bic))
        {
            message = "Invalid or missing BIC.";
            return false;
        }

        if (string.IsNullOrEmpty(amountStr) || !decimal.TryParse(amountStr, out decimal amount) || amount <= 0)
        {
            message = "Invalid or missing amount.";
            return false;
        }

        return true;
    }

    static void Main()
    {
        // Sample ISO 20022 payment data
        string paymentData = "ISO20022|IBAN=GB82WEST12345698765432|BIC=DEUTDEFF|Amount=1234.56";

        // Generate a QR code containing the payment data
        string tempImagePath = Path.Combine(Path.GetTempPath(), "payment_qr.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, paymentData))
        {
            generator.Save(tempImagePath);
        }

        // Verify the image file exists
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode using all supported types and enable checksum validation
        using (BarCodeReader reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                if (ValidateIso20022(result.CodeText, out string validationMessage))
                {
                    Console.WriteLine("ISO 20022 validation: SUCCESS");
                }
                else
                {
                    Console.WriteLine($"ISO 20022 validation: FAILED – {validationMessage}");
                }
            }

            if (!anyFound)
                Console.WriteLine("No barcode detected in the image.");
        }

        // Clean up temporary file
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Ignored – best‑effort cleanup
        }
    }
}