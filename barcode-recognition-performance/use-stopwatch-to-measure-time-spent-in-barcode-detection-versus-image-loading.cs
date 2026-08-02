// Title: Measure Barcode Detection vs Image Loading Time
// Description: Demonstrates how to use Stopwatch to compare the time required to load a barcode image and to detect the barcode within it.
// Category-Description: This example belongs to the Aspose.BarCode performance measurement category, illustrating the use of BarcodeGenerator, BarCodeReader, and System.Diagnostics.Stopwatch. Developers often need to benchmark image loading versus barcode recognition to optimize processing pipelines in scanning applications, inventory systems, and mobile capture scenarios.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, detection, performance, stopwatch, aspose.barcode, generation, recognition, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image (if missing), measures the time to load the image,
/// and measures the time to detect the barcode using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image.
        string imagePath = "sample.png";

        // Generate a sample barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------
        // Measure image loading time.
        // ------------------------------
        var loadStopwatch = new Stopwatch();
        loadStopwatch.Start();

        // Load the image into a Bitmap object.
        using (var bitmap = new Bitmap(imagePath))
        {
            loadStopwatch.Stop();
            Console.WriteLine($"Image loading time: {loadStopwatch.Elapsed.TotalMilliseconds} ms");
        }

        // ------------------------------
        // Measure barcode detection time.
        // ------------------------------
        var detectStopwatch = new Stopwatch();

        // Load the image again for barcode detection.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Initialize the barcode reader for all supported symbologies.
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                detectStopwatch.Start();

                // Perform barcode detection.
                var results = reader.ReadBarCodes();

                detectStopwatch.Stop();
                Console.WriteLine($"Barcode detection time: {detectStopwatch.Elapsed.TotalMilliseconds} ms");

                // Output detection results.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                }
            }
        }
    }
}