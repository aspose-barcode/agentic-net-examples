// Title: Parallel Barcode Reading Using Multiple CPU Cores
// Description: Demonstrates how to generate several barcode images and read them concurrently across all processor cores using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings for high‑performance, parallel recognition. Typical scenarios include bulk scanning of documents, automated inventory checks, and large‑scale data extraction where developers need to maximize CPU utilization while handling multiple image files.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode, parallel, multithreading, generation, recognition, aspose.barcode, csharp, cpu‑cores

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of barcode images and reads them in parallel using all available CPU cores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files, configures multithreaded reading,
    /// processes each image concurrently, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // 1. Prepare a unique temporary folder for the generated barcode images.
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ---------------------------------------------------------------
        // 2. Define a collection of barcode specifications (symbology + text).
        // ---------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string text)>
        {
            (EncodeTypes.Code128, "CODE128"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 Sample"),
            (EncodeTypes.Aztec, "Aztec123")
        };

        // ---------------------------------------------------------------
        // 3. Generate barcode images and store their file paths.
        // ---------------------------------------------------------------
        var generatedFiles = new List<string>();
        foreach (var (encodeType, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType.TypeName}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // ---------------------------------------------------------------
        // 4. Enable Aspose.BarCode's internal thread pool to use all cores.
        // ---------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Console.WriteLine("Starting parallel barcode reading...");

        var stopwatch = Stopwatch.StartNew();

        // ---------------------------------------------------------------
        // 5. Read each barcode image in parallel, creating a separate
        //    BarCodeReader instance per file to avoid thread‑safety issues.
        // ---------------------------------------------------------------
        Parallel.ForEach(generatedFiles, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unreadable file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Parallel reading completed in {stopwatch.ElapsedMilliseconds} ms");

        // ---------------------------------------------------------------
        // 6. Clean up temporary files and folder.
        // ---------------------------------------------------------------
        foreach (var file in generatedFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}