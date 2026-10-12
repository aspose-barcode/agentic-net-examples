// Title: Barcode Confidence Distribution Report
// Description: Generates sample barcodes, reads them back, and reports the distribution of confidence levels returned by the Aspose.BarCode recognizer.
// Category-Description: This example demonstrates core Aspose.BarCode operations: barcode generation with BarcodeGenerator, barcode recognition with BarCodeReader, and analysis of BarCodeResult confidence values. It is useful for developers who need to assess scan quality across large datasets, create batch processing pipelines, or generate statistical reports on barcode readability. Typical use cases include quality control, inventory audits, and automated data capture systems.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode, confidence, distribution, report, aspose.barcode, generation, recognition, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a set of barcodes, read them back,
/// and produce a statistical report of the confidence levels reported by the recognizer.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them,
    /// aggregates confidence counts, outputs a summary, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.Code128, "Sample128"),
            (EncodeTypes.QR, "SampleQR"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417Test"),
            (EncodeTypes.Aztec, "AztecDemo")
        };

        // Generate barcode images and collect file paths
        var generatedFiles = new List<string>();
        foreach (var (encodeType, codeText) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{codeText}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Initialize confidence distribution dictionary
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // Read each generated barcode and tally confidence values
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        BarCodeConfidence confidence = result.Confidence;
                        if (confidenceCounts.ContainsKey(confidence))
                        {
                            confidenceCounts[confidence]++;
                        }
                        else
                        {
                            confidenceCounts[confidence] = 1;
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
        }

        // Output the summary report
        Console.WriteLine("=== Barcode Confidence Distribution Report ===");
        int total = 0;
        foreach (var count in confidenceCounts.Values)
        {
            total += count;
        }

        foreach (var kvp in confidenceCounts)
        {
            double percentage = total > 0 ? (kvp.Value * 100.0) / total : 0;
            Console.WriteLine($"{kvp.Key}: {kvp.Value} ({percentage:F2}%)");
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in generatedFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}