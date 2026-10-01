// Title: Barcode Confidence Distribution Report
// Description: This example generates a set of sample barcodes, reads them back using Aspose.BarCode, and summarizes the distribution of confidence levels derived from the reading quality of each scanned barcode.
// Category-Description: Demonstrates combined barcode generation and recognition using Aspose.BarCode for .NET. It showcases the BarcodeGenerator, BarCodeReader, and related classes to create images, decode them, and evaluate ReadingQuality. Developers often need to assess scan reliability, generate confidence metrics, and produce summary reports for large barcode datasets.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode, confidence, distribution, generation, recognition, report, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeConfidenceReport
{
    // Simple confidence levels derived from reading quality
    enum ConfidenceLevel
    {
        High,
        Medium,
        Low
    }

    /// <summary>
    /// Generates sample barcodes, reads them, categorizes confidence based on reading quality,
    /// and outputs a distribution summary.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Executes barcode generation, recognition, confidence categorization,
        /// and prints a summary report.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary folder for sample barcodes
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Define barcode specifications to generate (type and content)
            var specs = new List<(BaseEncodeType EncodeType, string CodeText)>
            {
                (EncodeTypes.Code128, "ABC123456"),
                (EncodeTypes.QR, "https://example.com"),
                (EncodeTypes.DataMatrix, "DataMatrixTest"),
                (EncodeTypes.Pdf417, "PDF417 Sample Text"),
                (EncodeTypes.Aztec, "AztecContent")
            };

            // Generate barcode images and collect file paths
            var barcodeFiles = new List<string>();
            foreach (var spec in specs)
            {
                string filePath = Path.Combine(tempFolder, $"{spec.EncodeType.TypeName}_{Guid.NewGuid().ToString("N")}.png");
                using (var generator = new BarcodeGenerator(spec.EncodeType, spec.CodeText))
                {
                    // Save using default sizing; no explicit dimension settings required
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                barcodeFiles.Add(filePath);
            }

            // Initialize distribution counters for each confidence level
            var distribution = new Dictionary<ConfidenceLevel, int>
            {
                { ConfidenceLevel.High, 0 },
                { ConfidenceLevel.Medium, 0 },
                { ConfidenceLevel.Low, 0 }
            };

            // Prepare a reader that supports all barcode types
            BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;

            // Process each generated barcode file
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    using (var reader = new BarCodeReader(file, decodeAll))
                    {
                        // Read all barcodes present in the image
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (var result in results)
                        {
                            double quality = result.ReadingQuality; // Value range: 0‑100
                            ConfidenceLevel level = CategorizeConfidence(quality);
                            distribution[level]++;

                            Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Quality: {quality:F1} => {level}");
                        }
                    }
                }
                catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                {
                    Console.WriteLine($"Skipping unreadable file: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file}: {ex.Message}");
                }
            }

            // Output the confidence distribution summary
            Console.WriteLine("\n=== Confidence Distribution Summary ===");
            foreach (var kvp in distribution)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Cleanup temporary files and folder (best‑effort)
            try
            {
                foreach (string file in barcodeFiles)
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder);
            }
            catch
            {
                // Ignored – cleanup is non‑critical
            }
        }

        // Helper to map reading quality to a confidence level
        static ConfidenceLevel CategorizeConfidence(double quality)
        {
            if (quality >= 90.0)
                return ConfidenceLevel.High;
            if (quality >= 70.0)
                return ConfidenceLevel.Medium;
            return ConfidenceLevel.Low;
        }
    }
}