// Title: Region‑of‑Interest Scanning with Timing Comparison
// Description: Demonstrates scanning a barcode image using a full‑image scan versus a central region‑of‑interest (ROI) scan to measure performance improvements.
// Category-Description: This example belongs to the Aspose.BarCode scanning category, showcasing the BarCodeReader class with and without ROI constraints. Developers often need to limit barcode detection to specific areas of an image to reduce processing time, especially in high‑resolution or multi‑barcode scenarios. Typical use cases include document processing, industrial automation, and mobile scanning where speed is critical.
// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
// Tags: qr, scanning, console, barcodelibrary, barcodegenerator, barcodereader, bitmap, rectangle

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code (if needed) and compares full‑image barcode scanning
/// with scanning limited to a central region‑of‑interest, reporting the time saved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs barcode generation (if required), executes both scanning
    /// approaches, and outputs timing results to the console.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample_barcode.png";

        // Generate a sample QR code if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generator.Save(imagePath);
            }
        }

        // Load the image once and reuse it for both scans
        using (var bitmap = new Bitmap(imagePath))
        {
            // -------------------------------------------------
            // Full image scan
            // -------------------------------------------------
            var fullStopwatch = new Stopwatch();
            fullStopwatch.Start();

            using (var fullReader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                foreach (var result in fullReader.ReadBarCodes())
                {
                    // Result processing placeholder (e.g., counting or logging)
                }
            }

            fullStopwatch.Stop();
            long fullTimeMs = fullStopwatch.ElapsedMilliseconds;

            // -------------------------------------------------
            // Region‑of‑interest (central rectangle) scan
            // -------------------------------------------------
            // Define a central rectangle covering 50 % of the image width and height
            int roiWidth = bitmap.Width / 2;
            int roiHeight = bitmap.Height / 2;
            int roiX = (bitmap.Width - roiWidth) / 2;
            int roiY = (bitmap.Height - roiHeight) / 2;
            var roiRect = new Rectangle(roiX, roiY, roiWidth, roiHeight);

            var roiStopwatch = new Stopwatch();
            roiStopwatch.Start();

            using (var roiReader = new BarCodeReader(bitmap, new Rectangle[] { roiRect }, DecodeType.AllSupportedTypes))
            {
                foreach (var result in roiReader.ReadBarCodes())
                {
                    // Result processing placeholder (e.g., counting or logging)
                }
            }

            roiStopwatch.Stop();
            long roiTimeMs = roiStopwatch.ElapsedMilliseconds;

            // -------------------------------------------------
            // Output timing results
            // -------------------------------------------------
            Console.WriteLine($"Full image scan time: {fullTimeMs} ms");
            Console.WriteLine($"ROI (central rectangle) scan time: {roiTimeMs} ms");

            if (fullTimeMs > 0)
            {
                double savedPercent = 100.0 * (fullTimeMs - roiTimeMs) / fullTimeMs;
                Console.WriteLine($"Time saved by ROI scanning: {savedPercent:F2}%");
            }
        }
    }
}