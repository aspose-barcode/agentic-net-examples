// Title: Asynchronous Barcode Decoding for Large Image Collections
// Description: Demonstrates generating sample barcode images and decoding them concurrently using Aspose.BarCode with the Task Parallel Library.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, combined with .NET's Task Parallel Library to process large image sets efficiently. Developers often need to batch‑process images for inventory, logistics, or document management scenarios, requiring high‑performance, multithreaded barcode handling.
// Prompt: Implement asynchronous barcode decoding for large image collections using Task Parallel Library.
// Tags: barcode, code128, async, task parallel library, aspose.barcode, generation, recognition, highperformance

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous decoding of barcodes in a batch of images using Aspose.BarCode and TPL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them concurrently, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder, 5);

        // Decode the barcodes asynchronously using TPL
        Task decodeTask = DecodeBarcodesAsync(barcodeFiles);
        decodeTask.Wait();

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a set of barcode images using the Code128 symbology.
    /// </summary>
    /// <param name="folder">Folder where images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>List of file paths for the generated images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string text = $"Sample{i + 1}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Optional: set basic text appearance parameters
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Asynchronously decodes a collection of barcode image files using multiple threads.
    /// </summary>
    /// <param name="files">List of image file paths to decode.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task DecodeBarcodesAsync(List<string> files)
    {
        // Enable multithreading globally for all readers
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        var decodeTasks = new List<Task>();

        // Schedule a decoding task for each file
        foreach (string file in files)
        {
            decodeTasks.Add(Task.Run(() =>
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    return;
                }

                try
                {
                    // Initialize the reader for all supported barcode types
                    using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                    {
                        // Use a high‑performance quality preset to speed up processing
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Read all barcodes present in the image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Output each detected barcode
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }

                        // Inform if no barcode was found
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error processing file {Path.GetFileName(file)}: {ex.Message}");
                }
            }));
        }

        // Await completion of all decoding tasks
        await Task.WhenAll(decodeTasks);
    }
}