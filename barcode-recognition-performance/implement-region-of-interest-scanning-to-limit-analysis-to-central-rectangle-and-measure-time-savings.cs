// Title: Region-of-Interest Barcode Scanning with Timing
// Description: Demonstrates scanning a barcode image using Aspose.BarCode, first over the full image then within a central ROI, and reports time saved.
// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
// Tags: barcode, region-of-interest, timing, aspose.barcode, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, scans it using full‑image and ROI modes,
/// and compares the processing times.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, performs two scans (full image and ROI),
    /// and outputs detection counts and timing information.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample barcode image and save it to disk
        // ------------------------------------------------------------
        const string imagePath = "sample_barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // 2. Load the image once and reuse the bitmap for both scans
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(imagePath))
        {
            // --------------------------------------------------------
            // 2a. Scan the entire image and measure elapsed time
            // --------------------------------------------------------
            var fullScanTime = MeasureScan(bitmap, null, out var fullResults);
            Console.WriteLine($"Full scan detected {fullResults} barcode(s) in {fullScanTime} ms.");

            // --------------------------------------------------------
            // 2b. Define a central region‑of‑interest (ROI)
            //      – rectangle covering the middle half of the image
            // --------------------------------------------------------
            int roiX = bitmap.Width / 4;
            int roiY = bitmap.Height / 4;
            int roiWidth = bitmap.Width / 2;
            int roiHeight = bitmap.Height / 2;
            var roiRect = new Rectangle(roiX, roiY, roiWidth, roiHeight);

            // --------------------------------------------------------
            // 2c. Scan only the ROI and measure elapsed time
            // --------------------------------------------------------
            var roiScanTime = MeasureScan(bitmap, roiRect, out var roiResults);
            Console.WriteLine($"ROI scan detected {roiResults} barcode(s) in {roiScanTime} ms.");

            // --------------------------------------------------------
            // 3. Report simple time‑savings comparison
            // --------------------------------------------------------
            double saved = fullScanTime - roiScanTime;
            Console.WriteLine(saved > 0
                ? $"Time saved by ROI: {saved} ms."
                : "ROI did not improve scan time.");
        }
    }

    /// <summary>
    /// Scans the provided bitmap for barcodes, optionally limited to a region.
    /// Returns the elapsed time in milliseconds and outputs the number of barcodes detected.
    /// </summary>
    /// <param name="bitmap">Source image containing barcodes.</param>
    /// <param name="region">Optional rectangle defining the region‑of‑interest.</param>
    /// <param name="detectedCount">Number of barcodes detected during the scan.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    private static long MeasureScan(Bitmap bitmap, Rectangle? region, out int detectedCount)
    {
        detectedCount = 0;
        var stopwatch = Stopwatch.StartNew();

        if (region.HasValue)
        {
            // Scan only the specified rectangle (ROI)
            using (var reader = new BarCodeReader(bitmap, region.Value, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[ROI] Detected: {result.CodeText}");
                    detectedCount++;
                }
            }
        }
        else
        {
            // Scan the whole image
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[Full] Detected: {result.CodeText}");
                    detectedCount++;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}