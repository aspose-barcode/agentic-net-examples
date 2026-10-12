// Title: Measure Barcode Detection vs Image Loading Time with Stopwatch
// Description: Demonstrates how to generate a barcode image, load it, and use a Stopwatch to time the loading and detection phases.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for detecting them, illustrating typical performance‑measurement scenarios where developers need to benchmark image loading versus barcode detection using the Aspose.BarCode API.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, detection, performance, stopwatch, aspose.barcode, generation, recognition, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode image creation, loading, and detection while measuring performance with Stopwatch.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, measures loading and detection times, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Start timing the image loading operation
        Stopwatch loadWatch = Stopwatch.StartNew();

        // Initialize the barcode reader for the generated image and specific symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Stop the loading timer once the reader is instantiated
            loadWatch.Stop();
            Console.WriteLine($"Image loading time: {loadWatch.ElapsedMilliseconds} ms");

            // Start timing the barcode detection operation
            Stopwatch detectWatch = Stopwatch.StartNew();

            // Perform barcode detection
            reader.ReadBarCodes();

            // Stop the detection timer after reading is complete
            detectWatch.Stop();
            Console.WriteLine($"Barcode detection time: {detectWatch.ElapsedMilliseconds} ms");

            // Output the number of barcodes found and their details
            Console.WriteLine($"Barcodes found: {reader.FoundCount}");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}