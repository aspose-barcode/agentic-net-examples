// Title: Export Swiss QR Code to JPEG with adjustable quality
// Description: Demonstrates generating a Swiss QR Code (QR‑Bill) and saving it as a JPEG image while allowing configurable quality settings for web usage.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related classes to create Swiss QR Codes (QR‑Bills). Typical scenarios include generating payment QR codes for invoices and exporting them in web‑friendly image formats with adjustable resolution and anti‑aliasing.
// Prompt: Export the generated Swiss QR Code as a JPEG image with adjustable quality settings for web usage.
// Tags: swiss qr, qr‑bill, barcode generation, jpeg, quality, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss QR Code (QR‑Bill) and exporting it as a JPEG image with configurable quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output folder, configures image quality, builds the Swiss QR Code data,
    /// generates the barcode, and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Adjustable quality setting (1‑100). Higher values yield better image quality.
        int quality = 80; // Sample value; adjust as needed.

        // Map the quality value to resolution (DPI) and anti‑aliasing settings.
        float resolution;
        bool useAntiAlias;
        if (quality >= 80)
        {
            resolution = 300f;
            useAntiAlias = true;
        }
        else if (quality >= 50)
        {
            resolution = 150f;
            useAntiAlias = true;
        }
        else
        {
            resolution = 72f;
            useAntiAlias = false;
        }

        // Build the Swiss QR Code data (QR‑Bill) with creditor and payment details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";

        // Generate the barcode using the complex barcode generator.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Optional appearance settings.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Apply quality‑related settings.
            generator.Parameters.Resolution = resolution;
            generator.Parameters.UseAntiAlias = useAntiAlias;

            // Save the generated barcode as a JPEG image.
            string outputPath = Path.Combine(outputDir, "SwissQRBill.jpg");
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
        }
    }
}