// Title: Set AutoSizeMode based on DPI for barcode generation
// Description: Demonstrates how to configure a barcode generator's resolution and automatically select the appropriate AutoSizeMode, using interpolation for high‑resolution outputs.
// Category-Description: This example belongs to the Aspose.BarCode image rendering category, illustrating the use of BarcodeGenerator, its Parameters, and AutoSizeMode settings. Developers often need to adjust DPI and scaling behavior when generating barcodes for print or high‑quality displays; this snippet shows typical configuration steps and saving the result as PNG.
// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
// Tags: barcode, autosizemode, dpi, interpolation, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates configuring AutoSizeMode based on DPI for barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    // Configures the barcode generator's resolution and AutoSizeMode.
    // For high‑resolution (DPI > 200) outputs, Interpolation mode is used.
    static void ConfigureAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        if (targetDpi <= 0f) throw new ArgumentOutOfRangeException(nameof(targetDpi));

        // Set the desired resolution (DPI).
        generator.Parameters.Resolution = targetDpi;

        // Choose AutoSizeMode based on DPI.
        if (targetDpi > 200f)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }

    /// <summary>
    /// Entry point that creates a Code128 barcode, applies DPI‑based AutoSizeMode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample barcode data.
        string codeText = "1234567890";

        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Target DPI for the output image.
            float targetDpi = 300f;

            // Apply configuration (resolution and AutoSizeMode).
            ConfigureAutoSizeMode(generator, targetDpi);

            // Define output path.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Ensure the directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Output diagnostic information.
            Console.WriteLine($"Barcode saved to: {outputPath}");
            Console.WriteLine($"Resolution set to {generator.Parameters.Resolution} DPI");
            Console.WriteLine($"AutoSizeMode set to {generator.Parameters.AutoSizeMode}");
        }
    }
}