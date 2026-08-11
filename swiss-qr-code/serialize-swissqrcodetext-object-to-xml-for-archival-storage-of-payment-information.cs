// Title: Serialize SwissQR payment data to XML and regenerate barcode
// Description: Demonstrates creating a Swiss QR payment barcode, exporting its configuration to XML for archival storage, and reloading it to regenerate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and complex barcode handling category. It showcases the use of BarcodeGenerator, ComplexBarcodeGenerator, and SwissQRCodetext classes to create QR codes, serialize generator settings to XML, and deserialize them. Developers working with payment QR codes often need to archive barcode configurations and later reconstruct them without losing data.
// Prompt: Serialize the SwissQRCodetext object to XML for archival storage of payment information.
// Tags: qr code, serialization, xml, generation, complexbarcode, swissqr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Swiss QR payment barcode, exports its configuration to XML,
/// imports the configuration back, and regenerates the barcode from the restored data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode creation, XML export/import, and regeneration steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare SwissQR payment data
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Build the plain codetext string that will be encoded in the QR barcode
        string plainCodeText = swissQr.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Generate a QR barcode and export its configuration to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            // Save the barcode image (optional, for visual verification)
            generator.Save("SwissQR.png");

            // Export generator settings (including the codetext) to an XML file
            bool exported = generator.ExportToXml("SwissQR.xml");
            Console.WriteLine($"Export to XML successful: {exported}");
        }

        // ------------------------------------------------------------
        // Import the generator configuration from XML
        // ------------------------------------------------------------
        if (!File.Exists("SwissQR.xml"))
        {
            Console.WriteLine("XML file not found. Exiting.");
            return;
        }

        using (var importedGenerator = BarcodeGenerator.ImportFromXml("SwissQR.xml"))
        {
            // Decode the SwissQR codetext back into a SwissQRCodetext object
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(importedGenerator.CodeText);
            if (decoded == null)
            {
                Console.WriteLine("Failed to decode SwissQR codetext from imported XML.");
                return;
            }

            // ------------------------------------------------------------
            // Regenerate the barcode from the decoded object
            // ------------------------------------------------------------
            using (var complexGenerator = new ComplexBarcodeGenerator(decoded))
            {
                complexGenerator.Save("SwissQR_fromXml.png");
                Console.WriteLine("Regenerated barcode saved as SwissQR_fromXml.png");
            }

            // Output some of the restored payment information for verification
            Console.WriteLine("Restored payment data:");
            Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
            Console.WriteLine($"Country Code: {decoded.Bill.Creditor.CountryCode}");
            Console.WriteLine($"Account: {decoded.Bill.Account}");
            Console.WriteLine($"Amount: {decoded.Bill.Amount}");
            Console.WriteLine($"Currency: {decoded.Bill.Currency}");
            Console.WriteLine($"Version: {decoded.Bill.Version}");
        }
    }
}