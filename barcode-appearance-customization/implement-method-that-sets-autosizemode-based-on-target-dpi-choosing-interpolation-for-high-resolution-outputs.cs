// Title: AutoSizeMode Configuration Based on DPI
// Description: Demonstrates setting AutoSizeMode for a barcode generator according to target DPI, using interpolation for high‑resolution outputs.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control image resolution and sizing via the BarcodeGenerator, AutoSizeMode, and Resolution properties. Typical use cases include creating printable barcodes at various DPI settings, where developers need to switch between default sizing and interpolation for crisp high‑resolution results. Ideal for developers searching for barcode DPI handling, auto‑size configuration, and image scaling techniques in Aspose.BarCode.
/// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
/// Tags: barcode, code128, autosizemode, interpolation, high-resolution, dpi, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of configuring AutoSizeMode based on target DPI
/// and generating a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Configures the <see cref="BarcodeGenerator"/> AutoSizeMode according to the specified DPI.
    /// For DPI values greater than 150, Interpolation mode is applied; otherwise, No auto‑sizing is used.
    /// </summary>
    /// <param name="generator">The barcode generator to configure.</param>
    /// <param name="targetDpi">The desired image resolution in dots per inch.</param>
    static void ConfigureAutoSize(BarcodeGenerator generator, float targetDpi)
    {
        // Set the resolution for the barcode image.
        generator.Parameters.Resolution = targetDpi;

        if (targetDpi > 150f)
        {
            // High‑resolution output: enable interpolation.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // When using interpolation, control the final size via ImageWidth/ImageHeight.
            generator.Parameters.ImageWidth.Point = 300f;   // example width
            generator.Parameters.ImageHeight.Point = 150f; // example height
        }
        else
        {
            // Standard resolution: no automatic resizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }

    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode image with DPI‑based auto‑size settings.
    /// </summary>
    static void Main()
    {
        // Sample barcode data.
        const string codeText = "1234567890";

        // Desired DPI for the output image (high‑resolution example).
        const float targetDpi = 300f;

        // Determine the output file path.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        try
        {
            // Initialize the barcode generator with Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set a foreground color.
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Apply DPI‑based auto‑size configuration.
                ConfigureAutoSize(generator, targetDpi);

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }

            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}