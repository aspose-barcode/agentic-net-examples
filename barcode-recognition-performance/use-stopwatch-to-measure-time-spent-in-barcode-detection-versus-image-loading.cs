// Title: Measure barcode detection vs image loading time using Stopwatch
// Description: Demonstrates generating a Code128 barcode, loading the image, and measuring the time spent on image loading and barcode detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them. Typical scenarios include performance benchmarking of barcode processing pipelines, where developers need to assess loading and detection overhead using classes like Stopwatch, MemoryStream, and DecodeType.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, barcode generation, barcode detection, performance measurement, stopwatch, aspose.barcode, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring the time taken to load a barcode image and detect the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, measures image loading and detection times, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before proceeding
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Measure image loading time (reading file into a MemoryStream)
        Stopwatch loadTimer = new Stopwatch();
        loadTimer.Start();
        byte[] imageBytes = File.ReadAllBytes(barcodePath);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            loadTimer.Stop();
            Console.WriteLine($"Image loading time: {loadTimer.Elapsed.TotalMilliseconds} ms");

            // Measure barcode detection time
            Stopwatch detectTimer = new Stopwatch();
            detectTimer.Start();
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected code: {result.CodeText}");
                }
            }
            detectTimer.Stop();
            Console.WriteLine($"Barcode detection time: {detectTimer.Elapsed.TotalMilliseconds} ms");
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect the demo
        }
    }
}