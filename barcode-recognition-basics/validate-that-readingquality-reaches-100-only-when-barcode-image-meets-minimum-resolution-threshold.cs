// Title: Validate ReadingQuality based on barcode image resolution
// Description: Demonstrates generating QR barcodes at different DPI settings and checking the ReadingQuality property to ensure it reaches 100 only when the image meets a minimum resolution.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ReadingQuality metric to assess scan reliability. Developers often need to verify that barcode images meet resolution requirements for optimal scanning in applications such as inventory management, ticketing, and document processing.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: qr, barcode, readingquality, resolution, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how barcode reading quality varies with image resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates low and high resolution QR codes, evaluates their reading quality, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Minimum DPI required for perfect reading quality
        const float minResolution = 300f;

        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Paths for low‑ and high‑resolution barcode images
        string lowResPath = Path.Combine(tempDir, "qr_low.png");
        string highResPath = Path.Combine(tempDir, "qr_high.png");
        string codeText = "Test123";

        // Generate low resolution barcode (96 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Resolution = 96f;
            generator.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // Generate high resolution barcode (300 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Resolution = 300f;
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Local function to read a barcode image and report its ReadingQuality
        void Evaluate(string imagePath, float resolution)
        {
            // Verify the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Set the expected decode type (QR code)
            BaseDecodeType decodeType = DecodeType.QR;

            // Use BarCodeReader to decode the image
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, report and exit
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in image '{Path.GetFileName(imagePath)}' (Resolution: {resolution} DPI).");
                    return;
                }

                // Iterate through all detected barcodes and output their quality metrics
                foreach (BarCodeResult result in results)
                {
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)} | Resolution: {resolution} DPI | ReadingQuality: {quality}");

                    // Validate that quality matches expectations based on resolution
                    if (resolution >= minResolution && quality != 100.0)
                    {
                        Console.WriteLine("  Warning: Expected ReadingQuality 100 at this resolution, but got lower.");
                    }
                    else if (resolution < minResolution && quality == 100.0)
                    {
                        Console.WriteLine("  Warning: ReadingQuality 100 achieved below the minimum resolution threshold.");
                    }
                }
            }
        }

        // Evaluate both generated images
        Evaluate(lowResPath, 96f);
        Evaluate(highResPath, 300f);

        // Cleanup temporary files and directory
        try
        {
            File.Delete(lowResPath);
            File.Delete(highResPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}