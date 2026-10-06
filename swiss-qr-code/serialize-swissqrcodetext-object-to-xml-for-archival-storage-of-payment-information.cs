// Title: Serialize Swiss QR Code Data to XML and Generate PNG
// Description: Demonstrates creating a Swiss QR Code payment object, exporting its data to XML for archival storage, and optionally saving a PNG image for visual verification.
// Category-Description: This example belongs to the Aspose.BarCode suite of barcode generation and serialization operations. It showcases the use of SwissQRCodetext, BarcodeGenerator, and ExportToXml to handle Swiss QR payment data. Developers working with financial QR codes often need to archive payment details in a structured format while also providing a visual representation for users.
// Prompt: Serialize the SwissQRCodetext object to XML for archival storage of payment information.
// Tags: swissqr, barcode, serialization, xml, png, aspose.barcode, payment, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Swiss QR Code payment object, exports its data to XML,
/// and generates a PNG image of the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated files.
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo");
        Directory.CreateDirectory(outputDir);

        // Instantiate a Swiss QR Code text object and populate it with payment details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor (payee) address information.
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "12",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };

        // Set debtor (payer) address information.
        swissQr.Bill.Debtor = new Address
        {
            Name = "Acme Corp",
            Street = "Business Ave",
            HouseNo = "5",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Retrieve the plain QR code text that will be encoded.
        string plainCodeText = swissQr.GetConstructedCodetext();

        // Create a BarcodeGenerator for QR symbology using the plain text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, plainCodeText))
        {
            // Optional: adjust the module size (pixel dimension) of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generation state, which includes the Swiss QR data, to an XML file.
            string xmlPath = Path.Combine(outputDir, "SwissQR.xml");
            generator.ExportToXml(xmlPath);

            // Optionally generate a PNG image for visual verification.
            string pngPath = Path.Combine(outputDir, "SwissQR.png");
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the files have been saved.
        Console.WriteLine($"Swiss QR code XML saved to: {Path.Combine(outputDir, "SwissQR.xml")}");
        Console.WriteLine($"Swiss QR code image saved to: {Path.Combine(outputDir, "SwissQR.png")}");
    }
}