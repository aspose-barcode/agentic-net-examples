// Title: Generate Swiss QR Code and Save as PNG with Custom Size and Margins
// Description: Demonstrates creating a Swiss QR Code (QR‑bill) using Aspose.BarCode, customizing its dimensions and margins, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the ComplexBarcodeGenerator with SwissQRCodetext to produce QR‑bill images. Typical use cases include generating payment QR codes for Swiss banking, customizing image size, padding, and exporting to common formats such as PNG. Developers often need to adjust X‑dimension, margins, and fixed image dimensions when integrating QR‑bill generation into invoicing systems.
// Prompt: Save the generated Swiss QR Code to a PNG file with custom dimensions and margins.
// Tags: swissqr, barcode generation, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Swiss QR Code (QR‑bill), applies custom size and margin settings,
/// and saves the result as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR‑bill, configures image parameters,
    /// and writes the PNG file to the output folder.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Environment.CurrentDirectory, "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create Swiss QR Code data
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor address
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Define output file path
        string outputPath = Path.Combine(outputDir, "SwissQR.png");

        // Generate barcode with custom dimensions and margins
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set module (X) size in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure custom padding (margins) in points
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Use nearest auto‑size mode and fix image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 500f;
            generator.Parameters.ImageHeight.Pixels = 500f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}