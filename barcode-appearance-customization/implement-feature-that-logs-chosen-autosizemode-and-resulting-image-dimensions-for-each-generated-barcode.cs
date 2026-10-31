// Title: Barcode AutoSizeMode Demonstration with Dimension Logging
// Description: Shows how different AutoSizeMode settings affect the size of a generated Code128 barcode and logs the chosen mode along with the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image size parameters. Developers often need to control barcode scaling for various output formats, and this snippet demonstrates typical configuration patterns and logging practices for barcode image creation.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode, code128, autosizemode, image generation, png, aspose.barcode, aspose.drawing, logging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how different <see cref="AutoSizeMode"/> settings affect generated barcode images,
/// logs the selected mode and resulting dimensions, and saves the images as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code128 barcodes using various <see cref="AutoSizeMode"/> values,
    /// logs image dimensions, and writes PNG files to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoSizeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define barcode content and symbology.
        string codeText = "Sample123";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // List of AutoSizeMode options to demonstrate.
        AutoSizeMode[] modes = new[] { AutoSizeMode.None, AutoSizeMode.Interpolation, AutoSizeMode.Nearest };

        // Iterate over each AutoSizeMode, generate the barcode, log details, and save the image.
        foreach (AutoSizeMode mode in modes)
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the current AutoSizeMode to the generator.
                generator.Parameters.AutoSizeMode = mode;

                // Configure image dimensions based on the selected mode.
                if (mode == AutoSizeMode.Interpolation || mode == AutoSizeMode.Nearest)
                {
                    // For scaling modes, set explicit image width and height.
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;
                }
                else // AutoSizeMode.None
                {
                    // When no auto-sizing, define the barcode's X-dimension.
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                }

                // Generate the barcode image.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Log the mode and resulting image size.
                    Console.WriteLine($"AutoSizeMode: {mode}");
                    Console.WriteLine($"Resulting image dimensions: {bitmap.Width}x{bitmap.Height} pixels");

                    // Build the file path and save the image as PNG.
                    string filePath = Path.Combine(outputDir, $"barcode_{mode}.png");
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, ImageFormat.Png);
                    }
                }
            }
        }

        // Inform the user where the barcode images have been saved.
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}