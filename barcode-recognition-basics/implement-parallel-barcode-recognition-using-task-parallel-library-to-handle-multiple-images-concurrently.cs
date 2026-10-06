// Title: Parallel barcode recognition using Task Parallel Library
// Description: Demonstrates generating multiple barcode images and decoding them concurrently with TPL for improved throughput.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and related settings for bulk barcode operations. Typical use cases include batch scanning, high‑volume image processing, and performance‑optimized recognition in server‑side applications. Developers often need to generate barcodes, configure processor settings, and run parallel reads to maximize CPU utilization.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: barcode generation, barcode recognition, parallel processing, tpl, code128, qr, datamatrix, aztec, pdf417, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel barcode generation and recognition using Aspose.BarCode and TPL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, configures threading, and decodes them in parallel.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodesParallel_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        var barcodeFiles = new List<string>();
        var samples = new List<(BaseEncodeType encode, string text, string name)>
        {
            (EncodeTypes.Code128, "Sample001", "Code128"),
            (EncodeTypes.QR, "Sample002", "QR"),
            (EncodeTypes.DataMatrix, "Sample003", "DataMatrix"),
            (EncodeTypes.Aztec, "Sample004", "Aztec"),
            (EncodeTypes.Pdf417, "Sample005", "Pdf417")
        };

        foreach (var (encode, text, name) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{name}.png");
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure ThreadPool for multithreaded reading
        ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
        ThreadPool.SetMaxThreads(Environment.ProcessorCount * 2, Environment.ProcessorCount * 2);

        // Configure global processor settings for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Decode type for reading (all supported types)
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Process images in parallel using TPL
        var tasks = new List<Task>();
        foreach (string file in barcodeFiles)
        {
            tasks.Add(Task.Run(() =>
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    return;
                }

                try
                {
                    using (var reader = new BarCodeReader(file, decodeType))
                    {
                        // Optional: set a quality preset for faster processing
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Iterate through all detected barcodes in the image
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | Text: {result.CodeText} | Type: {result.CodeTypeName} | Quality: {result.ReadingQuality}");
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                }
            }));
        }

        // Wait for all parallel tasks to complete
        Task.WaitAll(tasks.ToArray());

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}