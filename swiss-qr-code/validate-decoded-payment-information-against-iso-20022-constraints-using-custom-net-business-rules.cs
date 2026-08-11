// Title: Validate ISO 20022 Payment Data via QR Code Generation and Decoding
// Description: Demonstrates generating a QR code with ISO 20022‑style payment information, decoding it, and applying custom .NET validation rules.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator (EncodeTypes) and BarCodeReader (DecodeType) for QR code creation and reading. Typical use cases include encoding payment details for mobile scanning, validating financial data, and integrating barcode workflows in .NET applications. Developers often need to generate QR codes, extract embedded data, and enforce domain‑specific constraints such as ISO 20022.
// Prompt: Validate decoded payment information against ISO 20022 constraints using custom .NET business rules.
// Tags: barcode symbology, qr, generation, recognition, validation, iso20022, payment, aspose.barcode

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code containing ISO 20022‑like payment data,
/// decodes it, and validates the extracted fields against basic ISO 20022 constraints.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR code, decodes it, validates the data, and cleans up.
    /// </summary>
    static void Main()
    {
        // Sample ISO 20022‑like payment data (key=value pairs)
        string paymentData = "IBAN=DE89370400440532013000;BIC=DEUTDEFF;Amount=1234.56";

        // Path for temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "payment_qr.png");

        // Generate QR code containing the payment data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, paymentData))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode and validate the extracted information
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string decodedText = result.CodeText;
                Console.WriteLine($"Decoded Text: {decodedText}");

                bool isValid = ValidatePaymentInfo(decodedText, out string validationMessage);
                Console.WriteLine(isValid
                    ? "Payment information is valid."
                    : $"Payment information is invalid: {validationMessage}");
            }
        }

        // Clean up temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit
        }
    }

    // Parses the key=value pairs and applies simple ISO 20022 constraints
    static bool ValidatePaymentInfo(string data, out string message)
    {
        // Split into individual fields
        string[] parts = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        string iban = null, bic = null, amountStr = null;

        foreach (string part in parts)
        {
            string[] kv = part.Split(new[] { '=' }, 2);
            if (kv.Length != 2) continue;

            string key = kv[0].Trim().ToUpperInvariant();
            string value = kv[1].Trim();

            switch (key)
            {
                case "IBAN":
                    iban = value;
                    break;
                case "BIC":
                    bic = value;
                    break;
                case "AMOUNT":
                    amountStr = value;
                    break;
            }
        }

        // Validate IBAN (basic structure: 2 letters, 2 digits, up to 30 alphanumerics)
        if (string.IsNullOrEmpty(iban) ||
            !Regex.IsMatch(iban, @"^[A-Z]{2}\d{2}[A-Z0-9]{1,30}$", RegexOptions.IgnoreCase))
        {
            message = "Invalid or missing IBAN.";
            return false;
        }

        // Validate BIC (8 or 11 characters, letters/digits)
        if (string.IsNullOrEmpty(bic) ||
            !(bic.Length == 8 || bic.Length == 11) ||
            !Regex.IsMatch(bic, @"^[A-Z0-9]{8}([A-Z0-9]{3})?$", RegexOptions.IgnoreCase))
        {
            message = "Invalid or missing BIC.";
            return false;
        }

        // Validate Amount (positive decimal number)
        if (string.IsNullOrEmpty(amountStr) ||
            !decimal.TryParse(amountStr, out decimal amount) ||
            amount <= 0)
        {
            message = "Invalid or missing Amount.";
            return false;
        }

        // All checks passed
        message = null;
        return true;
    }
}