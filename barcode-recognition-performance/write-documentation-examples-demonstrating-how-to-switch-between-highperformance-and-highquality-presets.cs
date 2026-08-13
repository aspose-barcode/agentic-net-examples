// Title: Switching between HighPerformance and HighQuality barcode reading presets
// Description: Demonstrates how to read a barcode using Aspose.BarCode's QualitySettings presets for performance versus quality.
// Category-Description: This example belongs to the Aspose.BarCode reading configuration category, illustrating the use of BarCodeReader with different QualitySettings (HighPerformance and HighQuality). Developers often need to balance speed and accuracy when scanning barcodes; this snippet shows how to toggle presets, a common requirement in batch processing or real‑time scanning scenarios.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: barcode symbology, reading preset, qualitysettings, highperformance, highquality, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode, then reads it twice using
/// different <see cref="QualitySettings"/> presets (HighPerformance and HighQuality).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with two
    /// different quality presets, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and full path for the temporary barcode image.
        string outputDir = Directory.GetCurrentDirectory();
        string barcodePath = Path.Combine(outputDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using the HighPerformance preset (faster, lower accuracy).
        // --------------------------------------------------------------------
        Console.WriteLine("Reading with QualitySettings.HighPerformance:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the HighPerformance preset.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcodes and output their type and text.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read the same barcode using the HighQuality preset (slower, higher accuracy).
        // --------------------------------------------------------------------
        Console.WriteLine("Reading with QualitySettings.HighQuality:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the HighQuality preset.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes and output their type and text.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Optional clean‑up: delete the temporary barcode image.
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignored – file may be in use or deletion may fail on some platforms.
        }
    }
}