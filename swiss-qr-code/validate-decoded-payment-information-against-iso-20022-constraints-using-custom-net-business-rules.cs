// Title: Validate Swiss QR Bill payment data against ISO 20022 rules
// Description: Generates a Swiss QR‑Bill QR code, decodes it, parses the payment fields and validates them according to ISO 20022 constraints.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for QR codes, focusing on the Swiss QR‑Bill format. It shows how to create a QR code with EncodeTypes.QR, read it with BarCodeReader, and apply custom .NET business rules to validate payment information such as IBAN, amount, currency, and reference. Developers working with financial QR codes, ISO 20022 compliance, or barcode‑based payment workflows will find this pattern useful.
// Prompt: Validate decoded payment information against ISO 20022 constraints using custom .NET business rules.
// Tags: qr, swiss-qr-bill, iso20022, validation, barcode, generation, recognition, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation, decoding, parsing, and validation of a Swiss QR‑Bill QR code
/// using Aspose.BarCode. The validation follows selected ISO 20022 constraints.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, decodes it, parses the data,
    /// validates the payment information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder to store the generated QR code image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "SwissQRBill.png");

        // --------------------------------------------------------------------
        // Sample Swiss QR Bill data encoded as a semi‑colon delimited string.
        // --------------------------------------------------------------------
        string qrText = "Account:CH4431999123000889012;" +
                        "Amount:1000.25;" +
                        "Currency:CHF;" +
                        "Reference:210000000003139471430009017;" +
                        "CreditorName:Muster & Söhne;" +
                        "DebtorName:Muster AG";

        // --------------------------------------------------------------------
        // Generate QR code image using Aspose.BarCode.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read and decode the QR code image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Parse the decoded text into a strongly‑typed DTO.
                SwissBillInfo decoded = ParseSwissBillInfo(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to parse Swiss QR Code text.");
                    continue;
                }

                Console.WriteLine("=== Validation Results ===");
                // Apply custom validation rules.
                ValidatePaymentInfo(decoded);
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional).
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }

    /// <summary>
    /// Parses a semi‑colon delimited string into a <see cref="SwissBillInfo"/> instance.
    /// Returns null if mandatory fields are missing.
    /// </summary>
    /// <param name="text">The raw QR code text.</param>
    /// <returns>Parsed payment information or null.</returns>
    static SwissBillInfo ParseSwissBillInfo(string text)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        var info = new SwissBillInfo();
        string[] parts = text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            string[] kv = part.Split(new[] { ':' }, 2);
            if (kv.Length != 2)
                continue;

            string key = kv[0].Trim();
            string value = kv[1].Trim();

            switch (key)
            {
                case "Account":
                    info.Account = value;
                    break;
                case "Amount":
                    if (decimal.TryParse(value, out decimal amt))
                        info.Amount = amt;
                    break;
                case "Currency":
                    info.Currency = value;
                    break;
                case "Reference":
                    info.Reference = value;
                    break;
                case "CreditorName":
                    info.CreditorName = value;
                    break;
                case "DebtorName":
                    info.DebtorName = value;
                    break;
            }
        }

        // Ensure at least the mandatory fields were parsed.
        return string.IsNullOrEmpty(info.Account) ? null : info;
    }

    /// <summary>
    /// Validates the parsed payment information against a subset of ISO 20022 rules.
    /// Writes individual validation results and an overall status to the console.
    /// </summary>
    /// <param name="data">The payment data to validate.</param>
    static void ValidatePaymentInfo(SwissBillInfo data)
    {
        // Account (IBAN) validation: must start with CH and be 21 characters.
        bool accountValid = data.Account != null &&
                            data.Account.StartsWith("CH") &&
                            data.Account.Length == 21;
        Console.WriteLine($"Account valid: {accountValid}");

        // Amount must be positive.
        bool amountValid = data.Amount > 0;
        Console.WriteLine($"Amount valid (>0): {amountValid}");

        // Currency must be CHF (case‑insensitive).
        bool currencyValid = string.Equals(data.Currency, "CHF", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine($"Currency valid (CHF): {currencyValid}");

        // Reference must be non‑empty and ≤ 27 characters (ISO 20022 limit).
        bool referenceValid = !string.IsNullOrEmpty(data.Reference) && data.Reference.Length <= 27;
        Console.WriteLine($"Reference valid (non‑empty, ≤27 chars): {referenceValid}");

        // Creditor name must be non‑empty.
        bool creditorValid = !string.IsNullOrEmpty(data.CreditorName);
        Console.WriteLine($"Creditor name valid: {creditorValid}");

        // Debtor name must be non‑empty.
        bool debtorValid = !string.IsNullOrEmpty(data.DebtorName);
        Console.WriteLine($"Debtor name valid: {debtorValid}");

        // Overall validation result.
        bool overall = accountValid && amountValid && currencyValid && referenceValid && creditorValid && debtorValid;
        Console.WriteLine($"Overall payment information valid: {overall}");
    }
}

/// <summary>
/// Simple DTO to hold parsed Swiss QR Bill information.
/// </summary>
class SwissBillInfo
{
    public string Account { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Reference { get; set; }
    public string CreditorName { get; set; }
    public string DebtorName { get; set; }
}