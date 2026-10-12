// Title: Barcode Recognition Performance Metrics Logging
// Description: Demonstrates generating a Code128 barcode, recognizing it, and logging processing time and found count for performance monitoring.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Developers commonly use these APIs for batch processing, quality assurance, and performance benchmarking of barcode workflows in .NET applications.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: barcode symbology, generation, recognition, performance, metrics, aspose.barcode, code128, .net

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a barcode, reads it back, and logs recognition metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image, measures recognition time,
    /// logs processing metrics, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Measure recognition time and log metrics
        var stopwatch = new Stopwatch();
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            stopwatch.Start();
            try
            {
                // Perform barcode recognition
                reader.ReadBarCodes();
            }
            catch (RecognitionAbortedException ex)
            {
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
            stopwatch.Stop();

            // Output performance metrics
            int foundCount = reader.FoundCount;
            Console.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Found count: {foundCount}");

            // List details of each recognized barcode
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo
        }
    }
}