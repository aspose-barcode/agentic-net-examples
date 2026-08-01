// Title: Parallel Barcode Reading Using Multiple Cores
// Description: Demonstrates how to read barcodes from multiple images concurrently by creating separate BarCodeReader instances per image.
// Category-Description: This example belongs to the Aspose.BarCode reading operations category. It showcases the use of BarCodeReader, ProcessorSettings, and parallel programming (Parallel.ForEach) to efficiently decode barcodes across many files. Developers often need to process large batches of images quickly, and this pattern illustrates typical usage for high‑throughput barcode recognition in .NET applications.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode symbology, barcode reading, parallel processing, multithreading, aspose.barcode, code128, png, decode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates parallel barcode reading across multiple CPU cores using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes and reads them in parallel.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Generate a set of sample barcode images (5 PNG files)
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{i}.png");
            string codeText = $"Sample{i:D3}";

            // Create a Code128 barcode and save it directly as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Configure the barcode processor to utilize all available CPU cores
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Perform parallel barcode reading – each thread gets its own reader
        // --------------------------------------------------------------------
        Parallel.ForEach(imageFiles, file =>
        {
            // Instantiate a BarCodeReader for the current image file
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Ensure console output from multiple threads does not interleave
                    lock (Console.Out)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Code Text: {result.CodeText}");
                    }
                }
            }
        });
    }
}