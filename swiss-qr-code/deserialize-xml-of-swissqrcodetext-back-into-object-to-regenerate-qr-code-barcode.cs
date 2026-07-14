// Title: Deserialize SwissQR XML to Regenerate QR Code
// Description: Demonstrates how to serialize a SwissQR barcode configuration to XML, deserialize it back, and regenerate the QR code image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of BarcodeGenerator, ComplexBarcodeGenerator, and SwissQRCodetext classes for QR code creation, XML export/import, and object reconstruction. Developers working with SwissQR (QR-bill) payments often need to persist barcode settings, transfer them between systems, or recreate barcodes from stored data.
// Prompt: Deserialize XML of SwissQRCodetext back into an object to regenerate the QR code barcode.
// Tags: qr code, swissqr, xml serialization, barcode generation, complex barcode, aspnet bar code

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a SwissQR barcode, exports its configuration to XML,
/// imports the configuration back, decodes it into a SwissQRCodetext object,
/// and finally regenerates the QR code image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full round‑trip of SwissQR barcode creation,
    /// XML persistence, and regeneration.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Build a SwissQR codetext object with required payment data
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // 2. Generate the plain QR code text from the object
        // ------------------------------------------------------------
        string plainCodeText = swissQr.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Create a QR barcode generator and export its configuration to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            string xmlPath = "SwissQR.xml";
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // 4. Import the generator configuration from the previously saved XML
        // ------------------------------------------------------------
        if (!File.Exists("SwissQR.xml"))
        {
            Console.WriteLine("XML file not found.");
            return;
        }

        var importedGenerator = BarcodeGenerator.ImportFromXml("SwissQR.xml");
        string importedCodeText = importedGenerator.CodeText;

        // ------------------------------------------------------------
        // 5. Decode the plain codetext back into a SwissQRCodetext object
        // ------------------------------------------------------------
        SwissQRCodetext decodedSwissQr = ComplexCodetextReader.TryDecodeSwissQR(importedCodeText);
        if (decodedSwissQr == null)
        {
            Console.WriteLine("Failed to decode SwissQR codetext.");
            return;
        }

        // ------------------------------------------------------------
        // 6. Regenerate the QR barcode image from the decoded object
        // ------------------------------------------------------------
        using (var complexGenerator = new ComplexBarcodeGenerator(decodedSwissQr))
        {
            complexGenerator.Save("SwissQR_fromXml.png");
        }

        Console.WriteLine("QR code regenerated successfully.");
    }
}