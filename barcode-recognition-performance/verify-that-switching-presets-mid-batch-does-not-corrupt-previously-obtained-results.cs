// Title: Switching Barcode Quality Presets Mid‑Batch
// Description: Demonstrates generating a batch of Code128 barcodes, reading them while changing the reader's QualitySettings preset halfway through, and confirming earlier results stay correct.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create images, BarCodeReader with QualitySettings presets (HighPerformance, HighQuality) to decode, and typical batch‑processing patterns where settings may change. Developers often need to switch presets for performance or quality reasons without affecting previously processed data, making this a useful reference for CI pipelines and automated scanning workflows.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode, code128, qualitysettings, preset, batch, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates switching barcode reader quality presets during batch processing
/// and verifying that earlier decoded results remain unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, reads them with varying presets,
    /// and validates consistency of earlier results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "PresetBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcodes
        int totalBarcodes = 6;
        List<string> filePaths = new List<string>();
        List<string> expectedTexts = new List<string>();
        for (int i = 1; i <= totalBarcodes; i++)
        {
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
            expectedTexts.Add(codeText);
        }

        // Read barcodes in a batch, switching presets mid‑batch
        Dictionary<string, string> firstHalfResults = new Dictionary<string, string>();
        int half = totalBarcodes / 2;
        for (int index = 0; index < filePaths.Count; index++)
        {
            string path = filePaths[index];
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Choose preset based on position in the batch
            QualitySettings preset = (index < half) ? QualitySettings.HighPerformance : QualitySettings.HighQuality;

            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Apply the selected quality preset
                reader.QualitySettings = preset;
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(path)} using preset {preset}.");
                    continue;
                }

                string decodedText = results[0].CodeText;
                Console.WriteLine($"File: {Path.GetFileName(path)} | Preset: {preset} | Decoded: {decodedText}");

                // Store results from the first half for later verification
                if (index < half)
                {
                    firstHalfResults[path] = decodedText;
                }
            }
        }

        // Verify that previously obtained results are unchanged
        bool allMatch = true;
        foreach (var kvp in firstHalfResults)
        {
            string original = expectedTexts[filePaths.IndexOf(kvp.Key)];
            if (!string.Equals(kvp.Value, original, StringComparison.Ordinal))
            {
                Console.WriteLine($"Mismatch for {Path.GetFileName(kvp.Key)}: expected {original}, got {kvp.Value}");
                allMatch = false;
            }
        }

        Console.WriteLine(allMatch
            ? "All previously obtained results match expected values after preset switch."
            : "Some previously obtained results do not match expected values.");

        // Cleanup temporary files
        try
        {
            foreach (var file in filePaths)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}