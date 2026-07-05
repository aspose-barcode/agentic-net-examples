// Title: Barcode Recognition Timing from High‑Resolution PNG
// Description: Loads a high‑resolution PNG image, runs barcode detection with default settings, and reports the elapsed time.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: barcode, recognition, timing, png, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a high‑resolution PNG image, recognizing barcodes,
/// and measuring the recognition time using Aspose.BarCode default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs image loading, barcode reading,
    /// and outputs timing and detection results to the console.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution PNG image
        string imagePath = "high_res.png";

        // Verify that the image file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image into a bitmap using Aspose.Drawing
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Initialize the barcode reader and assign the bitmap as the source image
            using (BarCodeReader reader = new BarCodeReader())
            {
                reader.SetBarCodeImage(bitmap);

                // Start timing the barcode recognition process
                Stopwatch sw = Stopwatch.StartNew();

                // Perform barcode detection with default settings
                BarCodeResult[] results = reader.ReadBarCodes();

                // Stop the timer once detection is complete
                sw.Stop();

                // Output the elapsed time in milliseconds
                Console.WriteLine($"Recognition time: {sw.Elapsed.TotalMilliseconds} ms");

                // Iterate through all detected barcodes and display their type and text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}