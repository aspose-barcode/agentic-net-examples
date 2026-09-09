// Title: Serialize Swiss QR Code generation state to XML
// Description: Demonstrates creating a Swiss QR Code, saving it as a PNG image, and exporting the generator's state to an XML file for archival storage of payment information.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode symbologies such as Swiss QR Codes. It showcases the use of BarcodeGenerator, SwissQRCodetext, and related classes to produce QR codes for financial transactions, a common requirement for developers implementing payment standards. The example also illustrates how to persist the generation parameters via XML, enabling later reconstruction or auditing of barcode data.
// Prompt: Serialize the SwissQRCodetext object to XML for archival storage of payment information.
// Tags: barcode, swissqr, xml, serialization, generation, aspose.barcode, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Swiss QR Code, saves it as an image,
/// and exports the barcode generation state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR Code, writes the PNG image,
    /// and serializes the generation state to XML.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output directory for the generated files.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------
        // Build the Swiss QR Code data (payment information) using the
        // SwissQRCodetext object and its nested Bill and Address objects.
        // --------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "12",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Acme Corp",
            Street = "Business Ave",
            HouseNo = "5",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // --------------------------------------------------------------
        // Retrieve the plain text representation of the QR code.
        // --------------------------------------------------------------
        string plainText = swissQr.GetConstructedCodetext();

        // --------------------------------------------------------------
        // Define file paths for the PNG image and the XML export.
        // --------------------------------------------------------------
        string pngPath = Path.Combine(outputDir, "SwissQR.png");
        string xmlPath = Path.Combine(outputDir, "SwissQR.xml");

        // --------------------------------------------------------------
        // Generate the barcode image and export the generator's state to XML.
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainText))
        {
            // Set the module size (pixel dimension) for the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the QR code as a PNG image.
            generator.Save(pngPath, BarCodeImageFormat.Png);

            // Export the complete generation state to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------
        // Output the locations of the generated files.
        // --------------------------------------------------------------
        Console.WriteLine("Swiss QR Code image saved to: " + pngPath);
        Console.WriteLine("Generation state exported to XML: " + xmlPath);
    }
}