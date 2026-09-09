// Title: Barcode ReadingQuality based on image resolution
// Description: Demonstrates generating low and high resolution QR barcodes and checking the ReadingQuality metric to ensure it reaches 100 only for sufficiently high resolution images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes with specific DPI settings and BarCodeReader to evaluate the ReadingQuality property of decoded results. Developers often need to verify that barcode images meet minimum resolution requirements for reliable scanning, especially in automated quality‑control pipelines.
// Prompt: Validate that ReadingQuality reaches 100 only when the barcode image meets a minimum resolution threshold.
// Tags: qr, readingquality, resolution, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how barcode image resolution affects the ReadingQuality metric using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates low‑ and high‑resolution QR barcodes, reads them, and validates that ReadingQuality equals 100 only for the high‑resolution image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary directory for barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for low and high resolution barcodes
        string lowResPath = Path.Combine(tempDir, "barcode_low.png");
        string highResPath = Path.Combine(tempDir, "barcode_high.png");
        string codeText = "ASPOSE";

        // Generate low resolution barcode (96 dpi)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Resolution = 96f;
            generator.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // Generate high resolution barcode (300 dpi)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.Resolution = 300f;
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Read low resolution barcode and capture its ReadingQuality
        double lowQuality = -1;
        if (File.Exists(lowResPath))
        {
            using (BarCodeReader reader = new BarCodeReader(lowResPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    lowQuality = result.ReadingQuality;
                    Console.WriteLine($"Low resolution ReadingQuality: {lowQuality}");
                }
            }
        }
        else
        {
            Console.WriteLine("Low resolution barcode file not found.");
        }

        // Read high resolution barcode and capture its ReadingQuality
        double highQuality = -1;
        if (File.Exists(highResPath))
        {
            using (BarCodeReader reader = new BarCodeReader(highResPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    highQuality = result.ReadingQuality;
                    Console.WriteLine($"High resolution ReadingQuality: {highQuality}");
                }
            }
        }
        else
        {
            Console.WriteLine("High resolution barcode file not found.");
        }

        // Validation logic: ensure only high‑resolution barcode reaches ReadingQuality 100
        const double targetQuality = 100.0;
        const float minResolution = 300f;

        if (lowQuality == targetQuality)
        {
            Console.WriteLine("Warning: Low resolution barcode achieved ReadingQuality 100, which is unexpected.");
        }
        else
        {
            Console.WriteLine("Low resolution barcode did not reach ReadingQuality 100 as expected.");
        }

        if (highQuality == targetQuality)
        {
            Console.WriteLine($"High resolution barcode (>= {minResolution} dpi) achieved ReadingQuality 100 as expected.");
        }
        else
        {
            Console.WriteLine($"High resolution barcode did not reach ReadingQuality 100; check resolution settings.");
        }

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(lowResPath)) File.Delete(lowResPath);
            if (File.Exists(highResPath)) File.Delete(highResPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}