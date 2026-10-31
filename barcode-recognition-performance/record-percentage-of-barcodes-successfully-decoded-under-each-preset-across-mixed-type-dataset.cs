// Title: Barcode decoding success rate across quality presets
// Description: Generates a set of sample barcodes, decodes them using different quality settings, and reports the percentage of barcodes successfully decoded for each preset.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings to recognize them, and how to evaluate recognition performance. Developers working with barcode scanning, image quality optimization, or batch processing can use these APIs to benchmark and select appropriate quality presets.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode symbology, decoding, quality settings, aspose.barcode, generation, recognition, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates recording the percentage of barcodes successfully decoded under each quality preset across a mixed‑type dataset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them with various quality settings, and outputs success rates.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes (symbology, code text)
        var samples = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.Code128, "CODE128TEST"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AZTEC123"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE")
        };

        // Generate barcode images and collect file paths
        var barcodeFiles = new List<string>();
        foreach (var (encodeType, codeText) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType.TypeName}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define recognition quality presets to test
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        int totalBarcodes = barcodeFiles.Count;

        // Evaluate each preset
        foreach (var kvp in presets)
        {
            string presetName = kvp.Key;
            QualitySettings preset = kvp.Value;

            int successCount = 0;

            // Attempt to decode each generated barcode using the current preset
            foreach (string file in barcodeFiles)
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
                        reader.QualitySettings = preset;
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Count as success if at least one result with non‑empty text is returned
                        if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                        {
                            successCount++;
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed or unsupported file; skip and log warning
                    Console.WriteLine($"Warning: {ex.Message}");
                }
            }

            // Calculate and display success percentage for the current preset
            double percentage = (double)successCount / totalBarcodes * 100.0;
            Console.WriteLine($"{presetName}: {successCount}/{totalBarcodes} successfully decoded ({percentage:F2}%)");
        }

        // Cleanup generated files and folder
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}