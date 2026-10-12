// Title: Compare barcode detection with and without UseMinimalXDimension on noisy images
// Description: This example generates a composite image containing multiple Code128 barcodes with added random noise, then reads the barcodes twice—once using the default XDimension mode and once with UseMinimalXDimension enabled—to compare detection counts.
// Category-Description: Demonstrates Aspose.BarCode barcode recognition settings for noisy images. It showcases the BarCodeReader class, QualitySettings, and XDimensionMode enumeration to adjust X‑dimension handling. Typical use cases include improving detection reliability in low‑quality or noisy scans, where developers often experiment with minimal X‑dimension settings to increase tolerance.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: code128, barcode recognition, useminimalxdimension, noisy image, qualitysettings, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the effect of toggling UseMinimalXDimension on barcode detection in noisy images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a noisy composite image with several Code128 barcodes,
    /// reads the barcodes with normal and minimal X‑dimension settings, and prints detection counts.
    /// </summary>
    static void Main()
    {
        // Generate a composite image that contains multiple barcodes and random noise
        using (var compositeStream = GenerateNoisyBarcodeImage())
        {
            // Read barcodes using the default (Normal) XDimension mode
            int countNormal = ReadBarcodes(compositeStream, useMinimal: false);

            // Reset the stream position to the beginning for the second read
            compositeStream.Position = 0;

            // Read barcodes using the UseMinimalXDimension mode
            int countMinimal = ReadBarcodes(compositeStream, useMinimal: true);

            // Output the detection results for comparison
            Console.WriteLine($"Barcodes detected with Normal XDimension: {countNormal}");
            Console.WriteLine($"Barcodes detected with UseMinimalXDimension: {countMinimal}");
        }
    }

    /// <summary>
    /// Creates an in‑memory PNG image that contains several Code128 barcodes placed at random positions,
    /// then adds random black pixel noise to simulate a low‑quality scan.
    /// </summary>
    /// <returns>A MemoryStream containing the generated PNG image.</returns>
    static MemoryStream GenerateNoisyBarcodeImage()
    {
        const int imageWidth = 800;
        const int imageHeight = 600;
        const int barcodeCount = 5;
        var random = new Random();

        // Create a blank bitmap canvas
        var bitmap = new Bitmap(imageWidth, imageHeight);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            // Fill background with white
            graphics.Clear(Color.White);

            // Generate and draw each barcode onto the canvas
            for (int i = 0; i < barcodeCount; i++)
            {
                string codeText = $"CODE{i + 1}";
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    using (var barcodeStream = new MemoryStream())
                    {
                        // Save barcode as PNG into a temporary stream
                        generator.Save(barcodeStream, BarCodeImageFormat.Png);
                        barcodeStream.Position = 0;

                        // Load the barcode image from the stream
                        using (var barcodeBitmap = new Bitmap(barcodeStream))
                        {
                            // Choose a random position that keeps the barcode fully inside the canvas
                            int x = random.Next(0, imageWidth - barcodeBitmap.Width);
                            int y = random.Next(0, imageHeight - barcodeBitmap.Height);
                            graphics.DrawImage(barcodeBitmap, x, y);
                        }
                    }
                }
            }

            // Add random black pixel noise to the image
            for (int n = 0; n < 2000; n++)
            {
                int x = random.Next(0, imageWidth);
                int y = random.Next(0, imageHeight);
                graphics.FillRectangle(Brushes.Black, x, y, 1, 1);
            }
        }

        // Save the composite bitmap to a memory stream as PNG
        var resultStream = new MemoryStream();
        bitmap.Save(resultStream, ImageFormat.Png);
        resultStream.Position = 0;

        // Release bitmap resources
        bitmap.Dispose();

        return resultStream;
    }

    /// <summary>
    /// Reads barcodes from the provided image stream using either the normal or minimal XDimension mode.
    /// </summary>
    /// <param name="imageStream">Stream containing the image to be processed.</param>
    /// <param name="useMinimal">If true, configures the reader to use minimal XDimension; otherwise uses normal mode.</param>
    /// <returns>The number of barcodes detected in the image.</returns>
    static int ReadBarcodes(Stream imageStream, bool useMinimal)
    {
        // Restrict decoding to Code128 symbology for this example
        BaseDecodeType decodeType = DecodeType.Code128;

        using (var reader = new BarCodeReader(imageStream, decodeType))
        {
            // Configure XDimension handling based on the useMinimal flag
            if (useMinimal)
            {
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f; // Set minimal XDimension value
            }
            else
            {
                reader.QualitySettings.XDimension = XDimensionMode.Normal;
            }

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output each detected barcode's type and text
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Return the total count of detected barcodes
            return results.Length;
        }
    }
}