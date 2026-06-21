using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, scanning it fully and within a region of interest (ROI),
/// and comparing the performance of both approaches.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image in memory, performs a full image scan and an ROI scan,
    /// then outputs timing and count information for each scan.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image in memory using Code128 encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample12345"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode to the memory stream as PNG.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the PNG image into a Bitmap for processing.
                using (var bitmap = new Bitmap(ms))
                {
                    // ---------- Full image scan ----------
                    long fullTime;   // Elapsed time in milliseconds for full scan.
                    int fullCount;   // Number of barcodes detected in full scan.

                    using (var fullReader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        var swFull = Stopwatch.StartNew();          // Start timing.
                        var results = fullReader.ReadBarCodes();    // Perform barcode detection.
                        swFull.Stop();                              // Stop timing.
                        fullTime = swFull.ElapsedMilliseconds;     // Record elapsed time.
                        fullCount = results.Length;                // Record number of barcodes found.
                    }

                    // ---------- Define region of interest (ROI) ----------
                    // ROI is centered and covers half the width and height of the image.
                    int roiWidth = bitmap.Width / 2;
                    int roiHeight = bitmap.Height / 2;
                    int roiX = (bitmap.Width - roiWidth) / 2;
                    int roiY = (bitmap.Height - roiHeight) / 2;
                    var roiRect = new Rectangle(roiX, roiY, roiWidth, roiHeight);

                    // ---------- ROI scan ----------
                    long roiTime;    // Elapsed time in milliseconds for ROI scan.
                    int roiCount;    // Number of barcodes detected in ROI scan.

                    using (var roiReader = new BarCodeReader())
                    {
                        // Configure reader to decode all supported barcode types.
                        roiReader.BarCodeReadType = DecodeType.AllSupportedTypes;
                        // Set the image and the ROI rectangle for scanning.
                        roiReader.SetBarCodeImage(bitmap, roiRect);
                        var swRoi = Stopwatch.StartNew();           // Start timing.
                        var results = roiReader.ReadBarCodes();     // Perform barcode detection within ROI.
                        swRoi.Stop();                               // Stop timing.
                        roiTime = swRoi.ElapsedMilliseconds;        // Record elapsed time.
                        roiCount = results.Length;                 // Record number of barcodes found.
                    }

                    // ---------- Output results ----------
                    Console.WriteLine($"Full scan:   Time = {fullTime} ms, Barcodes found = {fullCount}");
                    Console.WriteLine($"ROI scan:    Time = {roiTime} ms, Barcodes found = {roiCount}");
                    Console.WriteLine($"Time saved:  {fullTime - roiTime} ms");
                }
            }
        }
    }
}