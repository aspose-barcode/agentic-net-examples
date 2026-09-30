// Title: Generate barcodes with various AutoSizeMode settings and log image dimensions
// Description: This example creates Code128 barcodes using different AutoSizeMode options, saves them as PNG files, and logs the selected mode along with the resulting image size.
// Category-Description: Demonstrates Aspose.BarCode image sizing techniques, focusing on the AutoSizeMode property of BarcodeGenerator.Parameters. It shows how to configure fixed and dynamic canvas sizes, generate bitmap images, and retrieve dimensions—common tasks for developers integrating barcode generation into reporting, labeling, or UI workflows.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode, autosizemode, image generation, logging, code128, aspose.barcode, bitmap, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 barcodes with different AutoSizeMode settings,
/// saving them as PNG files, and logging the mode and resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, generates barcodes,
    /// saves the images, and writes details to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define test cases with different AutoSizeMode configurations.
        var samples = new[]
        {
            new { Text = "12345", Mode = AutoSizeMode.None, Width = 0f, Height = 0f },
            new { Text = "ABCDEFGHIJ", Mode = AutoSizeMode.Nearest, Width = 300f, Height = 150f },
            new { Text = "LongerCodeTextExample", Mode = AutoSizeMode.Interpolation, Width = 400f, Height = 200f }
        };

        // Iterate over each sample, generate barcode, save and log details.
        foreach (var sample in samples)
        {
            // Initialize generator with specified symbology and text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sample.Text))
            {
                // Set the AutoSizeMode as per the current sample.
                generator.Parameters.AutoSizeMode = sample.Mode;

                // If mode requires a fixed canvas, assign width and height.
                if (sample.Mode != AutoSizeMode.None)
                {
                    generator.Parameters.ImageWidth.Pixels = sample.Width;
                    generator.Parameters.ImageHeight.Pixels = sample.Height;
                }

                // Generate barcode bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save bitmap to PNG file.
                    string filePath = Path.Combine(outputDir, $"{sample.Text}_{sample.Mode}.png");
                    bitmap.Save(filePath, ImageFormat.Png);

                    // Output details to console.
                    Console.WriteLine($"Generated barcode for '{sample.Text}'");
                    Console.WriteLine($"AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                    Console.WriteLine($"Image dimensions: {bitmap.Width}x{bitmap.Height} pixels");
                    Console.WriteLine($"Saved to: {filePath}");
                    Console.WriteLine();
                }
            }
        }

        Console.WriteLine("All barcodes have been generated.");
    }
}