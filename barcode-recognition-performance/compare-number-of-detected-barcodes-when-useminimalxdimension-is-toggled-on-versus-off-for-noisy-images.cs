// Title: Barcode detection with and without UseMinimalXDimension on noisy images
// Description: Generates a Code128 barcode, adds random noise, then compares detection counts using default XDimension and the UseMinimalXDimension mode.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: barcode, detection, noise, useminimalxdimension, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how the UseMinimalXDimension setting influences barcode detection
/// in noisy images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, adds noise, and compares detection results
    /// with default XDimension settings versus UseMinimalXDimension mode.
    /// </summary>
    static void Main()
    {
        // Generate a simple barcode image in memory
        using (var barcodeStream = new MemoryStream())
        {
            // Create a Code128 barcode with sample data and save it as PNG into the stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before loading the image into a bitmap
            barcodeStream.Position = 0;

            // Load the barcode image from the stream into a bitmap for pixel manipulation
            using (var bitmap = new Bitmap(barcodeStream))
            {
                // Add random noise pixels to simulate a noisy image
                var random = new Random();
                int noisePixels = 1000; // number of noisy pixels to inject
                for (int i = 0; i < noisePixels; i++)
                {
                    int x = random.Next(bitmap.Width);
                    int y = random.Next(bitmap.Height);
                    var noisyColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    bitmap.SetPixel(x, y, noisyColor);
                }

                // Detect barcodes using default XDimension settings
                int defaultCount;
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    defaultCount = results.Length;
                }

                // Detect barcodes with UseMinimalXDimension mode enabled
                int minimalCount;
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Switch to minimal XDimension mode to improve detection in noisy conditions
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    var results = reader.ReadBarCodes();
                    minimalCount = results.Length;
                }

                // Output the comparison results to the console
                Console.WriteLine($"Detected barcodes (default XDimension): {defaultCount}");
                Console.WriteLine($"Detected barcodes (UseMinimalXDimension): {minimalCount}");
            }
        }
    }
}