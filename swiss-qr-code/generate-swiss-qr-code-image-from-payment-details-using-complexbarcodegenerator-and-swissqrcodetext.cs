// Title: Generate Swiss QR Code for Payment Using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code image containing payment information and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as Swiss QR Code. It showcases the use of ComplexBarcodeGenerator and SwissQRCodetext classes to encode payment details into a QR code, a common requirement for Swiss payment standards. Developers can adapt this pattern for generating other complex barcodes or customizing encoding parameters.
// Prompt: Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and SwissQRCodetext.
// Tags: swiss qr code, generation, png, complexbarcodegenerator, swissqrcodetext, payment

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss QR Code image with payment details using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code and saves it to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        string outputPath = Path.Combine(outputDir, "SwissQR.png");

        // Create Swiss QR Code payment details
        var swissQRCode = new SwissQRCodetext();

        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH9300762011623852957";
        swissQRCode.Bill.Amount = 199.95m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";

        // Set creditor address
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "12A",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };

        // Set debtor address
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Acme Corp",
            Street = "Business Road",
            HouseNo = "34B",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Configure barcode appearance and encoding options
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss QR Code image saved to: {outputPath}");
    }
}