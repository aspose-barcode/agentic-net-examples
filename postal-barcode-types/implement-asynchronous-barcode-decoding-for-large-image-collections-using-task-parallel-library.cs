// Title: Asynchronous barcode decoding with Task Parallel Library
// Description: Demonstrates generating sample barcode images and decoding them concurrently using TPL for high‑throughput scenarios.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation and BarCodeReader for recognition. It illustrates typical use cases such as large‑scale image collections, where developers need efficient, asynchronous decoding using core API classes like BarcodeGenerator, BarCodeReader, and QualitySettings.
// Prompt: Implement asynchronous barcode decoding for large image collections using Task Parallel Library.
// Tags: code128, generation, recognition, png, tpl, aspose.barcode, aspose.drawing, barcode decoding, asynchronous processing

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a set of barcode images and decodes them asynchronously
/// using the Task Parallel Library. Demonstrates high‑performance batch processing with
/// Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, decodes them in parallel,
    /// and cleans up temporary resources.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSamples");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Generate a small set of sample barcode images (5 items)
        // --------------------------------------------------------------------
        int sampleCount = 5;
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Auto‑size the barcode image for optimal dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Asynchronously decode all images using TPL
        // --------------------------------------------------------------------
        List<Task> decodeTasks = new List<Task>();
        foreach (string path in imagePaths)
        {
            // Queue each decode operation on the thread pool
            decodeTasks.Add(Task.Run(() => DecodeBarcode(path)));
        }

        // Wait for all decoding tasks to complete
        await Task.WhenAll(decodeTasks);

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        foreach (string path in imagePaths)
        {
            try { File.Delete(path); } catch { /* ignore cleanup errors */ }
        }
        try { Directory.Delete(tempFolder); } catch { /* ignore cleanup errors */ }
    }

    /// <summary>
    /// Decodes a single barcode image and writes the results to the console.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    private static void DecodeBarcode(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use BarCodeReader to read all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply a high‑performance quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                return;
            }

            // Output each detected barcode's type and text
            foreach (var result in results)
            {
                Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
            }
        }
    }
}