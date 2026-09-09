// Title: Switching Between HighPerformance and HighQuality Quality Settings in Aspose.BarCode
// Description: Demonstrates how to read a barcode using the HighPerformance and HighQuality presets, showing the impact on recognition speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings presets (HighPerformance, HighQuality) for decoding. Developers often need to balance speed versus accuracy when processing large volumes of barcodes; these presets provide quick switches for common scenarios.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: barcode symbology, barcode generation, barcode recognition, quality settings, highperformance, highquality, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates switching between HighPerformance and HighQuality quality settings
/// when reading a barcode with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, then reads it twice
    /// using different QualitySettings presets to illustrate their usage.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string demoFolder = Path.Combine(Path.GetTempPath(), "BarcodePresetDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(demoFolder);
        string barcodePath = Path.Combine(demoFolder, "code128.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeDemo"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using the HighPerformance preset (optimized for speed)
        Console.WriteLine("Reading with HighPerformance preset:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Read the barcode using the HighQuality preset (optimized for accuracy)
        Console.WriteLine("Reading with HighQuality preset:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings = QualitySettings.HighQuality;
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Optional: clean up the temporary files
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(demoFolder))
                Directory.Delete(demoFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}