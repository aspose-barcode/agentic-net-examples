// Title: Deserialize Swiss QR Code XML and Regenerate Barcode
// Description: This example demonstrates how to export a QR code generator configuration to XML, deserialize it back into a SwissQRCodetext object, and regenerate the Swiss QR barcode.
// Category-Description: Shows Aspose.BarCode complex barcode generation and XML import/export. It uses ComplexBarcodeGenerator, BarcodeGenerator, and ComplexCodetextReader to handle Swiss QR codes, a common use case for payment QR codes. Developers working with QR code serialization, deserialization, and regeneration can reference this pattern for similar scenarios.
// Prompt: Deserialize XML of SwissQRCodetext back into an object to regenerate the QR code barcode.
// Tags: qr, swissqr, xml, serialization, deserialization, barcode generation, aspose.barcode, complexbarcode, barcodegenerator, complexcodetextreader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates exporting a Swiss QR code generator to XML, importing it back,
/// decoding the Swiss QR codetext, and regenerating the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the full cycle of generation, XML export,
    /// import, decoding, and regeneration of a Swiss QR barcode.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the original, regenerated images and the XML configuration
        string qrImagePath = Path.Combine(tempDir, "SwissQR.png");
        string regeneratedImagePath = Path.Combine(tempDir, "SwissQR_fromXml.png");
        string xmlPath = Path.Combine(tempDir, "generator.xml");

        // Create Swiss QR Code data (bill information)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            CountryCode = "CH",
            Street = "Main St",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Jane Smith",
            CountryCode = "CH",
            Street = "Second St",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern"
        };

        // Generate the original Swiss QR barcode using ComplexBarcodeGenerator
        using (var complexGen = new ComplexBarcodeGenerator(swissQr))
        {
            complexGen.Parameters.Barcode.XDimension.Pixels = 4f;
            complexGen.Save(qrImagePath);
        }

        // Get the plain code text and export the generator configuration to XML
        string plainCodeText = swissQr.GetConstructedCodetext();
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            gen.Save(qrImagePath, BarCodeImageFormat.Png);
            gen.ExportToXml(xmlPath);
        }

        // Import the generator from XML, decode the Swiss QR codetext, and regenerate the barcode
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            string importedCodeText = importedGen.CodeText;
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(importedCodeText);
            if (decoded != null)
            {
                using (var regenGen = new ComplexBarcodeGenerator(decoded))
                {
                    regenGen.Save(regeneratedImagePath);
                }
                Console.WriteLine("Regenerated QR code saved to: " + regeneratedImagePath);
            }
            else
            {
                Console.WriteLine("Failed to decode Swiss QR code from imported XML.");
            }
        }

        // Optional clean-up: delete the temporary directory
        // Directory.Delete(tempDir, true);
    }
}