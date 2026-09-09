// Title: Generate Swiss QR Code and save as PNG with custom size and margins
// Description: Demonstrates creating a Swiss QR Code (QR-bill) using Aspose.BarCode, customizing its dimensions, margins, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the ComplexBarcodeGenerator and SwissQRCodetext classes for producing QR-bill compliant barcodes. Developers often need to adjust image size, module dimensions, and padding to fit UI or printing requirements.
// Prompt: Save the generated Swiss QR Code to a PNG file with custom dimensions and margins.
// Tags: swiss qr code, generation, png, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Code (QR‑Bill) with custom image size and margins,
/// then saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the QR‑Bill data, configures the barcode generator,
    /// and writes the resulting image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary output directory and define the PNG file path.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQR.png");

        // --------------------------------------------------------------------
        // Build the Swiss QR Code payload (QR‑Bill data).
        // --------------------------------------------------------------------
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Creditor address details.
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Debtor address details.
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // --------------------------------------------------------------------
        // Configure the barcode generator: size, margins, encoding options.
        // --------------------------------------------------------------------
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set module (pixel) size.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use ECI encoding mode with UTF‑8 character set.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Define custom image dimensions (pixels).
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 500f;

            // Ensure the generator respects the fixed dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Apply custom padding (margins) around the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}