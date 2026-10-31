// Title: Deserialize Swiss QR Code XML configuration and regenerate QR barcode
// Description: Demonstrates how to export a Swiss QR Code barcode configuration to XML, import it back, decode the codetext, and regenerate the QR image.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization/deserialization category. It showcases the use of BarcodeGenerator, ComplexCodetextReader, and ComplexBarcodeGenerator to persist barcode settings, reconstruct Swiss QR Code objects, and produce QR code images. Developers working with QR codes, especially Swiss QR bills, often need to store and restore barcode configurations for later processing or auditing.
// Prompt: Deserialize XML of SwissQRCodetext back into an object to regenerate the QR code barcode.
// Tags: swissqr, xml, deserialization, barcode, qrcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR Code, exports its configuration to XML,
/// imports the configuration, decodes the Swiss QR codetext, and regenerates the QR image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full workflow from generation to regeneration of a Swiss QR Code.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder to store intermediate files
        string workDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the original QR image, XML configuration, and regenerated image
        string qrImagePath = Path.Combine(workDir, "SwissQR.png");
        string xmlPath = Path.Combine(workDir, "SwissQRConfig.xml");
        string finalImagePath = Path.Combine(workDir, "SwissQR_FromXml.png");

        // Build Swiss QR Code data using the SwissQRCodetext model
        var swissQRCode = new SwissQRCodetext();
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

        // Get the plain codetext string that represents the Swiss QR data
        string plainText = swissQRCode.GetConstructedCodetext();

        // Generate the initial QR image and export the generator configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // Import the generator configuration from the previously saved XML file
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import generator configuration from XML.");
            return;
        }

        // Decode the Swiss QR codetext from the imported generator's CodeText property
        SwissQRCodetext decodedSwiss = ComplexCodetextReader.TryDecodeSwissQR(importedGenerator.CodeText);
        if (decodedSwiss == null)
        {
            Console.WriteLine("Failed to decode Swiss QR codetext from imported configuration.");
            return;
        }

        // Regenerate the QR barcode using the ComplexBarcodeGenerator with the decoded Swiss QR object
        using (var complexGen = new ComplexBarcodeGenerator(decodedSwiss))
        {
            complexGen.Parameters.Barcode.XDimension.Pixels = 4f;
            complexGen.Save(finalImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine("Initial QR image saved to: " + qrImagePath);
        Console.WriteLine("XML configuration saved to: " + xmlPath);
        Console.WriteLine("Regenerated QR image saved to: " + finalImagePath);
    }
}