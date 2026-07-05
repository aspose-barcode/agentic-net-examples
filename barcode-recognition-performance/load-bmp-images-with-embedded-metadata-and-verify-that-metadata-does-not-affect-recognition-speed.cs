// Title: Barcode Recognition from BMP with Metadata
// Description: Loads a BMP image that may contain embedded metadata and measures barcode recognition time, demonstrating that metadata does not impact performance.
// Prompt: Load BMP images with embedded metadata and verify that metadata does not affect recognition speed.
// Tags: barcode, recognition, bmp, metadata, performance, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Diagnostics;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a BMP image (potentially containing metadata) and measuring
/// the time required to recognize barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the image, runs barcode recognition,
    /// and reports detected barcodes along with the elapsed time.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image that may contain metadata
        string imagePath = "sample.bmp";

        // Verify that the file exists before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the BMP image using Aspose.Drawing within a using block to ensure disposal
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Start timing the barcode recognition process
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create a BarCodeReader for all supported symbologies
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Iterate through detected barcodes and output their type and text
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            // Stop timing and report the elapsed time
            stopwatch.Stop();
            Console.WriteLine($"Recognition elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}