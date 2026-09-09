// Title: Batch TIFF Barcode Generation and Recognition with Accuracy Reporting
// Description: Demonstrates generating a set of Code128 barcodes as TIFF images, then reading them back to calculate recognition accuracy.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to create barcodes, BarCodeReader for decoding, and QualitySettings for performance tuning. Typical use cases include batch processing of scanned documents, quality testing of barcode readability, and automated reporting. Developers often need to generate test images, evaluate decoding success rates, and log results for further analysis.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: code128, generation, recognition, tiff, barcodegenerator, barcodereader, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the batch barcode generation and recognition example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code128 barcodes as TIFF files, reads them back, and reports average recognition accuracy.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode texts
        List<string> codeTexts = new List<string>
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Generate TIFF images with barcodes
        List<string> generatedFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"barcode_{text}.tiff");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // No explicit size settings; default auto-sizing
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            generatedFiles.Add(filePath);
        }

        // Process each TIFF image and record recognition success
        int successCount = 0;
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Use a high-performance preset for speed
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                    if (success) successCount++;

                    Console.WriteLine($"Processed {Path.GetFileName(file)} - Success: {success}");
                }
            }
            catch (ArgumentException ex)
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping file due to load error: {file}. Message: {ex.Message}");
            }
        }

        // Calculate average recognition accuracy
        double averageAccuracy = (double)successCount / generatedFiles.Count * 100.0;
        string report = $"Total images: {generatedFiles.Count}, Successful reads: {successCount}, Average accuracy: {averageAccuracy:F2}%{Environment.NewLine}";
        Console.WriteLine(report);

        // Write report to a log file in the batch folder
        string logPath = Path.Combine(batchFolder, "RecognitionReport.txt");
        File.AppendAllText(logPath, report);
    }
}