// Title: Barcode AutoSizeMode Demonstration with Dimension Logging
// Description: Shows how different AutoSizeMode settings affect the generated DataMatrix barcode image and logs the resulting dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image size parameters. Developers often need to control barcode scaling and retrieve image dimensions for layout or validation purposes. The snippet demonstrates typical API usage for setting AutoSizeMode, adjusting image size, and extracting bitmap dimensions.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode, autosizemode, datamatrix, image, png, dimensions, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating DataMatrix barcodes with various AutoSizeMode settings and logs image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves them, and writes AutoSizeMode and image size to console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Text to encode in the barcode.
        string codeText = "ASPOSE";

        // Define the AutoSizeMode options to test.
        AutoSizeMode[] modes = new AutoSizeMode[]
        {
            AutoSizeMode.None,
            AutoSizeMode.Interpolation,
            AutoSizeMode.Nearest
        };

        // Iterate over each AutoSizeMode, generate a barcode, and log its dimensions.
        foreach (AutoSizeMode mode in modes)
        {
            // Initialize the barcode generator for DataMatrix symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Apply the current AutoSizeMode.
                generator.Parameters.AutoSizeMode = mode;

                // For modes other than None, set a target image size.
                if (mode != AutoSizeMode.None)
                {
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 300f;
                }

                // Set the X-dimension (module size) of the barcode.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Save the generated barcode image to a PNG file.
                string filePath = Path.Combine(tempDir, $"barcode_{mode}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Generate the barcode image in memory to retrieve its dimensions.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;
                    Console.WriteLine($"AutoSizeMode: {mode}, Image dimensions: {width}x{height}");
                }
            }
        }

        // Optional cleanup: delete the temporary directory and its contents.
        // Directory.Delete(tempDir, true);
    }
}