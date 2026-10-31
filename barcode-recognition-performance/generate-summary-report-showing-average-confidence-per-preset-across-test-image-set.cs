// Title: Generate Summary Report of Average Confidence per Recognition Preset
// Description: The example creates several barcode images, reads them using different quality presets, and calculates the average confidence score for each preset.
// Category-Description: This sample belongs to the Aspose.BarCode recognition category, demonstrating how to configure QualitySettings presets (HighPerformance, NormalQuality, HighQuality, MaxQuality) with BarCodeReader, generate barcodes, and compute confidence metrics. Developers often need to evaluate recognition reliability across presets for batch processing or testing scenarios.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode, recognition, confidence, qualitysettings, preset, summary, report, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images, reading them with various quality presets,
/// and reporting the average confidence for each preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, evaluates them, and prints a summary report.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for test barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode specifications to generate (type, data, file name)
        var barcodeSpecs = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Test123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AztecData", "aztec.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encodeType, spec.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Define recognition presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Prepare a dictionary to hold the average confidence per preset
        var report = new Dictionary<string, double>();

        // Process each preset
        foreach (var presetEntry in presets)
        {
            string presetName = presetEntry.Key;
            QualitySettings preset = presetEntry.Value;

            int totalConfidence = 0;
            int resultCount = 0;

            // Read each generated barcode image using the current preset
            foreach (var spec in barcodeSpecs)
            {
                string imagePath = Path.Combine(tempFolder, spec.fileName);
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings = preset;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Accumulate confidence values from all detected barcodes
                    foreach (var result in results)
                    {
                        // Confidence is an enum; cast to int for averaging
                        totalConfidence += (int)result.Confidence;
                        resultCount++;
                    }
                }
            }

            // Compute average confidence for the current preset
            double average = resultCount > 0 ? (double)totalConfidence / resultCount : 0.0;
            report[presetName] = average;
        }

        // Output summary report
        Console.WriteLine("Average Confidence per Recognition Preset:");
        foreach (var entry in report)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value:F2}");
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}