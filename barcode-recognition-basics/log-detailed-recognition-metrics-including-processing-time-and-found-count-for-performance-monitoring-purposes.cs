// Title: Barcode Generation, Recognition, and Performance Logging
// Description: Generates a Code128 barcode, recognizes it, and logs processing time and detection count for monitoring.
// Prompt: Log detailed recognition metrics, including processing time and found count, for performance monitoring purposes.
// Tags: barcode, generation, recognition, performance, metrics, code128, aspose, aspnet

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode, recognizing it, and logging detailed performance metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it back, and outputs recognition statistics.
    /// </summary>
    static void Main()
    {
        // Create a sample barcode image in memory using Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the PNG image as a Bitmap for barcode recognition.
                using (Bitmap bitmap = new Bitmap(ms))
                {
                    // Initialize the barcode reader to detect all supported symbologies.
                    using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Start timing the recognition process.
                        Stopwatch sw = Stopwatch.StartNew();

                        // Perform barcode detection.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Stop timing after detection completes.
                        sw.Stop();

                        // Log overall performance metrics.
                        Console.WriteLine($"Processing Time (ms): {sw.ElapsedMilliseconds}");
                        Console.WriteLine($"Barcodes Detected: {results.Length}");

                        // Iterate through each detected barcode and log detailed information.
                        for (int i = 0; i < results.Length; i++)
                        {
                            BarCodeResult result = results[i];
                            Console.WriteLine($"--- Barcode #{i + 1} ---");
                            Console.WriteLine($"Type: {result.CodeTypeName}");
                            Console.WriteLine($"Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                            Console.WriteLine($"Angle: {result.Region.Angle}");

                            // Output the region rectangle coordinates (pixel values).
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        }
                    }
                }
            }
        }
    }
}