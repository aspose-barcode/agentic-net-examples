// Title: Barcode Recognition Performance on Large Images
// Description: Demonstrates generating a QR code, embedding it into a large 4000x4000 bitmap, and measuring recognition time using the HighPerformance preset.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: qr, barcode, recognition, performance, highperformance, aspose.barcode, image processing

using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR barcode, places it on a large bitmap,
/// and profiles the recognition time using the HighPerformance quality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR barcode, draws it onto a 4000x4000 image,
    /// and measures how long recognition takes with HighPerformance settings.
    /// </summary>
    static void Main()
    {
        // Generate a simple QR barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            using (Image barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a large bitmap (e.g., 4000x4000) and draw the barcode onto it
                const int largeSize = 4000;
                using (var largeBitmap = new Bitmap(largeSize, largeSize))
                {
                    // Prepare graphics object for drawing
                    using (var graphics = Graphics.FromImage(largeBitmap))
                    {
                        // Fill background with white
                        graphics.Clear(Color.White);

                        // Center the barcode image on the large bitmap
                        int x = (largeSize - barcodeImage.Width) / 2;
                        int y = (largeSize - barcodeImage.Height) / 2;
                        graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);
                    }

                    // Measure recognition time using HighPerformance preset
                    using (var reader = new BarCodeReader(largeBitmap, DecodeType.AllSupportedTypes))
                    {
                        // Apply HighPerformance quality settings
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Start timing
                        var stopwatch = Stopwatch.StartNew();

                        // Perform barcode recognition
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Stop timing
                        stopwatch.Stop();

                        // Output recognition duration
                        Console.WriteLine($"Recognition time (HighPerformance) on {largeSize}x{largeSize} image: {stopwatch.ElapsedMilliseconds} ms");

                        // List detected barcodes
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}