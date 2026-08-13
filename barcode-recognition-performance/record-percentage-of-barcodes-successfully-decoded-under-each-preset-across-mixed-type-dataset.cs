// Title: Barcode generation and decoding success rate per symbology preset
// Description: This example generates a set of barcodes for several symbologies, decodes them, and reports the percentage of successful decodings.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, covering BarcodeGenerator, BarCodeReader, and related parameter settings. Useful for developers testing barcode quality, batch processing, or evaluating decoding reliability across different symbologies.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode symbology, generation, recognition, png, aspose.barcode, encode type, decode type

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes for multiple symbologies, decoding them, and reporting success percentages.
/// </summary>
class Program
{
    // Simple data holder for each preset (symbology)
    class PresetInfo
    {
        public string Name { get; set; }
        public BaseEncodeType EncodeType { get; set; }
        public List<string> Files { get; } = new List<string>();
        public List<string> ExpectedTexts { get; } = new List<string>();
    }

    /// <summary>
    /// Entry point. Generates barcodes, decodes them, and prints success rates per preset.
    /// </summary>
    static void Main()
    {
        // Folder to store generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Define a few presets (symbologies) with sample texts
        var presets = new List<PresetInfo>
        {
            new PresetInfo
            {
                Name = "Code128",
                EncodeType = EncodeTypes.Code128
            },
            new PresetInfo
            {
                Name = "QR",
                EncodeType = EncodeTypes.QR
            },
            new PresetInfo
            {
                Name = "DataMatrix",
                EncodeType = EncodeTypes.DataMatrix
            }
        };

        // Number of samples per preset (kept small for CI safety)
        const int samplesPerPreset = 5;

        // Generate sample barcodes for each preset
        foreach (var preset in presets)
        {
            for (int i = 0; i < samplesPerPreset; i++)
            {
                string codeText = $"{preset.Name}_Sample_{i + 1}";
                string filePath = Path.Combine(outputFolder, $"{preset.Name}_{i + 1}.png");

                using (var generator = new BarcodeGenerator(preset.EncodeType, codeText))
                {
                    // Example of setting a simple parameter (optional)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Store generated file path and expected text for later verification
                preset.Files.Add(filePath);
                preset.ExpectedTexts.Add(codeText);
            }
        }

        // Prepare results container: key = preset name, value = (total count, successful decodes)
        var results = new Dictionary<string, (int total, int success)>();

        // Decode each generated image and evaluate success per preset
        foreach (var preset in presets)
        {
            int total = preset.Files.Count;
            int success = 0;

            for (int i = 0; i < total; i++)
            {
                string file = preset.Files[i];
                string expected = preset.ExpectedTexts[i];

                if (!File.Exists(file))
                {
                    Console.WriteLine($"Warning: File not found '{file}'. Skipping.");
                    continue;
                }

                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes in the image (there should be only one)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Consider it a success if a code text is returned and matches the expected value
                        if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == expected)
                        {
                            success++;
                        }
                        break; // only first result needed
                    }
                }
            }

            results[preset.Name] = (total, success);
        }

        // Output percentage of successful decodings per preset
        Console.WriteLine("Decoding success percentages per preset:");
        foreach (var kvp in results)
        {
            string name = kvp.Key;
            int total = kvp.Value.total;
            int success = kvp.Value.success;
            double percentage = total > 0 ? (double)success / total * 100.0 : 0.0;
            Console.WriteLine($"{name}: {percentage:F2}% ({success}/{total})");
        }
    }
}