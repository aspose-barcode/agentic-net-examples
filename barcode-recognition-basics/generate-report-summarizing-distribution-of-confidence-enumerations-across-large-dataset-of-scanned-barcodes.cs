// Title: Barcode Confidence Distribution Report
// Description: Generates a set of sample barcodes, reads them back, and summarizes the distribution of confidence levels reported by the Aspose.BarCode recognizer.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to work with the BarCodeConfidence enumeration. Developers often need to batch‑process images, evaluate scan quality, and produce statistical reports on recognition confidence.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode symbology, generation, recognition, confidence, report, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate barcodes, read them, and produce a confidence distribution report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample barcodes, reads them, aggregates confidence values,
    /// outputs a report, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and corresponding text)
        var samples = new List<(BaseEncodeType encodeType, string codeText)>
        {
            (EncodeTypes.Code128, "Sample001"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AztecTest"),
            (EncodeTypes.Pdf417, "PDF417Sample")
        };

        // Store full paths of generated image files
        var generatedFiles = new List<string>();

        // Generate barcode images and save them as PNG files
        foreach (var (encodeType, codeText) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Initialise distribution dictionary for confidence levels
        var distribution = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // Set decode type to support all available symbologies
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Read each generated barcode image and tally confidence values
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        BarCodeConfidence confidence = result.Confidence;
                        if (distribution.ContainsKey(confidence))
                        {
                            distribution[confidence]++;
                        }
                        else
                        {
                            distribution[confidence] = 1;
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
        }

        // Output the confidence distribution report to the console
        Console.WriteLine("Barcode Confidence Distribution Report");
        Console.WriteLine("-------------------------------------");
        foreach (var kvp in distribution)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Attempt to delete the temporary folder and its contents
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