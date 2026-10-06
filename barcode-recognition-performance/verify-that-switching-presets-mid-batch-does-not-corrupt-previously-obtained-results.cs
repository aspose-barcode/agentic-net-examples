// Title: Verify barcode decoding after switching quality presets mid‑batch
// Description: Demonstrates generating a batch of Code128 barcodes, then reading them while changing the reader's quality preset halfway through, ensuring earlier results remain unaffected.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create images, BarCodeReader with QualitySettings to control decoding performance, and typical batch processing patterns. Developers often need to adjust quality presets for speed or accuracy and must verify that changing settings does not corrupt previously decoded results.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode, code128, qualitysettings, batch, generation, recognition, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading a batch of Code128 barcodes while switching
/// quality presets mid‑batch to verify that earlier results remain correct.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them with varying quality
    /// presets, validates decoded texts, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPresetTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and store expected texts
        List<string> filePaths = new List<string>();
        List<string> expectedTexts = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            // Generate a Code128 barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            filePaths.Add(filePath);
            expectedTexts.Add(codeText);
        }

        // Read the barcodes, switching quality presets mid‑batch
        bool allMatch = true;
        for (int i = 0; i < filePaths.Count; i++)
        {
            string file = filePaths[i];
            string expected = expectedTexts[i];

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply NormalQuality to the first half, HighQuality to the second half
                if (i < filePaths.Count / 2)
                {
                    reader.QualitySettings = QualitySettings.NormalQuality;
                }
                else
                {
                    reader.QualitySettings = QualitySettings.HighQuality;
                }

                // Decode the barcode(s) in the current image
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                    allMatch = false;
                    continue;
                }

                string decoded = results[0].CodeText;
                if (decoded != expected)
                {
                    Console.WriteLine($"Mismatch in {Path.GetFileName(file)}: expected '{expected}', got '{decoded}'");
                    allMatch = false;
                }
                else
                {
                    Console.WriteLine($"Decoded {Path.GetFileName(file)} successfully: '{decoded}'");
                }
            }
        }

        // Report overall verification result
        Console.WriteLine(allMatch
            ? "All barcodes decoded correctly after preset switch."
            : "Some barcodes failed verification.");

        // Clean up temporary files and folder
        try
        {
            foreach (string f in filePaths) File.Delete(f);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored
        }
    }
}