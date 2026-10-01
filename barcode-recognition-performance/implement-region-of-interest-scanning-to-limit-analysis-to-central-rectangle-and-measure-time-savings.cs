// Title: Region‑of‑Interest Scanning with Time Measurement
// Description: Generates a QR barcode, embeds it in a larger canvas, then decodes the full image and a central ROI to demonstrate how limiting the scan area can reduce processing time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to create barcodes with BarcodeGenerator, render them with Aspose.Drawing, and read them using BarCodeReader. Developers often need to improve performance by scanning only a region of interest, especially in large images or real‑time scenarios. The code illustrates cropping with Bitmap.Clone and measuring decode duration with Stopwatch.
// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
// Tags: barcode, qr, region-of-interest, performance, generation, recognition, aspose.barcode, .net

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates ROI scanning and performance comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, creates a canvas, measures decode times for full image and ROI.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode
        var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        using (var barcodeStream = new MemoryStream())
        {
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0;

            // Load the barcode image into a bitmap
            using (var barcodeBitmap = new Bitmap(barcodeStream))
            {
                // Create a larger canvas (500x500) with white background
                int canvasSize = 500;
                using (var canvas = new Bitmap(canvasSize, canvasSize))
                {
                    using (var graphics = Graphics.FromImage(canvas))
                    {
                        graphics.Clear(Color.White);
                        // Calculate position to draw the barcode at the center
                        int x = (canvasSize - barcodeBitmap.Width) / 2;
                        int y = (canvasSize - barcodeBitmap.Height) / 2;
                        graphics.DrawImage(barcodeBitmap, x, y, barcodeBitmap.Width, barcodeBitmap.Height);
                    }

                    // Save the full canvas to a memory stream for reading
                    using (var fullImageStream = new MemoryStream())
                    {
                        canvas.Save(fullImageStream, ImageFormat.Png);
                        fullImageStream.Position = 0;

                        // Measure reading time on the full image
                        var fullReadTime = MeasureReadTime(fullImageStream);
                        Console.WriteLine($"Full image read time: {fullReadTime.TotalMilliseconds} ms");

                        // Define a central region of interest (e.g., 200x200 rectangle at the center)
                        int roiSize = 200;
                        int roiX = (canvasSize - roiSize) / 2;
                        int roiY = (canvasSize - roiSize) / 2;
                        var roiRect = new Rectangle(roiX, roiY, roiSize, roiSize);

                        // Crop the ROI from the canvas
                        using (var roiBitmap = canvas.Clone(roiRect, PixelFormat.Format24bppRgb))
                        {
                            using (var roiStream = new MemoryStream())
                            {
                                roiBitmap.Save(roiStream, ImageFormat.Png);
                                roiStream.Position = 0;

                                // Measure reading time on the cropped ROI image
                                var roiReadTime = MeasureReadTime(roiStream);
                                Console.WriteLine($"ROI image read time: {roiReadTime.TotalMilliseconds} ms");

                                // Simple time savings calculation
                                double savings = fullReadTime.TotalMilliseconds - roiReadTime.TotalMilliseconds;
                                Console.WriteLine($"Estimated time saved by ROI scanning: {savings} ms");
                            }
                        }
                    }
                }
            }
        }

        // Note:
        // Aspose.BarCode.BarCodeReader does not provide a direct RegionOfInterest property.
        // To simulate ROI scanning, the image is manually cropped to the desired rectangle before decoding.
    }

    // Helper method to read barcodes from a stream and return the elapsed time
    private static TimeSpan MeasureReadTime(Stream imageStream)
    {
        // Reset stream position before each read
        imageStream.Position = 0;

        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(imageStream, decodeType))
        {
            // Use a high‑performance preset for faster scanning
            reader.QualitySettings = QualitySettings.HighPerformance;

            var stopwatch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();

            // Output detected barcode information (if any)
            if (results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeText} ({result.CodeTypeName})");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected.");
            }

            return stopwatch.Elapsed;
        }
    }
}