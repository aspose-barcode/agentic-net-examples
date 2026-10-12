// Title: Parallel Barcode Reading and Aggregation Example
// Description: Demonstrates generating several barcode images, reading them concurrently using parallel processing, and aggregating the results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with DecodeType.AllSupportedTypes to recognize them, and parallel constructs (Parallel.ForEach, ConcurrentBag) to process multiple images efficiently. Developers often need to batch‑process large numbers of barcode images for inventory, logistics, or document automation scenarios.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: barcode, symbology, parallel, recognition, aggregation, generation, aspose.barcode, decode, encode

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel barcode generation, recognition, and result aggregation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, reads them in parallel,
    /// aggregates the recognition results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Prepare sample data: list of (symbology, text, file name)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Code128Sample", "code128.png"),
            (EncodeTypes.QR, "QRSample", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417Sample", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecSample", "aztec.png")
        };

        // --------------------------------------------------------------------
        // Generate barcode images and collect their file paths
        // --------------------------------------------------------------------
        var generatedFiles = new List<string>();
        foreach (var (encode, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Enable multithreaded recognition (optional performance tuning)
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // --------------------------------------------------------------------
        // Thread‑safe collection for aggregated results
        // --------------------------------------------------------------------
        var resultsBag = new ConcurrentBag<(string file, string type, string text)>();

        // --------------------------------------------------------------------
        // Parallel processing of barcode images
        // --------------------------------------------------------------------
        Parallel.ForEach(generatedFiles, filePath =>
        {
            if (!File.Exists(filePath))
                return;

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes in the image
                    BarCodeResult[] readResults = reader.ReadBarCodes();

                    foreach (var result in readResults)
                    {
                        resultsBag.Add((Path.GetFileName(filePath), result.CodeTypeName, result.CodeText));
                    }
                }
            }
            catch (ArgumentException)
            {
                // Image loading failed – skip this file
                Console.WriteLine($"Warning: Unable to load image '{filePath}'. Skipping.");
            }
        });

        // --------------------------------------------------------------------
        // Output aggregated results
        // --------------------------------------------------------------------
        Console.WriteLine("Aggregated barcode recognition results:");
        foreach (var (file, type, text) in resultsBag)
        {
            Console.WriteLine($"File: {file} | Type: {type} | Text: {text}");
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (var file in generatedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}