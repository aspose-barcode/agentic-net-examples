// Title: Region-of-Interest Barcode Scanning Demo
// Description: Demonstrates scanning a barcode image using Aspose.BarCode with and without a region-of-interest to compare performance.
// Category-Description: This example belongs to the Aspose.BarCode image recognition category, illustrating how to use BarCodeReader with a full image and a specified Rectangle to limit the scan area. Developers often need to improve processing speed by focusing on a central region where barcodes are expected, using classes such as BarcodeGenerator, BarCodeReader, BarCodeResult, and System.Drawing.Rectangle.
// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
// Tags: barcode, region of interest, performance, qrcode, aspose.barcode, generation, recognition, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Shows how to generate a QR code, then read it using full‑image scanning
/// and region‑of‑interest scanning, measuring the time difference.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, scans it twice (full image
    /// and central region), prints timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample QR code image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Load the generated image into a bitmap for recognition
        using (Bitmap bmp = new Bitmap(imagePath))
        {
            // -------------------------------------------------
            // Full‑image scan (baseline)
            // -------------------------------------------------
            int fullCount = 0;
            Stopwatch swFull = Stopwatch.StartNew();

            using (BarCodeReader readerFull = new BarCodeReader(bmp, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in readerFull.ReadBarCodes())
                {
                    fullCount++;
                }
            }

            swFull.Stop();

            // -------------------------------------------------
            // Define a central region (half the width and height)
            // -------------------------------------------------
            int regionWidth = bmp.Width / 2;
            int regionHeight = bmp.Height / 2;
            int regionX = (bmp.Width - regionWidth) / 2;
            int regionY = (bmp.Height - regionHeight) / 2;
            Rectangle region = new Rectangle(regionX, regionY, regionWidth, regionHeight);

            // -------------------------------------------------
            // Region‑limited scan (focus on central rectangle)
            // -------------------------------------------------
            int regionCount = 0;
            Stopwatch swRegion = Stopwatch.StartNew();

            using (BarCodeReader readerRegion = new BarCodeReader(bmp, region, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in readerRegion.ReadBarCodes())
                {
                    regionCount++;
                }
            }

            swRegion.Stop();

            // Output timing and detection results
            Console.WriteLine($"Full scan: {swFull.ElapsedMilliseconds} ms, barcodes found: {fullCount}");
            Console.WriteLine($"Region scan: {swRegion.ElapsedMilliseconds} ms, barcodes found: {regionCount}");
        }

        // Cleanup temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}