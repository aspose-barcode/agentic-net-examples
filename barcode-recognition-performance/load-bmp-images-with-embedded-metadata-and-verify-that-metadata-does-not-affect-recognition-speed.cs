using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode recognition from a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads a BMP image, recognizes any barcodes it contains, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image (replace with your actual file)
        string imagePath = "sample.bmp";

        // Verify that the file exists before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the BMP image; Aspose.BarCode ignores embedded metadata during recognition
        using (var bitmap = new Bitmap(imagePath))
        {
            // Start timing the barcode recognition process
            var stopwatch = Stopwatch.StartNew();

            // Initialize the barcode reader for all supported barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Perform the recognition and retrieve all detected barcodes
                var results = reader.ReadBarCodes();

                // Stop the timer now that recognition is complete
                stopwatch.Stop();

                // Output performance and detection summary
                Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Barcodes detected: {results.Length}");

                // Iterate through each detected barcode and display its type and decoded text
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}