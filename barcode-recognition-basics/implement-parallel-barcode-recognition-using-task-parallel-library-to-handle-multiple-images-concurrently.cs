// Title: Parallel Barcode Recognition with TPL
// Description: Generates sample Code128 barcodes, then uses Task Parallel Library to recognize them concurrently, demonstrating multi‑core processing.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: code128, generation, recognition, parallel, tpl, console

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images and recognizing them in parallel using the Task Parallel Library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, processes them concurrently, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Generate a small set of sample barcode images (5 items)
        // --------------------------------------------------------------------
        var imagePaths = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            // Create unique barcode text for each image
            string codeText = $"CODE{i + 1}";
            // Determine a temporary file path for the image
            string filePath = Path.Combine(Path.GetTempPath(), $"barcode_{i}.png");

            // Generate and save the barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Store the path for later processing
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 2. Configure the barcode reader to utilize all available CPU cores
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // 3. Perform parallel recognition of the generated images
        // --------------------------------------------------------------------
        Parallel.ForEach(imagePaths, path =>
        {
            // Open a reader for the current image file
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Iterate over all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Synchronize console output to avoid interleaved lines from multiple threads
                    lock (Console.Out)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(path)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        });

        // --------------------------------------------------------------------
        // 4. Clean up temporary files
        // --------------------------------------------------------------------
        foreach (var path in imagePaths)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                // Ignore any cleanup errors (e.g., file in use)
            }
        }
    }
}