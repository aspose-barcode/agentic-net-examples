// Title: Benchmark Recognition of Stacked vs Single Linear Barcodes
// Description: Generates a single Code128 barcode and a stacked DataBar barcode, then measures the elapsed time required to recognize each image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings to decode them. Typical scenarios include performance testing, scalability analysis, and benchmarking of different symbologies. Developers often need to evaluate recognition speed for single‑line versus stacked linear barcodes in high‑throughput applications.
// Prompt: Benchmark recognition of stacked linear barcodes versus single barcodes to evaluate algorithm scalability.
// Tags: barcode symbology, recognition, performance, benchmark, stacked, linear, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates benchmarking the recognition speed of single‑line and stacked linear barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, runs recognition benchmarks, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBenchmark_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test data: each tuple contains the encode type, decode type, text to encode, and output file name
        var testItems = new (BaseEncodeType encodeType, BaseDecodeType decodeType, string codeText, string fileName)[]
        {
            // Single linear barcode (Code128)
            (EncodeTypes.Code128, DecodeType.Code128, "CODE128TEST", "code128.png"),
            // Stacked linear barcode (DataBar Stacked)
            (EncodeTypes.DatabarStacked, DecodeType.DatabarStacked, "(01)12345678901231", "databarstacked.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images for each test item
        // -----------------------------------------------------------------
        foreach (var item in testItems)
        {
            string filePath = Path.Combine(tempFolder, item.fileName);
            using (var generator = new BarcodeGenerator(item.encodeType, item.codeText))
            {
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Benchmark recognition of each generated barcode
        // -----------------------------------------------------------------
        Console.WriteLine("Barcode Recognition Benchmark (elapsed milliseconds):");
        foreach (var item in testItems)
        {
            string filePath = Path.Combine(tempFolder, item.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"{item.fileName}: file not found.");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, item.decodeType))
            {
                // Use a high‑performance preset to focus on speed
                reader.QualitySettings = QualitySettings.HighPerformance;

                Stopwatch sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();

                Console.Write($"{item.fileName}: ");
                if (results.Length > 0)
                {
                    Console.WriteLine($"{sw.ElapsedMilliseconds} ms (detected {results.Length} barcode(s))");
                }
                else
                {
                    Console.WriteLine($"{sw.ElapsedMilliseconds} ms (no barcode detected)");
                }
            }
        }

        // -----------------------------------------------------------------
        // Clean up temporary files and folder
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – temporary files will be cleaned by the OS later
        }
    }
}