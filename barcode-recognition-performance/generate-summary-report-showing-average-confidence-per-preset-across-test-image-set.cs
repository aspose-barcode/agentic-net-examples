// Title: Average Confidence Report for Barcode Recognition Presets
// Description: This example generates sample barcode images, reads them with different quality settings, and calculates the average confidence level for each preset.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on QualitySettings and confidence evaluation. It shows how to create barcodes, configure a BarCodeReader with various presets, and aggregate results—common tasks for developers testing recognition performance across image sets.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode, generation, recognition, qualitysettings, confidence, report, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, reading them with different quality presets,
/// and reporting the average confidence per preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, evaluates them with various QualitySettings,
    /// and prints the average confidence for each preset.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType EncodeType, string Text)>
        {
            (EncodeTypes.Code128, "SampleCode128"),
            (EncodeTypes.QR, "SampleQR"),
            (EncodeTypes.DataMatrix, "SampleDM")
        };

        // Store the full paths of generated images
        var imagePaths = new List<string>();

        // Generate barcode images and save them as PNG files
        foreach (var (encodeType, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{text}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Define the quality presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Map BarCodeConfidence enum values to numeric scores for averaging
        var confidenceValues = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 1 },
            { BarCodeConfidence.Strong, 2 }
        };

        Console.WriteLine("Average Confidence per Preset:");
        // Iterate over each preset and calculate the average confidence
        foreach (var preset in presets)
        {
            var confidences = new List<int>();

            // Process each generated image with the current preset
            foreach (string imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality setting to the reader
                    reader.QualitySettings = preset.Value;

                    BarCodeResult[] results;
                    try
                    {
                        // Attempt to read all barcodes from the image
                        results = reader.ReadBarCodes();
                    }
                    catch (ArgumentException)
                    {
                        // Skip files that cannot be loaded as images
                        continue;
                    }

                    // Convert each result's confidence to a numeric value
                    foreach (var result in results)
                    {
                        if (confidenceValues.TryGetValue(result.Confidence, out int numeric))
                        {
                            confidences.Add(numeric);
                        }
                    }
                }
            }

            // Compute the average confidence for the current preset
            double average = confidences.Count > 0 ? confidences.Average() : 0.0;
            Console.WriteLine($"{preset.Key}: {average:F2}");
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