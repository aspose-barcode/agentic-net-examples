// Title: Swiss QR Code generation, XML export, and regeneration from deserialized data
// Description: Demonstrates creating a Swiss QR code, exporting its configuration to XML, then deserializing the XML to reconstruct the QR code.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations, showcasing how to work with Swiss QR (SwissQR) codetext using the ComplexBarcode API. It covers generating a QR code, exporting generator settings to XML, decoding the codetext back into a SwissQRCodetext object, and regenerating the barcode. Developers needing to persist and later restore complex barcode configurations will find this pattern useful.
// Prompt: Deserialize XML of SwissQRCodetext back into an object to regenerate the QR code barcode.
// Tags: swissqr, qr, barcode, xml, deserialization, complexbarcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Swiss QR code, exporting its configuration to XML,
/// deserializing the XML back into a SwissQRCodetext object, and regenerating the QR code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs QR code generation, XML export, import, and regeneration.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for XML configuration and PNG images
        string xmlPath = Path.Combine(tempDir, "SwissQR.xml");
        string firstImagePath = Path.Combine(tempDir, "SwissQR_original.png");
        string restoredImagePath = Path.Combine(tempDir, "SwissQR_restored.png");

        // ------------------------------------------------------------
        // 1. Create SwissQRCodetext, generate the initial QR code, and export configuration to XML
        // ------------------------------------------------------------
        var swissCodetext = new SwissQRCodetext();
        swissCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissCodetext.Bill.Account = "CH4431999123000889012";
        swissCodetext.Bill.Amount = 1000.25m;
        swissCodetext.Bill.Currency = "CHF";
        swissCodetext.Bill.Reference = "210000000003139471430009017";
        swissCodetext.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissCodetext.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Build the plain text representation required for QR generation
        string plainCodeText = swissCodetext.GetConstructedCodetext();

        // Generate QR code image and export generator settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(firstImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        Console.WriteLine($"Initial QR code saved to: {firstImagePath}");
        Console.WriteLine($"Configuration exported to XML: {xmlPath}");

        // ------------------------------------------------------------
        // 2. Import generator configuration from XML and decode the Swiss QR codetext
        // ------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Decode the plain code text back into a SwissQRCodetext object
            SwissQRCodetext decodedCodetext = ComplexCodetextReader.TryDecodeSwissQR(importedGenerator.CodeText);
            if (decodedCodetext == null)
            {
                Console.WriteLine("Failed to decode Swiss QR code text from imported configuration.");
                return;
            }

            // ------------------------------------------------------------
            // 3. Regenerate QR code from the decoded SwissQRCodetext object
            // ------------------------------------------------------------
            using (var complexGenerator = new ComplexBarcodeGenerator(decodedCodetext))
            {
                complexGenerator.Save(restoredImagePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Restored QR code saved to: {restoredImagePath}");
        }

        // Cleanup: optional removal of temporary files (comment out if inspection needed)
        // Directory.Delete(tempDir, true);
    }
}