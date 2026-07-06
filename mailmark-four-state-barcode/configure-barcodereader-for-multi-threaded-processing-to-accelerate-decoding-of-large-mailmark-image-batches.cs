// Title: Multi‑Threaded Mailmark Barcode Decoding Example
// Description: Demonstrates how to generate a batch of Mailmark barcode images and decode them concurrently using BarCodeReader to speed up processing of large image sets.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on high‑performance multi‑threaded reading of complex barcodes such as Mailmark. It showcases the use of BarCodeReader, QualitySettings, and ProcessorSettings classes to leverage all CPU cores, a common requirement for developers processing bulk barcode images in mail and logistics applications.
// Prompt: Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mailmark image batches.
// Tags: mailmark, barcode, decoding, multithreading, barcodereader, complexbarcode, generation, qualitysettings, processorsettings

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Entry point for the multi‑threaded Mailmark barcode decoding example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample Mailmark barcode images and decodes them in parallel using BarCodeReader.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated sample images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "MailmarkSamples");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Generate a small batch of Mailmark barcode images (5 samples)
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            // Prepare Mailmark codetext with varying ItemID
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // Premium (default)
                VersionID = 1,
                Class = "0",                    // Null/Test
                SupplychainID = 384224,
                ItemID = 16563762 + i,          // Vary ItemID for each sample
                DestinationPostCodePlusDPS = "EF61AH8T " // Known valid value
            };

            // Create a ComplexBarcodeGenerator using the Mailmark codetext
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode image as PNG
                string filePath = Path.Combine(outputDir, $"Mailmark_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                imagePaths.Add(filePath);
            }
        }

        // Configure BarCodeReader to use all available processor cores for each call
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Set up parallel processing options (max degree equals processor count)
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Process the batch of images in parallel
        Parallel.ForEach(imagePaths, parallelOptions, imagePath =>
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Initialize BarCodeReader for Mailmark decode type
            using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
            {
                // Apply high‑performance quality settings for faster decoding
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"  Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                }
            }
        });
    }
}