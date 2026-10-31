// Title: Generate and validate a Swiss QR Code using Aspose.BarCode
// Description: Demonstrates how to create a Swiss QR Code, save it as PNG, decode it, and verify required fields according to the Swiss Implementation Guidelines.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcodes such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and related Swiss QR specific classes (SwissQRCodetext, SwissQRBill). Developers commonly need to generate payment QR codes, ensure compliance with standards, and programmatically validate decoded data.
// Prompt: Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by checking required data fields.
// Tags: swiss qr code, barcode generation, barcode validation, aspose.barcode, complex barcode, payment qr, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation, saving, decoding, and validation of a Swiss QR Code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Swiss QR Code, writes it to a temporary PNG file,
    /// reads it back, decodes the content, and validates required fields.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N") + ".png");

        // Build the Swiss QR Code data structure with required bill information.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor address details.
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address details.
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Initialize a barcode reader to decode the QR code from the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool anyResult = false;

            // Iterate over all detected barcodes (should be only one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                anyResult = true;

                // Attempt to decode the Swiss QR Code specific structure.
                var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Swiss QR Code.");
                    continue;
                }

                // Validate required fields according to the Swiss Implementation Guidelines.
                bool valid = true;

                if (decoded.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
                {
                    Console.WriteLine("Invalid version.");
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(decoded.Bill.Account))
                {
                    Console.WriteLine("Account is missing.");
                    valid = false;
                }

                if (decoded.Bill.Amount <= 0)
                {
                    Console.WriteLine("Amount must be greater than zero.");
                    valid = false;
                }

                if (string.IsNullOrWhiteSpace(decoded.Bill.Currency))
                {
                    Console.WriteLine("Currency is missing.");
                    valid = false;
                }

                if (decoded.Bill.Creditor == null || string.IsNullOrWhiteSpace(decoded.Bill.Creditor.Name))
                {
                    Console.WriteLine("Creditor name is missing.");
                    valid = false;
                }

                if (decoded.Bill.Creditor == null || decoded.Bill.Creditor.CountryCode != "CH")
                {
                    Console.WriteLine("Creditor country code must be 'CH'.");
                    valid = false;
                }

                if (decoded.Bill.Debtor == null || string.IsNullOrWhiteSpace(decoded.Bill.Debtor.Name))
                {
                    Console.WriteLine("Debtor name is missing.");
                    valid = false;
                }

                Console.WriteLine(valid ? "Swiss QR Code validation passed." : "Swiss QR Code validation failed.");
            }

            // If no barcodes were detected, inform the user.
            if (!anyResult)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }
}