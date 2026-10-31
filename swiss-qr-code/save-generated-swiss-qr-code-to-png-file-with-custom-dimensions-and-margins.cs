// Title: Generate Swiss QR Code with custom dimensions and margins
// Description: Demonstrates creating a Swiss QR Bill barcode, customizing its size and margins, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to control image dimensions, module size, and padding. Developers working with payment QR codes or custom barcode rendering can refer to this pattern for generating high‑resolution images with precise layout control.
// Prompt: Save the generated Swiss QR Code to a PNG file with custom dimensions and margins.
// Tags: swissqr, barcode, generation, png, complexbarcode, parameters, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code with custom dimensions, margins, and saving it as a PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code, configures size and padding, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQRBill.png");

        // Build the Swiss QR Code data structure with required bill information
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode with custom size, module dimension, and margins
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Define the size of a single module (pixel size)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Enable fixed image size mode and set desired width and height
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 800f;
            generator.Parameters.ImageHeight.Pixels = 800f;

            // Apply custom padding (margins) around the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}