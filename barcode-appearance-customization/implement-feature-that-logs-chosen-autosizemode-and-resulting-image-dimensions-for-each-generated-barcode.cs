// Title: Demonstrate AutoSizeMode effects on barcode image generation
// Description: Shows how different AutoSizeMode settings affect the size of a generated Code128 barcode and logs the resulting dimensions.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode, autosizemode, code128, image generation, logging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes using different AutoSizeMode settings and logs image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for each AutoSizeMode, saves them, and logs dimensions.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        const string codeText = "1234567890";

        // Output directory for generated images; ensure it exists
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // Define the AutoSizeMode variations to test
        AutoSizeMode[] modes = new AutoSizeMode[]
        {
            AutoSizeMode.None,
            AutoSizeMode.Nearest,
            AutoSizeMode.Interpolation
        };

        // Iterate through each mode, generate a barcode, and log its size
        foreach (AutoSizeMode mode in modes)
        {
            // Create and configure the barcode generator for the current mode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.AutoSizeMode = mode;

                // For modes other than None, specify a target image size (in points)
                if (mode != AutoSizeMode.None)
                {
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                }

                // Build the file path and save the barcode image as PNG
                string filePath = Path.Combine(outputDir, $"{mode}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Load the saved image to obtain its actual dimensions
                using (var image = (Bitmap)Image.FromFile(filePath))
                {
                    // Log the AutoSizeMode used and the resulting image width x height
                    Console.WriteLine($"AutoSizeMode: {mode}, Image Size: {image.Width}x{image.Height}");
                }
            }
        }
    }
}