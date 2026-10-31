// Title: Parallel Barcode Recognition with TPL
// Description: Demonstrates generating multiple barcode images and recognizing them concurrently using Aspose.BarCode and the Task Parallel Library.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create Code128 barcodes, BarCodeReader with QualitySettings for fast decoding, and the Task Parallel Library (TPL) to process several images in parallel. Developers working with bulk barcode scanning, high‑throughput image pipelines, or multi‑core environments will find this pattern useful for maximizing CPU utilization while maintaining simple, readable code.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: barcode, recognition, parallel, tpl, aspose.barcode, code128, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample Code128 barcodes, then reads them concurrently using the Task Parallel Library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, configures thread‑pool and reader settings,
    /// runs parallel recognition tasks, and finally cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Prepare a temporary folder for generated barcode images.
        // -----------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // -----------------------------------------------------------------
        // 2. Generate a set of sample barcode PNG files.
        // -----------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // 3. Tune the .NET ThreadPool to match the number of logical processors.
        // -----------------------------------------------------------------
        int processorCount = Environment.ProcessorCount;
        ThreadPool.SetMinThreads(processorCount, processorCount);
        ThreadPool.SetMaxThreads(processorCount * 2, processorCount * 2);

        // -----------------------------------------------------------------
        // 4. Configure global Aspose.BarCode processor settings for optimal parallelism.
        // -----------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = processorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = processorCount * 2;

        // -----------------------------------------------------------------
        // 5. Run barcode recognition on each image in parallel using TPL.
        // -----------------------------------------------------------------
        List<Task> tasks = new List<Task>();
        foreach (string file in imageFiles)
        {
            tasks.Add(Task.Run(() =>
            {
                // Verify that the file still exists before attempting to read it.
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    return;
                }

                try
                {
                    // Open the image with BarCodeReader and apply a high‑performance quality preset.
                    using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                    {
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Perform the actual barcode detection.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"File: {Path.GetFileName(file)} | Text: {result.CodeText} | Type: {result.CodeTypeName}");
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handle cases where the image cannot be loaded or is invalid.
                    Console.WriteLine($"Failed to load {Path.GetFileName(file)}: {ex.Message}");
                }
            }));
        }

        // Wait for all recognition tasks to complete.
        Task.WaitAll(tasks.ToArray());

        // -----------------------------------------------------------------
        // 6. Optional cleanup of temporary files and directory.
        // -----------------------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any cleanup exceptions (e.g., file in use).
        }
    }
}