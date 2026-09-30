// Title: Generate Code128 Barcode with Interpolation Mode at 150 DPI
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode with interpolation auto‑size mode at 150 dpi, then reads the saved PNG to report its pixel dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure resolution and AutoSizeMode (Interpolation) for high‑quality output. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for developers needing precise control over barcode image size and DPI in automated reporting or printing pipelines.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: barcode, code128, generation, png, interpolation, resolution, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with interpolation auto‑size mode at a specific DPI
/// and reports the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Calls the method that creates and tests the barcode.
    /// </summary>
    static void Main()
    {
        // Generate a barcode with Interpolation mode at 150 dpi and report its pixel size.
        GenerateAndTestBarcode();
    }

    /// <summary>
    /// Creates a Code128 barcode, saves it as PNG using a fixed canvas and 150 dpi resolution,
    /// then loads the image to display its actual pixel dimensions.
    /// </summary>
    static void GenerateAndTestBarcode()
    {
        // Prepare output path in the system's temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode_interpolation.png");

        // Create a BarcodeGenerator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set the image resolution to 150 dpi.
            generator.Parameters.Resolution = 150f;

            // Use Interpolation auto‑size mode with a fixed canvas.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a fixed canvas size (points). These values are arbitrary for the demo.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image to a file in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to obtain its actual pixel dimensions.
        using (var bitmap = new Bitmap(outputPath))
        {
            Console.WriteLine($"Barcode saved to: {outputPath}");
            Console.WriteLine($"Image dimensions (pixels): Width = {bitmap.Width}, Height = {bitmap.Height}");
            Console.WriteLine($"Resolution used: 150 dpi (set via Parameters.Resolution)");
            Console.WriteLine("If the barcode appears distorted, consider increasing the DPI.");
        }
    }
}