// Title: Serialize Swiss QR Code data to XML
// Description: Demonstrates creating a Swiss QR Code payment payload, exporting its configuration to XML for archival storage, and optionally generating a PNG image for visual verification.
// Category-Description: This example belongs to the Aspose.BarCode suite focusing on Swiss QR Code generation and serialization. It showcases the use of SwissQRCodetext to build payment information, BarcodeGenerator for QR encoding, and ExportToXml for persisting configuration. Developers working with payment QR codes often need to archive the exact data used for generation, validate visual output, or integrate with downstream systems; this pattern provides a reusable reference for such scenarios.
// Prompt: Serialize the SwissQRCodetext object to XML for archival storage of payment information.
// Tags: swissqr, barcode, xml, serialization, payment, aspose.barcode, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates Swiss QR Code payment data, exports its configuration to XML,
/// and generates a PNG image for verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds Swiss QR Code data, exports to XML, and saves an image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo");
        Directory.CreateDirectory(outputDir);

        // Create Swiss QR Code data object and populate required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor address details
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address details
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Retrieve the plain QR code text representation
        string plainCodeText = swissQr.GetConstructedCodetext();

        // Initialize barcode generator for QR code using the constructed text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            // Optional: adjust generation parameters (e.g., module size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generator's state, which includes Swiss QR data, to an XML file
            string xmlPath = Path.Combine(outputDir, "SwissQRConfig.xml");
            generator.ExportToXml(xmlPath);

            // Optionally generate a PNG image for visual verification
            string imagePath = Path.Combine(outputDir, "SwissQR.png");
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the files have been saved
        Console.WriteLine("Swiss QR Code XML configuration saved.");
        Console.WriteLine("Files are located in: " + outputDir);
    }
}