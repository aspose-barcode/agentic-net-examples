// Title: Record barcode detection counts per image across quality presets
// Description: Generates sample barcode images and reads them using various quality settings to count detected barcodes, illustrating how to analyze detection consistency.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates creating barcodes with BarcodeGenerator, configuring BarCodeReader quality presets, and counting detected symbols. Developers commonly use these APIs to evaluate detection performance, tune quality settings, and ensure reliable barcode scanning across different image conditions.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode, generation, recognition, qualitysettings, count, detection, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes, reading them with different quality presets,
/// and recording the number of detected barcodes per image for consistency analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, applies various quality
    /// presets during recognition, and outputs detection counts per image.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample128", "code128.png"),
            (EncodeTypes.QR, "SampleQR", "qr.png"),
            (EncodeTypes.DataMatrix, "SampleDM", "datamatrix.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(type, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Define the quality presets to test during recognition
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Record detection counts per image for each preset
        var results = new Dictionary<string, Dictionary<string, int>>();

        foreach (var presetKvp in presets)
        {
            string presetName = presetKvp.Key;
            QualitySettings preset = presetKvp.Value;
            var imageCounts = new Dictionary<string, int>();

            // Iterate over each generated image
            foreach (var (_, _, fileName) in samples)
            {
                string filePath = Path.Combine(tempFolder, fileName);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Read barcodes using the current quality preset
                using (var reader = new BarCodeReader(filePath))
                {
                    reader.QualitySettings = preset;
                    int count = reader.ReadBarCodes().Length;
                    imageCounts[fileName] = count;
                }
            }

            results[presetName] = imageCounts;
        }

        // Output the recorded detection counts
        Console.WriteLine("Barcode detection counts per image under each preset:");
        foreach (var presetKvp in results)
        {
            Console.WriteLine($"Preset: {presetKvp.Key}");
            foreach (var imageKvp in presetKvp.Value)
            {
                Console.WriteLine($"  Image: {imageKvp.Key} => Detected: {imageKvp.Value}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}