// Title: Demonstrate AutoSizeMode selection based on target DPI for barcode generation
// Description: Shows how to configure Aspose.BarCode's AutoSizeMode to use interpolation for high‑resolution outputs and none for lower DPI values.
// Category-Description: This example belongs to the Aspose.BarCode image rendering category, illustrating how to adjust barcode generation parameters such as Resolution and AutoSizeMode. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common in scenarios where developers need to produce barcodes optimized for different display or print densities. Typical use cases include generating printable labels, high‑resolution marketing materials, and screen‑display barcodes.
// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
// Tags: barcode, autosizemode, dpi, interpolation, highresolution, aspnet, aspnetcore, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates configuring AutoSizeMode based on DPI and generating sample barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Configures the generator's resolution and selects an appropriate AutoSizeMode.
    /// Uses Interpolation for DPI >= 300, otherwise disables auto‑sizing.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="targetDpi">Desired output resolution in dots per inch.</param>
    static void ConfigureAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        // Set the target resolution for the barcode image.
        generator.Parameters.Resolution = targetDpi;

        // Choose AutoSizeMode based on the DPI threshold.
        if (targetDpi >= 300f)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }
    }

    /// <summary>
    /// Entry point that creates barcodes at various DPI settings and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory with a unique name.
        string outputDir = Path.Combine(Path.GetTempPath(), "AutoSizeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the DPI values to test.
        float[] dpis = new float[] { 96f, 300f, 600f };

        // Generate a barcode for each DPI setting.
        foreach (float dpi in dpis)
        {
            string codeText = $"DPI{dpi}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply DPI‑specific AutoSizeMode configuration.
                ConfigureAutoSizeMode(generator, dpi);

                // When using Interpolation mode, set a fixed canvas size for demonstration.
                if (generator.Parameters.AutoSizeMode == AutoSizeMode.Interpolation)
                {
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;
                }

                // Build the output file path and save the barcode as PNG.
                string filePath = Path.Combine(outputDir, $"Barcode_{dpi}dpi.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode at {dpi} DPI to {filePath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}