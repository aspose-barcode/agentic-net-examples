// Title: Swiss QR Code Generation, Decoding, and ISO 20022 Validation
// Description: Demonstrates creating a Swiss QR Code, saving it as an image, decoding it, and validating the extracted payment data against ISO 20022 constraints.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcodes such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and SwissQRCodetext classes for creating, reading, and processing QR Code payment information. Developers commonly use these APIs to integrate QR‑based payment flows, ensuring compliance with financial standards.
// Prompt: Validate decoded payment information against ISO 20022 constraints using custom .NET business rules.
// Tags: swissqr, qr, generation, recognition, validation, iso20022, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation, decoding, and validation of Swiss QR Code payment information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR Code, decodes it, and validates the data.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "SwissQR.png");

        // Create sample Swiss QR Code data (payment bill)
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(barcodePath);
        }

        // Read and decode the generated QR Code
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to parse Swiss QR Code text.");
                    continue;
                }

                // Output decoded fields for demonstration purposes
                Console.WriteLine("Decoded Swiss QR Code:");
                Console.WriteLine($"Version: {decoded.Bill.Version}");
                Console.WriteLine($"Account: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
                Console.WriteLine($"Reference: {decoded.Bill.Reference}");
                Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"Debtor Name: {decoded.Bill.Debtor.Name}");

                // Validate the decoded data against ISO 20022 business rules
                ValidateSwissQR(decoded);
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not crash the program
        }
    }

    /// <summary>
    /// Performs custom validation of Swiss QR Code data according to ISO 20022 constraints.
    /// </summary>
    /// <param name="codetext">The decoded Swiss QR Code data.</param>
    static void ValidateSwissQR(SwissQRCodetext codetext)
    {
        bool isValid = true;

        // Validate version (must be V2_0 for this example)
        if (codetext.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
        {
            Console.WriteLine("Invalid version: must be V2_0.");
            isValid = false;
        }

        // Basic IBAN validation (21 characters, starts with "CH")
        string iban = codetext.Bill.Account;
        if (string.IsNullOrWhiteSpace(iban) || iban.Length != 21 || !iban.StartsWith("CH"))
        {
            Console.WriteLine("Invalid IBAN: must be 21 characters and start with 'CH'.");
            isValid = false;
        }

        // Amount must be greater than zero
        if (codetext.Bill.Amount <= 0)
        {
            Console.WriteLine("Invalid amount: must be greater than zero.");
            isValid = false;
        }

        // Currency must be CHF (case‑insensitive)
        if (!string.Equals(codetext.Bill.Currency, "CHF", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Invalid currency: must be 'CHF'.");
            isValid = false;
        }

        // Creditor name is required
        if (string.IsNullOrWhiteSpace(codetext.Bill.Creditor?.Name))
        {
            Console.WriteLine("Invalid creditor: name is required.");
            isValid = false;
        }

        // Reference length must not exceed 25 characters (ISO 20022 limit)
        if (!string.IsNullOrEmpty(codetext.Bill.Reference) && codetext.Bill.Reference.Length > 25)
        {
            Console.WriteLine("Invalid reference: exceeds maximum length of 25 characters.");
            isValid = false;
        }

        Console.WriteLine(isValid ? "All ISO 20022 constraints satisfied." : "One or more ISO 20022 constraints violated.");
    }
}