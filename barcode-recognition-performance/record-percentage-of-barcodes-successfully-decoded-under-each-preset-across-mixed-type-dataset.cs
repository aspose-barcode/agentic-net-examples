// Title: Barcode decoding success rate per quality preset
// Description: Demonstrates generating a mixed‑type barcode set, decoding them with different quality presets, and reporting the success percentage for each preset.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode, decoding, qualitysettings, aspose.barcode, csharp, example

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes of various symbologies, decoding them using different
/// quality presets, and reporting the success rate for each preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, decodes them with each preset, and prints success percentages.
    /// </summary>
    static void Main()
    {
        // Create output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a small mixed‑type dataset (symbology + sample text + file name)
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC12345", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM123", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE_TEXT", "pdf417.png"),
            (EncodeTypes.DatabarStacked, "(01)01234567890128", "databar.png")
        };

        // Generate barcode images for each sample
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(outputDir, fileName);
            using (var generator = new BarcodeGenerator(type, text))
            {
                generator.Save(filePath);
            }
        }

        // Define decoding presets (different QualitySettings)
        var presets = new List<(string name, QualitySettings settings)>
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("HighQuality", QualitySettings.HighQuality),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        // Evaluate each preset by decoding all generated barcodes
        foreach (var (presetName, presetSettings) in presets)
        {
            int total = samples.Count;
            int success = 0;

            foreach (var (type, expectedText, fileName) in samples)
            {
                string filePath = Path.Combine(outputDir, fileName);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Warning: file not found {filePath}");
                    continue;
                }

                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = presetSettings;

                    // Read barcodes from the image
                    var results = reader.ReadBarCodes();

                    // Check if any result matches the expected text
                    foreach (var result in results)
                    {
                        if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == expectedText)
                        {
                            success++;
                            break;
                        }
                    }
                }
            }

            // Calculate and display success percentage for the current preset
            double percentage = (double)success / total * 100.0;
            Console.WriteLine($"{presetName}: {percentage:F2}% ({success}/{total}) barcodes decoded successfully.");
        }

        // Cleanup: optional removal of generated files
        // foreach (var file in Directory.GetFiles(outputDir)) File.Delete(file);
        // Directory.Delete(outputDir);
    }
}