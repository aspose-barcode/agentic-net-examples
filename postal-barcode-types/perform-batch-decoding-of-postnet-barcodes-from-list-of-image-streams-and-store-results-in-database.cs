// Title: Batch decode Postnet barcodes from image streams and save results to JSON
// Description: Demonstrates generating multiple Postnet barcode images, decoding them in a batch, and persisting the results as JSON (simulating a database).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Postnet barcodes, BarCodeReader for batch decoding, and common result handling patterns. Developers working with postal barcode automation, bulk scanning, or data extraction will find these APIs useful for integrating barcode workflows into .NET applications.
// Prompt: Perform batch decoding of Postnet barcodes from a list of image streams and store results in a database.
// Tags: postnet, barcode, batch decoding, json, aspose.barcode, barcodegeneration, barcoderecognition, .net

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and decoding of Postnet barcodes, storing results in a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Postnet barcodes, decodes them, and writes results to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPostnet_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Postnet code texts (5‑digit ZIP codes)
        var codeTexts = new List<string> { "12345", "67890", "01234", "56789", "24680" };
        var imagePaths = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images for each sample text
        // -----------------------------------------------------------------
        foreach (var text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"Postnet_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, text))
            {
                // Optional: adjust barcode dimensions
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Prepare a list to hold decoding results
        var results = new List<DecodeRecord>();

        // -----------------------------------------------------------------
        // Decode each generated image using the Postnet decoder
        // -----------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Postnet;
        foreach (var path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(path, decodeType))
                {
                    BarCodeResult[] barCodes = reader.ReadBarCodes();

                    // Process each detected barcode
                    foreach (var result in barCodes)
                    {
                        var record = new DecodeRecord
                        {
                            FileName = Path.GetFileName(path),
                            CodeText = result.CodeText,
                            CodeTypeName = result.CodeTypeName
                        };
                        results.Add(record);
                        Console.WriteLine($"Decoded {record.FileName}: {record.CodeTypeName} -> {record.CodeText}");
                    }

                    // Record an empty result if no barcode was found
                    if (barCodes.Length == 0)
                    {
                        var record = new DecodeRecord
                        {
                            FileName = Path.GetFileName(path),
                            CodeText = string.Empty,
                            CodeTypeName = "None"
                        };
                        results.Add(record);
                        Console.WriteLine($"No barcode detected in {record.FileName}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image loading failed or format unsupported
                Console.WriteLine($"Skipping file due to error: {path}. Message: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Serialize results to JSON (simulating a database write)
        // -----------------------------------------------------------------
        string jsonPath = Path.Combine(tempFolder, "PostnetDecodeResults.json");
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Decoding results saved to: {jsonPath}");

        // Cleanup: (optional) keep files for inspection; comment out if not needed
        // Directory.Delete(tempFolder, true);
    }

    // Simple DTO for storing decode information
    class DecodeRecord
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
    }
}