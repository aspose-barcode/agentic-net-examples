// Title: Measure barcode detection vs image loading time using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, load the image from disk, and use a Stopwatch to compare the time spent loading the image versus detecting the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include performance benchmarking, batch processing, and validation of barcode images. Developers often need to measure loading and detection times to optimize workflows, and this snippet provides a concise reference.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, detection, image loading, performance, stopwatch, aspose.barcode, generation, recognition, code128, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates measuring the time spent loading a barcode image and detecting the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, measures load and detection times, and outputs results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and barcode image file path
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // --------------------------------------------------------------------
        // Generate a sample Code128 barcode image and save it to disk
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Measure the time required to load the image bytes from the file system
        // --------------------------------------------------------------------
        Stopwatch loadWatch = Stopwatch.StartNew();
        byte[] imageData = File.ReadAllBytes(barcodePath);
        loadWatch.Stop();
        Console.WriteLine($"Image loading time: {loadWatch.ElapsedMilliseconds} ms");

        // --------------------------------------------------------------------
        // Measure the time required to detect the barcode within the loaded image
        // --------------------------------------------------------------------
        using (var ms = new MemoryStream(imageData))
        {
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                Stopwatch detectWatch = Stopwatch.StartNew();
                reader.ReadBarCodes();
                detectWatch.Stop();

                Console.WriteLine($"Barcodes read: {reader.FoundCount}, Detection time: {detectWatch.ElapsedMilliseconds} ms");

                // Output each detected barcode's type and text
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directory
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}