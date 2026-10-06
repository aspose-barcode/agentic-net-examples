// Title: Switching Between HighPerformance and HighQuality Presets in Aspose.BarCode
// Description: Demonstrates how to read a barcode using the HighPerformance and HighQuality quality presets, illustrating their effect on recognition results.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader with different QualitySettings presets (HighPerformance, HighQuality) to decode the image. Developers working with barcode scanning often need to balance speed versus accuracy; these presets provide quick ways to toggle between performance‑focused and quality‑focused recognition.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: barcode symbology, quality preset, recognition, generation, aspose.barcode, highperformance, highquality

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates switching between HighPerformance and HighQuality quality presets
/// when reading a barcode with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, then reads it
    /// using both HighPerformance and HighQuality presets, outputting the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "PresetDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate a sample Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Define the quality presets to demonstrate
        var presets = new (string Name, QualitySettings Settings)[]
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("HighQuality", QualitySettings.HighQuality)
        };

        // Iterate over each preset, read the barcode, and display results
        foreach (var preset in presets)
        {
            Console.WriteLine($"Reading with preset: {preset.Name}");
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Apply the selected quality preset
                reader.QualitySettings = preset.Settings;

                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            Console.WriteLine();
        }

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}