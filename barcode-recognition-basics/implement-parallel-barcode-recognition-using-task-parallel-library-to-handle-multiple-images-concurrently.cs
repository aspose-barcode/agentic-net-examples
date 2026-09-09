// Title: Parallel Barcode Recognition with TPL
// Description: Demonstrates generating multiple barcode images and recognizing them concurrently using the Task Parallel Library.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and ProcessorSettings for optimizing multithreaded processing. Typical scenarios include batch processing of scanned documents, high‑throughput inventory systems, and automated data capture pipelines where developers need to handle many images efficiently.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: barcode, parallel, tpl, aspose.barcode, generation, recognition, multithreading, c#

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a set of barcode images and decodes them in parallel using TPL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures processor settings, creates sample barcodes, runs parallel recognition,
    /// and cleans up temporary files.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Enable multithreaded barcode processing and configure core usage.
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Create a unique temporary folder for the generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "ParallelBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data: a list of (symbology, text) pairs to encode.
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.DatabarOmniDirectional, "(01)01234567890123"),
            (EncodeTypes.AustraliaPost, "1100000000")
        };

        // Generate barcode images and collect their file paths.
        var barcodeFiles = new List<string>();
        foreach (var (type, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{type}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Set a simple black‑on‑white appearance.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare parallel recognition tasks using TPL.
        var recognitionTasks = new List<Task>();
        foreach (string file in barcodeFiles)
        {
            recognitionTasks.Add(Task.Run(() =>
            {
                // Verify that the file exists before attempting to read.
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    return;
                }

                // Use all supported barcode types for decoding.
                BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    // Enable checksum validation (default is On).
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the actual barcode reading.
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }));
        }

        // Await completion of all recognition tasks.
        await Task.WhenAll(recognitionTasks);

        // Cleanup: delete generated barcode files.
        foreach (string file in barcodeFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Suppress any errors during file deletion.
            }
        }

        // Remove the temporary folder.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors during folder deletion.
        }
    }
}