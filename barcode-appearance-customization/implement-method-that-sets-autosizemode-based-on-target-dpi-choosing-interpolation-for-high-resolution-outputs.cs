// Title: Set AutoSizeMode Based on Target DPI for Barcode Generation
// Description: Demonstrates how to configure the AutoSizeMode of Aspose.BarCode's BarcodeGenerator according to the desired output DPI, using interpolation for high‑resolution images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to adjust image scaling behavior via the AutoSizeMode property. It covers the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes at different resolutions. Developers often need to control scaling for print‑ready or screen‑display barcodes, making this pattern useful for high‑DPI output scenarios.
// Prompt: Implement a method that sets AutoSizeMode based on target DPI, choosing Interpolation for high‑resolution outputs.
// Tags: barcode, autosizemode, dpi, interpolation, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring AutoSizeMode based on target DPI and generating barcodes at different resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Sets the AutoSizeMode of the provided <see cref="BarcodeGenerator"/> according to the target DPI.
    /// Uses <see cref="AutoSizeMode.Interpolation"/> for high‑resolution (≥300 DPI) outputs; otherwise disables auto‑sizing.
    /// Also assigns the requested resolution to the generator parameters.
    /// </summary>
    /// <param name="generator">The barcode generator to configure.</param>
    /// <param name="targetDpi">Desired output DPI.</param>
    static void ConfigureAutoSizeMode(BarcodeGenerator generator, float targetDpi)
    {
        // Choose interpolation for high‑resolution images, otherwise no auto‑sizing.
        if (targetDpi >= 300f)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
        }

        // Apply the target DPI to the generator.
        generator.Parameters.Resolution = targetDpi;
    }

    /// <summary>
    /// Generates two barcode images—one high‑resolution and one low‑resolution—showcasing the AutoSizeMode configuration.
    /// </summary>
    static void Main()
    {
        // Prepare output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // -------------------- High‑resolution example (300 DPI) --------------------
        float highDpi = 300f;
        string highPath = Path.Combine(outputDir, $"barcode_{highDpi}dpi.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE_HIGH"))
        {
            // Configure generator for high DPI.
            ConfigureAutoSizeMode(generator, highDpi);
            generator.Parameters.ImageWidth.Pixels = 400f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the barcode image.
            generator.Save(highPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved high‑resolution barcode to {highPath} with AutoSizeMode {generator.Parameters.AutoSizeMode}");
        }

        // -------------------- Low‑resolution example (96 DPI) --------------------
        float lowDpi = 96f;
        string lowPath = Path.Combine(outputDir, $"barcode_{lowDpi}dpi.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE_LOW"))
        {
            // Configure generator for low DPI.
            ConfigureAutoSizeMode(generator, lowDpi);
            generator.Parameters.ImageWidth.Pixels = 400f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the barcode image.
            generator.Save(lowPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved low‑resolution barcode to {lowPath} with AutoSizeMode {generator.Parameters.AutoSizeMode}");
        }
    }
}