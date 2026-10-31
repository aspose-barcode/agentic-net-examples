// Title: Generate Swiss QR Code (QR Bill) Image with Aspose.BarCode
// Description: Demonstrates how to create a Swiss QR Code (QR Bill) image using Aspose.BarCode's ComplexBarcodeGenerator and SwissQRCodetext.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related classes to produce payment‑specific QR codes. Typical use cases include generating QR Bill images for Swiss financial transactions, where developers need to encode payer/payee details, amount, and reference information into a scannable QR code.
// Prompt: Generate a Swiss QR Code image from payment details using ComplexBarcodeGenerator and SwissQRCodetext.
// Tags: swiss qr code, barcode generation, png, aspose.barcode, complexbarcode, payment, qr bill

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Swiss QR Code (QR Bill) image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR Code image and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the generated image will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create and populate Swiss QR Code data (QR Bill) with payment details.
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
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };

        // Set debtor (payer) address information.
        swissQr.Bill.Debtor = new Address
        {
            Name = "Acme Corp",
            Street = "Second Street",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Define the output file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "SwissQRBill.png");

        // Generate the barcode using ComplexBarcodeGenerator with specific QR settings.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;    // Use ECI encoding mode.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8; // Specify UTF‑8 character set.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Set error correction level.
            generator.Save(outputPath, BarCodeImageFormat.Png);               // Save as PNG.
        }

        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}