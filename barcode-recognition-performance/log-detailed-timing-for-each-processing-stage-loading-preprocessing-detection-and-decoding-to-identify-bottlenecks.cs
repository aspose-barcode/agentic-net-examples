// Title: QR Code generation, loading, and decoding with timing analysis
// Description: Demonstrates generating a QR barcode, loading it into a bitmap, and measuring the time taken for loading, preprocessing, detection, and decoding stages.
// Prompt: Log detailed timing for each processing stage—loading, preprocessing, detection, and decoding—to identify bottlenecks.
// Tags: qr, barcode, generation, recognition, timing, aspose.barcode, bitmap

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, loads it into a bitmap,
/// and measures the time taken for each processing stage (loading, preprocessing,
/// detection, and decoding) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, processes it,
    /// and logs detailed timing information for each stage.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0;

                // Stage: Loading the image into a Bitmap
                var loadStopwatch = Stopwatch.StartNew();
                using (var bitmap = new Bitmap(imageStream))
                {
                    loadStopwatch.Stop();
                    Console.WriteLine($"Loading time: {loadStopwatch.ElapsedMilliseconds} ms");

                    // Stage: Preprocessing (setting quality settings)
                    var preprocessStopwatch = Stopwatch.StartNew();
                    using (var reader = new BarCodeReader())
                    {
                        // Use all supported decode types
                        reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                        // Apply a high‑performance preset (optional preprocessing step)
                        reader.QualitySettings = QualitySettings.HighPerformance;
                        // Assign the bitmap image to the reader
                        reader.SetBarCodeImage(bitmap);
                        preprocessStopwatch.Stop();
                        Console.WriteLine($"Preprocessing time: {preprocessStopwatch.ElapsedMilliseconds} ms");

                        // Stage: Detection (reading barcodes)
                        var detectionStopwatch = Stopwatch.StartNew();
                        BarCodeResult[] results = reader.ReadBarCodes();
                        detectionStopwatch.Stop();
                        Console.WriteLine($"Detection time: {detectionStopwatch.ElapsedMilliseconds} ms");

                        // Stage: Decoding (extracting information from results)
                        var decodingStopwatch = Stopwatch.StartNew();
                        int count = 0;
                        foreach (var result in results)
                        {
                            count++;
                            Console.WriteLine($"--- Barcode #{count} ---");
                            Console.WriteLine($"Type: {result.CodeTypeName}");
                            Console.WriteLine($"Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                            var rect = result.Region.Rectangle;
                            Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                            Console.WriteLine($"Angle: {result.Region.Angle}");
                        }
                        decodingStopwatch.Stop();
                        Console.WriteLine($"Decoding time: {decodingStopwatch.ElapsedMilliseconds} ms");
                    }
                }
            }
        }
    }
}