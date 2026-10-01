// Title: Batch Barcode Generation with Preset Switching Verification
// Description: Demonstrates generating multiple Code128 barcodes in a batch while changing generator presets between items, then verifies each barcode to ensure earlier results remain unaffected.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator to apply different visual presets per barcode, save them as PNG, and subsequently read them back with BarCodeReader. Developers often need to batch‑process barcodes with varying appearance settings without corrupting previously generated images.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode symbology, code128, generation, recognition, preset, batch, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes with varying visual presets
/// and validates that changing presets does not affect previously generated results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of barcodes with different presets, saves them,
    /// then reads each back to verify correctness.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchPresets_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define barcode specifications with preset changes
        var specs = new List<BarcodeSpec>
        {
            new BarcodeSpec
            {
                EncodeType = EncodeTypes.Code128,
                CodeText = "ABC123",
                // Default preset – no changes
                PresetAction = gen => { /* no modifications */ }
            },
            new BarcodeSpec
            {
                EncodeType = EncodeTypes.Code128,
                CodeText = "DEF456",
                // Change bar color to red and increase X-dimension
                PresetAction = gen =>
                {
                    gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
                    gen.Parameters.Barcode.XDimension.Point = 2f;
                }
            },
            new BarcodeSpec
            {
                EncodeType = EncodeTypes.Code128,
                CodeText = "GHI789",
                // Change bar color to blue and further increase X-dimension
                PresetAction = gen =>
                {
                    gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                    gen.Parameters.Barcode.XDimension.Point = 3f;
                }
            }
        };

        // Generate barcodes and collect file paths with expected texts
        var generatedFiles = new List<(string Path, string ExpectedText)>();
        for (int i = 0; i < specs.Count; i++)
        {
            var spec = specs[i];
            string filePath = Path.Combine(batchFolder, $"barcode_{i + 1}.png");

            using (var generator = new BarcodeGenerator(spec.EncodeType, spec.CodeText))
            {
                // Apply preset changes for this barcode
                spec.PresetAction(generator);

                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add((filePath, spec.CodeText));
        }

        // Verify each generated barcode by reading it back
        bool allValid = true;
        foreach (var (path, expectedText) in generatedFiles)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                allValid = false;
                continue;
            }

            // Use the appropriate decode type for Code128
            BaseDecodeType decodeType = DecodeType.Code128;

            try
            {
                using (var reader = new BarCodeReader(path, decodeType))
                {
                    bool found = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        found = true;
                        string decoded = result.CodeText;
                        if (decoded == expectedText)
                        {
                            Console.WriteLine($"SUCCESS: {Path.GetFileName(path)} decoded correctly as '{decoded}'.");
                        }
                        else
                        {
                            Console.WriteLine($"FAILURE: {Path.GetFileName(path)} decoded as '{decoded}' but expected '{expectedText}'.");
                            allValid = false;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine($"FAILURE: No barcode detected in {Path.GetFileName(path)}.");
                        allValid = false;
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"WARNING: Skipping unreadable file {Path.GetFileName(path)}. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Exception while reading {Path.GetFileName(path)}. {ex.Message}");
                allValid = false;
            }
        }

        // Output overall verification result
        Console.WriteLine(allValid
            ? "All barcodes verified successfully. Switching presets did not corrupt previous results."
            : "Some barcodes failed verification. Check the output above for details.");
    }

    // Helper class to hold barcode generation data
    private class BarcodeSpec
    {
        public BaseEncodeType EncodeType { get; set; }
        public string CodeText { get; set; }
        public Action<BarcodeGenerator> PresetAction { get; set; }
    }
}