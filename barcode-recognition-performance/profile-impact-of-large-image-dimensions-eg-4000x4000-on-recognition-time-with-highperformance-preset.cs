// Title: Barcode recognition performance profiling with large images
// Description: Demonstrates how image size (4000x4000) affects barcode recognition time using the HighPerformance preset.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category. It shows how to generate a barcode, embed it in a large image, and measure decoding speed using BarCodeReader with QualitySettings.HighPerformance. Developers often need to evaluate processing time for high‑resolution scans in industrial or retail scenarios, and this snippet illustrates typical API usage such as BarcodeGenerator, BarCodeReader, and QualitySettings.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: barcode, performance, highresolution, highperformance, code128, png, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a small barcode, places it on a large 4000x4000 image,
/// and measures the recognition time using the HighPerformance quality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates images, runs recognition, and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths
        string smallImagePath = "smallBarcode.png";
        string largeImagePath = "largeBarcode.png";

        // ------------------------------------------------------------
        // Generate a small barcode image (Code128) and save as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(smallImagePath, BarCodeImageFormat.Png);
        }

        // Verify the small barcode image was created
        if (!File.Exists(smallImagePath))
        {
            Console.WriteLine("Failed to create small barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Load the small barcode and embed it into a large blank bitmap
        // ------------------------------------------------------------
        using (var smallBmp = new Bitmap(smallImagePath))
        {
            // Create a large white bitmap (4000x4000) with 24‑bpp RGB format
            using (var largeBmp = new Bitmap(4000, 4000, PixelFormat.Format24bppRgb))
            {
                using (var graphics = Graphics.FromImage(largeBmp))
                {
                    // Fill background with white
                    graphics.Clear(Color.White);

                    // Calculate offsets to center the small barcode
                    int offsetX = (largeBmp.Width - smallBmp.Width) / 2;
                    int offsetY = (largeBmp.Height - smallBmp.Height) / 2;

                    // Draw the small barcode onto the large image
                    graphics.DrawImage(smallBmp, offsetX, offsetY);
                }

                // Save the composite large image as PNG
                largeBmp.Save(largeImagePath, ImageFormat.Png);
            }
        }

        // Verify the large image was saved successfully
        if (!File.Exists(largeImagePath))
        {
            Console.WriteLine("Failed to create large barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Measure barcode recognition time using HighPerformance preset
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(largeImagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighPerformance quality setting for faster decoding
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Optional timeout to prevent hangs on problematic images (10 seconds)
            reader.Timeout = 10000;

            // Start timing
            var stopwatch = Stopwatch.StartNew();

            // Perform barcode detection
            var results = reader.ReadBarCodes();

            // Stop timing
            stopwatch.Stop();

            // Output elapsed time
            Console.WriteLine($"Recognition time (HighPerformance): {stopwatch.ElapsedMilliseconds} ms");

            // Report detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files (optional)
        // ------------------------------------------------------------
        try { File.Delete(smallImagePath); } catch { }
        try { File.Delete(largeImagePath); } catch { }
    }
}