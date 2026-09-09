// Title: Multithreaded Barcode Scanning with Aspose.BarCode
// Description: Demonstrates scanning multiple barcode images in parallel using Aspose.BarCode's default ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarCodeReader, DecodeType, and ProcessorSettings to read various symbologies from image files concurrently. Developers often need to process large batches of barcode images efficiently; parallel processing with the default settings provides a simple yet performant solution.
// Prompt: Create a multithreaded barcode scanner that processes image files in parallel using default ProcessorSettings.
// Tags: barcode, recognition, multithreading, parallel, aspose.barcode, processorsettings, symbology

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates several barcode images and scans them concurrently.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, scans them in parallel, and reports the results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data: symbology, text, and output file name.
        var samples = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AztecText", "aztec.png")
        };

        var files = new List<string>();

        // Generate barcode images and collect their file paths.
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // Measure the time taken to process all images in parallel.
        var stopwatch = Stopwatch.StartNew();

        // Scan each image concurrently using the default ProcessorSettings.
        Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, file =>
        {
            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Processed {files.Count} images in {stopwatch.ElapsedMilliseconds} ms.");

        // Clean up temporary files and folder.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., file locks).
        }
    }
}