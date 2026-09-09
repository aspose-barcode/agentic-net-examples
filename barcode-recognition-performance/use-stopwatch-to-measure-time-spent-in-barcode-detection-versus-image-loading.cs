// Title: Measure Barcode Detection vs Image Loading Time with Stopwatch
// Description: Demonstrates generating a barcode image, loading it from disk, and measuring the time spent on image loading versus barcode detection using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical use cases include performance profiling of barcode processing pipelines, where developers need to assess loading and detection overhead. The key API classes demonstrated are BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and BarCodeImageFormat.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, detection, generation, performance, stopwatch, aspose.barcode, code128, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode, loads it from disk,
/// and measures the time taken for image loading and barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the generated barcode.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ---------------------------------------------------------------
        // Generate a Code128 barcode image and save it as PNG.
        // ---------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ---------------------------------------------------------------
        // Measure the time required to load the image bytes from disk.
        // ---------------------------------------------------------------
        Stopwatch loadWatch = Stopwatch.StartNew();
        byte[] imageBytes = File.ReadAllBytes(barcodePath);
        loadWatch.Stop();
        Console.WriteLine($"Image loading time: {loadWatch.ElapsedMilliseconds} ms");

        // ---------------------------------------------------------------
        // Measure the time required to detect the barcode within the image.
        // ---------------------------------------------------------------
        using (var ms = new MemoryStream(imageBytes))
        {
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                Stopwatch detectWatch = Stopwatch.StartNew();
                reader.ReadBarCodes();
                detectWatch.Stop();

                Console.WriteLine($"Barcode detection time: {detectWatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Barcodes found: {reader.FoundCount}");

                // Output each detected barcode's type and text.
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // ---------------------------------------------------------------
        // Clean up temporary files and directories.
        // ---------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}