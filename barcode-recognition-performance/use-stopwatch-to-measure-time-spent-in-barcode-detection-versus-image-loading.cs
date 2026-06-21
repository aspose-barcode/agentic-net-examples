using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading an image and detecting barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads a bitmap, measures loading time, detects barcodes, and reports results.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "sample.png";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Start timer to measure how long it takes to load the image into a Bitmap object
        Stopwatch loadTimer = Stopwatch.StartNew();

        // Load the image using Aspose.Drawing.Bitmap within a using block to ensure disposal
        using (var bitmap = new Bitmap(imagePath))
        {
            // Stop the loading timer once the bitmap is created
            loadTimer.Stop();
            Console.WriteLine($"Image loading time: {loadTimer.ElapsedMilliseconds} ms");

            // Start timer to measure barcode detection performance
            Stopwatch detectTimer = Stopwatch.StartNew();

            // Initialize the barcode reader for all supported barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Perform barcode detection
                var results = reader.ReadBarCodes();

                // Stop the detection timer after reading is complete
                detectTimer.Stop();

                // Output detection timing and result count
                Console.WriteLine($"Barcode detection time: {detectTimer.ElapsedMilliseconds} ms");
                Console.WriteLine($"Barcodes found: {results.Length}");

                // Iterate through each detected barcode and display its type and decoded text
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}