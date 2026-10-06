// Title: Region‑of‑Interest Barcode Scanning Benchmark
// Description: Demonstrates scanning a barcode within a defined central rectangle to reduce processing time compared to scanning the full image.
// Category-Description: Shows Aspose.BarCode barcode recognition using BarCodeReader with and without a region‑of‑interest. Typical use cases include speeding up scanning in large images or video frames by limiting analysis to a specific area. Developers often work with BarcodeGenerator, BarCodeReader, DecodeType, and Rectangle to generate, embed, and detect barcodes efficiently.
/// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
/// Tags: barcode, region of interest, scanning, performance, aspose.barcode, qr, decode, benchmark

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a QR code, embed it in a larger image,
/// and compare full‑image barcode scanning with region‑of‑interest scanning
/// using Aspose.BarCode. The example measures the time saved by limiting the
/// analysis area to a central rectangle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, creates a composite image,
    /// runs two scanning benchmarks (full image vs. ROI), outputs the results,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated files
        string workDir = Path.Combine(Path.GetTempPath(), "RegionOfInterestDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Paths for the individual barcode image and the combined canvas image
        string barcodeFile = Path.Combine(workDir, "barcode.png");
        string combinedFile = Path.Combine(workDir, "combined.png");

        // ------------------------------------------------------------
        // Generate a QR barcode and save it to a PNG file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (var barcodeBmp = new Bitmap(ms))
                {
                    // Save the barcode image for later composition
                    barcodeBmp.Save(barcodeFile, ImageFormat.Png);
                }
            }
        }

        // ------------------------------------------------------------
        // Create a larger canvas and draw the barcode at its center
        // ------------------------------------------------------------
        const int canvasWidth = 800;
        const int canvasHeight = 600;
        using (var canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (var graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
                using (var barcodeBmp = new Bitmap(barcodeFile))
                {
                    int x = (canvasWidth - barcodeBmp.Width) / 2;
                    int y = (canvasHeight - barcodeBmp.Height) / 2;
                    graphics.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                }
            }
            canvas.Save(combinedFile, ImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Define a central rectangle (region of interest) that is half the canvas size
        // ------------------------------------------------------------
        int regionWidth = canvasWidth / 2;
        int regionHeight = canvasHeight / 2;
        int regionX = (canvasWidth - regionWidth) / 2;
        int regionY = (canvasHeight - regionHeight) / 2;
        Rectangle centralRect = new Rectangle(regionX, regionY, regionWidth, regionHeight);

        // ------------------------------------------------------------
        // Benchmark scanning the full image
        // ------------------------------------------------------------
        Stopwatch swFull = new Stopwatch();
        int fullCount = 0;
        swFull.Start();
        using (var readerFull = new BarCodeReader(combinedFile, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in readerFull.ReadBarCodes())
            {
                fullCount++;
            }
        }
        swFull.Stop();

        // ------------------------------------------------------------
        // Benchmark scanning only the defined region of interest
        // ------------------------------------------------------------
        Stopwatch swRegion = new Stopwatch();
        int regionCount = 0;
        using (var bmp = new Bitmap(combinedFile))
        {
            swRegion.Start();
            using (var readerRegion = new BarCodeReader(bmp, centralRect, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in readerRegion.ReadBarCodes())
                {
                    regionCount++;
                }
            }
            swRegion.Stop();
        }

        // ------------------------------------------------------------
        // Output benchmark results
        // ------------------------------------------------------------
        Console.WriteLine($"Full image scan:   Time = {swFull.ElapsedMilliseconds} ms, Barcodes detected = {fullCount}");
        Console.WriteLine($"Region scan:       Time = {swRegion.ElapsedMilliseconds} ms, Barcodes detected = {regionCount}");
        Console.WriteLine($"Time saved by ROI: {swFull.ElapsedMilliseconds - swRegion.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodeFile);
            File.Delete(combinedFile);
            Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}