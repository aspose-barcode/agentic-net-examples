// Title: Center-aligned Code128 barcode with auto-scaling for receipt printing
// Description: Demonstrates how to generate a Code128 barcode, center the human‑readable text, enable automatic scaling, and produce a narrow‑bar PNG image suitable for receipt printers.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to configure BarcodeGenerator parameters such as size, resolution, X‑dimension, and text alignment. Typical use cases include creating barcodes for point‑of‑sale receipts, tickets, and labels where narrow bars and precise layout are required. Developers often need to adjust auto‑size modes and alignment to fit limited print areas while maintaining readability.
// Prompt: Align barcode text to center, enable automatic scaling, and generate image suitable for narrow receipt printing.
// Tags: code128, barcode generation, receipt printing, png, autoscaling, text alignment, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a centered Code128 barcode with automatic scaling for narrow receipt printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeReceipt");
        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, "receipt.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable automatic scaling to fit the specified image size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;
            generator.Parameters.Resolution = 300f;

            // Set narrow bar width suitable for receipt printers
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Center align the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outPath}");
    }
}