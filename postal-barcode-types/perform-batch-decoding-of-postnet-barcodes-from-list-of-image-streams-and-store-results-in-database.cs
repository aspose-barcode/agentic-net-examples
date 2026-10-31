// Title: Batch decode of Postnet barcodes and store results in JSON (as DB placeholder)
// Description: Demonstrates generating multiple Postnet barcode images, decoding them in a batch, and persisting the decoded data. Useful for processing large sets of postal barcodes in automated workflows.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Postnet symbols, BarCodeReader for batch decoding, and common result handling patterns. Developers working with postal automation, logistics, or bulk barcode processing often need to generate test data, decode many images efficiently, and store outcomes in databases or files.
// Prompt: Perform batch decoding of Postnet barcodes from a list of image streams and store results in a database.
// Tags: postnet, barcode, batch decoding, json, aspose.barcode, generation, recognition, database placeholder

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BatchPostnetDecode
{
    /// <summary>
    /// Demonstrates batch generation and decoding of Postnet barcodes, storing results in a JSON file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates sample barcodes, decodes them, and writes results to a JSON file.
        /// </summary>
        static void Main()
        {
            // Create a dedicated temporary folder for the sample
            string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPostnet_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Generate sample Postnet barcode images and collect their file paths
            List<string> barcodeFiles = new List<string>();
            GenerateSampleBarcodes(tempFolder, barcodeFiles);

            // Decode the generated barcodes in a batch operation
            List<DecodeResult> results = DecodeBarcodes(barcodeFiles);

            // Store results in a JSON file (as a stand‑in for a database)
            string jsonPath = Path.Combine(tempFolder, "decode_results.json");
            string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Decoded {results.Count} barcodes. Results saved to: {jsonPath}");
        }

        /// <summary>
        /// Generates Postnet barcode images for a set of sample codes and records their file paths.
        /// </summary>
        /// <param name="folder">Folder where images will be saved.</param>
        /// <param name="fileList">List that will receive the generated file paths.</param>
        static void GenerateSampleBarcodes(string folder, List<string> fileList)
        {
            // Sample Postnet code texts (5, 9, or 11 digits are typical)
            string[] sampleCodes = new[] { "12345", "123456789", "12345678901", "98765", "54321" };

            foreach (string code in sampleCodes)
            {
                string filePath = Path.Combine(folder, $"Postnet_{code}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
                {
                    // Optional visual settings
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                    // Save the barcode image to PNG format
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                fileList.Add(filePath);
            }
        }

        /// <summary>
        /// Decodes a collection of barcode image files using Postnet settings.
        /// </summary>
        /// <param name="files">List of image file paths to decode.</param>
        /// <returns>List of decoding results.</returns>
        static List<DecodeResult> DecodeBarcodes(List<string> files)
        {
            List<DecodeResult> results = new List<DecodeResult>();
            BaseDecodeType decodeType = DecodeType.Postnet;

            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    // Initialize the reader for Postnet decoding
                    using (var reader = new BarCodeReader(file, decodeType))
                    {
                        // Optional: improve performance by limiting to Postnet only
                        // reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                        // Read all barcodes found in the image
                        BarCodeResult[] barResults = reader.ReadBarCodes();
                        foreach (BarCodeResult result in barResults)
                        {
                            // Capture relevant information for each decoded barcode
                            results.Add(new DecodeResult
                            {
                                FileName = Path.GetFileName(file),
                                CodeText = result.CodeText,
                                CodeTypeName = result.CodeTypeName,
                                ReadingQuality = result.ReadingQuality
                            });
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed or unsupported format – skip this file
                    Console.WriteLine($"Skipping file due to error: {file}. Message: {ex.Message}");
                }
            }

            return results;
        }

        // Simple class to hold decoding information for each barcode
        class DecodeResult
        {
            public string FileName { get; set; }
            public string CodeText { get; set; }
            public string CodeTypeName { get; set; }
            public double ReadingQuality { get; set; }
        }
    }
}