// Title: Generate average confidence report for barcode recognition presets
// Description: This example creates sample barcode images, reads them using various quality presets, and calculates the average confidence score for each preset.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on QualitySettings to control recognition performance. It showcases creating barcodes with BarcodeGenerator, reading them with BarCodeReader, and aggregating confidence metrics—common tasks for developers optimizing barcode scanning accuracy and speed.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode symbology, generation, recognition, qualitysettings, confidence, report, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate barcodes, recognize them with different quality presets,
/// and compute the average confidence for each preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, evaluates them, and outputs average confidence per preset.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the list of barcode symbologies to generate
        var encodeTypes = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix,
            EncodeTypes.Aztec
        };

        // Generate sample barcode images and collect their file paths
        var barcodeFiles = new List<string>();
        int index = 0;
        foreach (BaseEncodeType encode in encodeTypes)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}_{encode.GetType().Name}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(encode, $"Sample{index}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
            index++;
        }

        // Define recognition quality presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Output header for the confidence report
        Console.WriteLine("Average Confidence per Preset:");

        // Iterate over each preset, read all barcodes, and calculate average confidence
        foreach (var presetEntry in presets)
        {
            string presetName = presetEntry.Key;
            QualitySettings preset = presetEntry.Value;

            int totalConfidence = 0;
            int resultCount = 0;

            // Process each generated barcode file
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset to the reader
                    reader.QualitySettings = preset;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Accumulate confidence values from all detected barcodes
                    foreach (BarCodeResult result in results)
                    {
                        totalConfidence += (int)result.Confidence;
                        resultCount++;
                    }
                }
            }

            // Compute and display the average confidence for the current preset
            double average = resultCount > 0 ? (double)totalConfidence / resultCount : 0.0;
            Console.WriteLine($"{presetName}: Average Confidence = {average:F2}");
        }

        // Cleanup temporary barcode files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}