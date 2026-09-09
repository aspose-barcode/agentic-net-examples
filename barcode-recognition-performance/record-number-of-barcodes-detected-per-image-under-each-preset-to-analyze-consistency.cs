// Title: Barcode detection count per image across quality presets
// Description: Demonstrates generating several barcode images, then recognizing them using different quality settings and recording how many barcodes are detected per image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings presets to control recognition performance versus accuracy. Developers often need to evaluate how different presets affect detection consistency across multiple images, especially when optimizing for speed or quality in batch processing scenarios.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode, generation, recognition, qualitysettings, count, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images, recognizing them with various quality presets,
/// and recording the number of barcodes detected per image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, runs recognition under different presets,
    /// outputs detection counts, and cleans up temporary files.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode specifications: type, text, and output file name
        var barcodeSpecs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AztecTest", "aztec.png")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encodeType, spec.codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Define recognition quality presets to be tested
        var presets = new List<(string name, QualitySettings preset)>
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighQuality", QualitySettings.HighQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        // Decode types covering all generated barcodes
        BaseDecodeType[] decodeTypes = new BaseDecodeType[]
        {
            DecodeType.Code128,
            DecodeType.QR,
            DecodeType.DataMatrix,
            DecodeType.Aztec
        };

        // Store results: preset name -> (image file name -> detected barcode count)
        var results = new Dictionary<string, Dictionary<string, int>>();

        // Iterate over each quality preset and count detected barcodes per image
        foreach (var presetInfo in presets)
        {
            var perImageCounts = new Dictionary<string, int>();
            foreach (var file in generatedFiles)
            {
                int count = 0;
                try
                {
                    using (var reader = new BarCodeReader(file, decodeTypes))
                    {
                        reader.QualitySettings = presetInfo.preset;
                        count = reader.ReadBarCodes().Length;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Warning: could not read file {Path.GetFileName(file)}: {ex.Message}");
                }
                perImageCounts[Path.GetFileName(file)] = count;
            }
            results[presetInfo.name] = perImageCounts;
        }

        // Output the recorded counts for each preset and image
        foreach (var presetEntry in results)
        {
            Console.WriteLine($"Preset: {presetEntry.Key}");
            foreach (var imgEntry in presetEntry.Value)
            {
                Console.WriteLine($"  Image: {imgEntry.Key}, Barcodes detected: {imgEntry.Value}");
            }
        }

        // Cleanup temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}