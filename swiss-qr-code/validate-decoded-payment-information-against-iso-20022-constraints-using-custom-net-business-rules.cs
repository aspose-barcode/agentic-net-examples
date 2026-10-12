// Title: Generate and Validate Swiss QR Code with ISO 20022 Business Rules
// Description: Demonstrates creating a Swiss QR Code (QR‑Bill) image, decoding it, and applying custom .NET validation that mirrors ISO 20022 constraints.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcodes such as Swiss QR Codes. It showcases the ComplexBarcodeGenerator, BarCodeReader, and related classes for creating, saving, and reading QR‑Bill data. Developers often need to generate payment QR codes, extract their payload, and enforce business‑level validation rules, making this a typical use case for financial and invoicing applications.
// Prompt: Validate decoded payment information against ISO 20022 constraints using custom .NET business rules.
// Tags: swissqr, qr-bill, barcode generation, barcode recognition, iso20022, validation, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation, decoding, and validation of a Swiss QR Code (QR‑Bill) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR‑Bill image, reads it back, and validates the extracted data.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "SwissQR.png");

        // Build Swiss QR Code payload with required bill information
        SwissQRCodetext swissCode = new SwissQRCodetext();
        swissCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissCode.Bill.Account = "CH4431999123000889012";
        swissCode.Bill.Amount = 1234.56m;
        swissCode.Bill.Currency = "CHF";
        swissCode.Bill.Reference = "210000000003139471430009017";
        swissCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the QR‑Bill image using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;   // Enable ECI for UTF‑8
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(imagePath, BarCodeImageFormat.Png);                // Save as PNG
        }

        // Read the generated QR code and perform validation
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            foreach (BarCodeResult result in results)
            {
                // Decode the Swiss QR Code text into a strongly‑typed object
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to parse Swiss QR Code text.");
                    continue;
                }

                // Apply custom ISO 20022‑like validation rules
                bool isValid = ValidateSwissQR(decoded, out string validationMessage);
                Console.WriteLine(isValid ? "Validation succeeded." : "Validation failed.");
                Console.WriteLine(validationMessage);
            }
        }
    }

    /// <summary>
    /// Validates the decoded Swiss QR Code data against a set of simple ISO 20022‑style business rules.
    /// </summary>
    /// <param name="data">The decoded Swiss QR Code payload.</param>
    /// <param name="message">Output message describing validation result.</param>
    /// <returns>True if all checks pass; otherwise false.</returns>
    static bool ValidateSwissQR(SwissQRCodetext data, out string message)
    {
        // Ensure the QR‑Bill version is supported
        if (data.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
        {
            message = "Unsupported QR bill version.";
            return false;
        }

        // Validate IBAN format for Swiss accounts
        if (string.IsNullOrWhiteSpace(data.Bill.Account) || !IsValidIban(data.Bill.Account))
        {
            message = "Invalid IBAN account.";
            return false;
        }

        // Amount must be positive
        if (data.Bill.Amount <= 0)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

        // Currency must be Swiss Francs
        if (data.Bill.Currency != "CHF")
        {
            message = "Currency must be CHF.";
            return false;
        }

        // Creditor information must be present
        if (data.Bill.Creditor == null || string.IsNullOrWhiteSpace(data.Bill.Creditor.Name))
        {
            message = "Creditor information missing.";
            return false;
        }

        // Debtor information must be present
        if (data.Bill.Debtor == null || string.IsNullOrWhiteSpace(data.Bill.Debtor.Name))
        {
            message = "Debtor information missing.";
            return false;
        }

        message = "All checks passed.";
        return true;
    }

    /// <summary>
    /// Performs a minimal validation of a Swiss IBAN (CH + 21 alphanumeric characters).
    /// </summary>
    /// <param name="iban">IBAN string to validate.</param>
    /// <returns>True if the IBAN meets basic format requirements; otherwise false.</returns>
    static bool IsValidIban(string iban)
    {
        // Very basic IBAN validation for Swiss accounts (CH + 21 characters, alphanumeric)
        if (iban.Length != 21) return false;
        if (!iban.StartsWith("CH")) return false;
        foreach (char c in iban)
        {
            if (!char.IsLetterOrDigit(c)) return false;
        }
        return true;
    }
}