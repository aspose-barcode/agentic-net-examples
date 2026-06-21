using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode recognition from a high‑resolution PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads an image, reads all supported barcodes, and prints the results with timing information.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution PNG image
        string imagePath = "barcode.png";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image into a bitmap and create a barcode reader that supports all types
        using (var bitmap = new Bitmap(imagePath))
        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            // Start measuring the time taken for barcode recognition
            var stopwatch = Stopwatch.StartNew();

            // Perform the barcode reading operation
            var results = reader.ReadBarCodes();

            // Stop the timer after reading is complete
            stopwatch.Stop();

            // Iterate through each detected barcode and output its type and decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Output the total elapsed time for the recognition process
            Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}