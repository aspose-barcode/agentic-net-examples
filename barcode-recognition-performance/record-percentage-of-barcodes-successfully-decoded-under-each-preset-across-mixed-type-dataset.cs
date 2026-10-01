// Title: Barcode decoding success rate across quality presets
// Description: Generates a mixed set of barcode images and measures the percentage successfully decoded using each Aspose.BarCode quality preset.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create barcodes with BarcodeGenerator, read them with BarCodeReader, and evaluate different QualitySettings (HighPerformance, HighQuality, MaxQuality, NormalQuality). Developers often need to benchmark decoding reliability under varying performance/quality trade‑offs, especially when processing heterogeneous barcode datasets in batch workflows.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode, decoding, quality, aspose.barcode, generation, recognition, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating several barcode types, then evaluating the decoding success
/// percentage for each Aspose.BarCode quality preset on the generated images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, runs decoding
    /// with each quality preset, reports success rates, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode specifications (symbology, sample text, and output file name)
        var barcodeSpecs = new List<(BaseEncodeType EncodeType, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "TestQR", "qr.png"),
            (EncodeTypes.DataMatrix, "DM1234", "datamatrix.png"),
            (EncodeTypes.Interleaved2of5, "9876543210", "itf.png")
        };

        // Generate barcode images and collect their file paths
        var barcodeFiles = new List<string>();
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(tempFolder, spec.FileName);
            using (var generator = new BarcodeGenerator(spec.EncodeType, spec.Text))
            {
                // Minimal configuration; defaults are sufficient for this demo
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        int totalBarcodes = barcodeFiles.Count;

        // Define the quality presets to evaluate
        string[] presetNames = { "HighPerformance", "HighQuality", "MaxQuality", "NormalQuality" };

        // Evaluate each preset by attempting to decode all generated barcodes
        foreach (string presetName in presetNames)
        {
            int successCount = 0;

            foreach (string file in barcodeFiles)
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset to the reader
                    switch (presetName)
                    {
                        case "HighPerformance":
                            reader.QualitySettings = QualitySettings.HighPerformance;
                            break;
                        case "HighQuality":
                            reader.QualitySettings = QualitySettings.HighQuality;
                            break;
                        case "MaxQuality":
                            reader.QualitySettings = QualitySettings.MaxQuality;
                            break;
                        case "NormalQuality":
                            reader.QualitySettings = QualitySettings.NormalQuality;
                            break;
                    }

                    // Read barcodes; success is determined by presence of any result with non‑empty text
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        successCount++;
                    }
                }
            }

            // Calculate and display the success percentage for the current preset
            double successPercent = (double)successCount / totalBarcodes * 100.0;
            Console.WriteLine($"Preset {presetName}: {successPercent:F2}% success ({successCount}/{totalBarcodes})");
        }

        // Cleanup temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually reclaim the temp folder.
        }
    }
}