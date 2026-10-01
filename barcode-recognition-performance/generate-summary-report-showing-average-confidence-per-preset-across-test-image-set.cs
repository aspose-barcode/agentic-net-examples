// Title: Generate average barcode reading quality report across quality presets
// Description: The example creates sample Code128 barcodes, reads them using different quality presets, and calculates the average reading confidence for each preset.
// Category-Description: This Aspose.BarCode example demonstrates barcode generation (BarcodeGenerator) and recognition (BarCodeReader) with a focus on QualitySettings. It shows how to evaluate reading quality across HighPerformance, HighQuality, MaxQuality, and NormalQuality presets—common tasks for developers optimizing barcode scanning performance and accuracy in batch processing scenarios.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode symbology, generation, recognition, quality settings, readingquality, report, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, reading them with various quality presets,
/// and reporting average reading confidence per preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode images, evaluates them with different
    /// quality presets, outputs average reading quality, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode texts to generate
        List<string> sampleTexts = new List<string>
        {
            "123456",
            "ABCDEF",
            "https://example.com",
            "9876543210",
            "Test123"
        };

        // Generate PNG barcode images using Code128 symbology
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < sampleTexts.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define the quality presets to evaluate
        var presets = new Dictionary<string, Action<BarCodeReader>>
        {
            { "HighPerformance", r => r.QualitySettings = QualitySettings.HighPerformance },
            { "HighQuality",     r => r.QualitySettings = QualitySettings.HighQuality },
            { "MaxQuality",      r => r.QualitySettings = QualitySettings.MaxQuality },
            { "NormalQuality",   r => r.QualitySettings = QualitySettings.NormalQuality }
        };

        Console.WriteLine("Average ReadingQuality per preset:");
        Console.WriteLine("-----------------------------------");

        // Process each preset
        foreach (var preset in presets)
        {
            double totalQuality = 0.0;
            int resultCount = 0;

            // Read each generated barcode image
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"Warning: File not found '{file}'. Skipping.");
                    continue;
                }

                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset
                    preset.Value(reader);

                    // Read all barcodes in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Accumulate reading quality values
                    foreach (BarCodeResult result in results)
                    {
                        totalQuality += result.ReadingQuality; // ReadingQuality is a double (0-100)
                        resultCount++;
                    }
                }
            }

            // Compute and display the average reading quality for the preset
            double average = resultCount > 0 ? totalQuality / resultCount : 0.0;
            Console.WriteLine($"{preset.Key}: {average:F2}");
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}