// Title: Asynchronous barcode decoding for multiple images using TPL
// Description: Demonstrates generating several barcode images and decoding them concurrently with the Task Parallel Library for high‑performance processing.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as ProcessorSettings and QualitySettings. Typical scenarios include batch processing of scanned documents, inventory management, and large‑scale image analysis where developers need to decode many barcodes efficiently using multithreading.
// Prompt: Implement asynchronous barcode decoding for large image collections using Task Parallel Library.
// Tags: barcode, generation, recognition, async, taskparallel, multithreading, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous decoding of a batch of barcode images using Aspose.BarCode and the Task Parallel Library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, configures multithreading, and decodes them concurrently.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a list to hold file paths of generated barcodes
        var files = new List<string>();

        // Define the set of barcode symbologies to generate
        var symbologies = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix,
            EncodeTypes.Pdf417,
            EncodeTypes.Aztec
        };

        // Generate sample barcode images and store their paths
        for (int i = 0; i < symbologies.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(symbologies[i], $"Sample{i + 1}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // Configure global multithreading settings for the barcode reader
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Asynchronously decode all generated barcodes using TPL
        var decodeTasks = new List<Task>();
        foreach (string file in files)
        {
            decodeTasks.Add(Task.Run(() => DecodeFile(file)));
        }

        // Wait for all decoding tasks to complete
        await Task.WhenAll(decodeTasks);
    }

    /// <summary>
    /// Decodes a single barcode image and writes the results to the console.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    static void DecodeFile(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the barcode reader for all supported types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Set a high‑performance quality preset to speed up processing
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                }
                else
                {
                    // Output each detected barcode's type and text
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Handle cases where the image cannot be loaded or is invalid
            Console.WriteLine($"Failed to load image {Path.GetFileName(imagePath)}: {ex.Message}");
        }
    }
}