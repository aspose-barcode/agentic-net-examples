// Title: Generate Code128 barcode with fixed width and automatic height
// Description: Demonstrates creating a Code128 barcode image with a width of 40 mm while letting the library automatically calculate the height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image dimensions using the BarcodeGenerator.Parameters.ImageWidth and ImageHeight properties. Developers often need to produce barcodes that fit specific layout constraints, such as a fixed width for printing on labels, while allowing the height to adapt automatically. The key API classes shown are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which are commonly used for barcode creation, format selection, and image export.
// Prompt: Write code generating barcode with BarCodeHeight zero, BarCodeWidth 40 mm, and verify auto‑height behavior.
// Tags: barcode, code128, image generation, width, auto height, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a fixed width of 40 mm and automatic height calculation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates the barcode, saves it as PNG, and outputs image dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "barcode.png");

        // Initialize barcode generator (Code128) with sample text
        var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE");

        // Set image width to 40 millimeters; height will be auto‑sized (BarCodeHeight = 0)
        generator.Parameters.ImageWidth.Millimeters = 40f;

        // Optionally set a higher resolution for more precise size calculations
        generator.Parameters.Resolution = 300f;

        // Save barcode image as PNG
        generator.Save(imagePath, BarCodeImageFormat.Png);

        // Load the saved image to verify dimensions
        using (var bitmap = new Bitmap(imagePath))
        {
            Console.WriteLine($"Barcode image saved to: {imagePath}");
            Console.WriteLine($"Image width (pixels): {bitmap.Width}");
            Console.WriteLine($"Image height (pixels): {bitmap.Height}");

            // Calculate height in millimeters based on resolution
            float heightMm = bitmap.Height * 25.4f / generator.Parameters.Resolution;
            Console.WriteLine($"Calculated image height (mm): {heightMm:F2}");
        }
    }
}