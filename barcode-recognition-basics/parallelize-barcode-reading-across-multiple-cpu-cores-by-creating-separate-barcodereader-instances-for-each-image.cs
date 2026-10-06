// Title: Parallel Barcode Reading Using Multiple CPU Cores
// Description: Demonstrates generating several barcode images and reading them concurrently with Aspose.BarCode, leveraging all processor cores for faster recognition.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to combine the BarcodeGenerator and BarCodeReader classes with ProcessorSettings for high‑throughput scenarios. Typical use cases include scanning large image collections, automated inventory checks, and real‑time data capture where developers need to maximize CPU utilization while maintaining thread‑safe barcode recognition.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode, generation, recognition, parallel, multithreading, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a set of barcode images and reads them in parallel
/// using all available CPU cores. It demonstrates the use of Aspose.BarCode's
/// <c>BarcodeGenerator</c>, <c>BarCodeReader</c>, and <c>ProcessorSettings</c> for
/// high‑performance batch recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, configures
    /// multithreaded recognition, processes the images in parallel, and cleans up
    /// temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a dedicated temporary folder for generated barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------
        // 2. Define a collection of sample barcodes to be generated
        // --------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Interleaved2of5, "1234567890", "itf.png")
        };

        var generatedFiles = new List<string>();

        // --------------------------------------------------------------
        // 3. Generate barcode images and store their file paths
        // --------------------------------------------------------------
        foreach (var (encodeType, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // --------------------------------------------------------------
        // 4. Enable multithreaded recognition using all available cores
        // --------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        var stopwatch = Stopwatch.StartNew();

        // --------------------------------------------------------------
        // 5. Parallel reading of barcodes – one BarCodeReader per image
        // --------------------------------------------------------------
        Parallel.ForEach(generatedFiles, filePath =>
        {
            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)} => {result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Image could not be loaded – skip and continue processing other files
                Console.WriteLine($"Skipping unreadable file: {filePath}");
            }
            catch (Exception ex)
            {
                // Log unexpected errors without terminating the parallel loop
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Total recognition time: {stopwatch.ElapsedMilliseconds} ms");

        // --------------------------------------------------------------
        // 6. Cleanup temporary files and folder
        // --------------------------------------------------------------
        try
        {
            foreach (var file in generatedFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}