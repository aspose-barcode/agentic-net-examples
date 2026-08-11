// Title: Regenerate Swiss QR Code from Exported XML
// Description: This example creates a Swiss QR Code, exports its generator settings to XML, imports the XML, decodes the Swiss QR codetext, and regenerates the barcode.
// Category-Description: Demonstrates Aspose.BarCode's XML serialization and complex barcode generation workflow. It uses BarcodeGenerator for QR encoding, ExportToXml/ImportFromXml for configuration persistence, ComplexCodetextReader to parse Swiss QR codetext, and ComplexBarcodeGenerator to produce the final image. Developers working with QR codes and needing to store or transfer barcode settings will find this pattern useful.
// Prompt: Deserialize XML of SwissQRCodetext back into an object to regenerate the QR code barcode.
// Tags: qr, swissqr, xml, serialization, barcodegenerator, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code, exporting its configuration to XML,
/// importing it back, decoding the codetext, and regenerating the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the full create‑export‑import‑decode‑regenerate cycle.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original image, XML configuration, and regenerated image
        string pngPath = "SwissQR.png";
        string xmlPath = "SwissQR.xml";
        string regeneratedPngPath = "SwissQR_fromXml.png";

        // -------------------------------------------------
        // Step 1: Create a SwissQRCodetext object and set required fields
        // -------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Construct the plain codetext string that represents the Swiss QR data
        string plainCodeText = swissQr.GetConstructedCodetext();

        // -------------------------------------------------
        // Step 2: Generate a QR barcode and export its configuration to XML
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            // Save the initial barcode image (optional, just to have a file)
            generator.Save(pngPath, BarCodeImageFormat.Png);

            // Export generator settings, including the codetext, to an XML file
            generator.ExportToXml(xmlPath);
        }

        // -------------------------------------------------
        // Step 3: Import the generator configuration from XML
        // -------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Retrieve the codetext that was stored in the XML
            string importedCodeText = importedGenerator.CodeText;

            // -------------------------------------------------
            // Step 4: Decode the SwissQR codetext back into a SwissQRCodetext object
            // -------------------------------------------------
            SwissQRCodetext decodedSwissQr = ComplexCodetextReader.TryDecodeSwissQR(importedCodeText);
            if (decodedSwissQr == null)
            {
                Console.WriteLine("Failed to decode SwissQR codetext from imported XML.");
                return;
            }

            // -------------------------------------------------
            // Step 5: Regenerate the SwissQR barcode using ComplexBarcodeGenerator
            // -------------------------------------------------
            using (var complexGenerator = new ComplexBarcodeGenerator(decodedSwissQr))
            {
                // Save the regenerated barcode image to a new file
                complexGenerator.Save(regeneratedPngPath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("SwissQR barcode regenerated successfully.");
    }
}