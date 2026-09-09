// Title: Generate Swiss QR Code with Transparent Background at 300 DPI
// Description: Demonstrates creating a Swiss QR bill barcode using Aspose.BarCode, configuring a 300 DPI resolution and a transparent background, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext to produce high‑resolution, custom‑styled barcodes. Developers often need to generate payment QR codes, embed them in documents, or render them with specific visual requirements such as transparency or DPI settings. The snippet illustrates typical steps: preparing data, configuring generator parameters, and exporting to common image formats.
// Prompt: Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent background.
// Tags: swissqr, barcode generation, high resolution, transparent background, png, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR bill barcode with high resolution and transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, builds Swiss QR data, configures generator, and saves PNG.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build Swiss QR code data (bill information)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Define the output file path
        string outputPath = Path.Combine(outputDir, "SwissQR_Transparent_300dpi.png");

        // Generate the barcode with desired resolution and transparent background
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Resolution = 300f;               // Set DPI to 300
            generator.Parameters.BackColor = Color.Transparent; // Make background transparent
            generator.Save(outputPath, BarCodeImageFormat.Png);   // Save as PNG
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}