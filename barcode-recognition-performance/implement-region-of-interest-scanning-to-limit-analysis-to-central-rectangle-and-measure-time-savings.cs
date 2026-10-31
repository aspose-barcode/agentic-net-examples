// Title: Region‑of‑Interest Barcode Scanning with Performance Timing
// Description: Demonstrates how to limit barcode recognition to a central rectangle of an image and compares the processing time against a full‑image scan.
// Category-Description: This example belongs to the Aspose.BarCode image‑processing category, showcasing the BarCodeReader class with region‑of‑interest support. Typical use cases include speeding up recognition in large images by focusing on expected barcode locations. Developers often need to create composite images, define a Rectangle region, and measure performance gains.
// Prompt: Implement region‑of‑interest scanning to limit analysis to a central rectangle and measure time savings.
// Tags: barcode, region-of-interest, scanning, performance, timing, aspose.barcode, generation, recognition, png, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates region‑of‑interest barcode scanning and performance comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a composite image with multiple barcodes, scans it fully and within a central region,
    /// and outputs detection counts and elapsed time.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Create a composite image that contains several barcodes at different positions
        string compositePath = Path.Combine(workFolder, "composite.png");
        CreateCompositeImage(compositePath);

        // Verify that the image was created successfully
        if (!File.Exists(compositePath))
        {
            Console.WriteLine("Failed to create the composite image.");
            return;
        }

        // Load the composite image into a bitmap for processing
        using (Bitmap bitmap = new Bitmap(compositePath))
        {
            // -------------------------------------------------
            // Full‑image scan (baseline measurement)
            // -------------------------------------------------
            Stopwatch swFull = Stopwatch.StartNew();
            using (BarCodeReader fullReader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] fullResults = fullReader.ReadBarCodes();
                swFull.Stop();

                Console.WriteLine($"Full scan: Detected {fullResults.Length} barcode(s) in {swFull.ElapsedMilliseconds} ms.");
                foreach (BarCodeResult res in fullResults)
                {
                    Console.WriteLine($"  Type: {res.CodeTypeName}, Text: {res.CodeText}");
                }
            }

            // Define a central region that is half the width and height of the image, centered
            Rectangle region = new Rectangle(
                bitmap.Width / 4,
                bitmap.Height / 4,
                bitmap.Width / 2,
                bitmap.Height / 2);

            // -------------------------------------------------
            // Region‑limited scan (performance test)
            // -------------------------------------------------
            Stopwatch swRegion = Stopwatch.StartNew();
            using (BarCodeReader regionReader = new BarCodeReader(bitmap, region, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] regionResults = regionReader.ReadBarCodes();
                swRegion.Stop();

                Console.WriteLine($"Region scan: Detected {regionResults.Length} barcode(s) in {swRegion.ElapsedMilliseconds} ms.");
                foreach (BarCodeResult res in regionResults)
                {
                    Console.WriteLine($"  Type: {res.CodeTypeName}, Text: {res.CodeText}");
                }
            }
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    /// <summary>
    /// Creates a composite PNG image containing three different barcodes positioned across the canvas.
    /// </summary>
    /// <param name="outputPath">File path where the composite image will be saved.</param>
    static void CreateCompositeImage(string outputPath)
    {
        const int canvasWidth = 800;
        const int canvasHeight = 600;

        using (Bitmap canvas = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                // Fill background with white
                g.Clear(Color.White);

                // Generate and draw a Code128 barcode at the top‑left corner
                DrawBarcode(g, "CODE128-EXAMPLE", EncodeTypes.Code128, 0, 0, 200, 100);

                // Generate and draw a QR code at the center of the canvas
                DrawBarcode(g, "QR-EXAMPLE", EncodeTypes.QR, 300, 200, 200, 200);

                // Generate and draw a DataMatrix barcode at the bottom‑right corner
                DrawBarcode(g, "DM-EXAMPLE", EncodeTypes.DataMatrix, 550, 400, 200, 200);
            }

            // Save the composed image as PNG
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                canvas.Save(fs, ImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Generates a barcode image using <see cref="BarcodeGenerator"/> and draws it onto the provided graphics surface.
    /// </summary>
    /// <param name="g">Graphics surface to draw the barcode on.</param>
    /// <param name="text">Text to encode in the barcode.</param>
    /// <param name="encodeType">Symbology type for the barcode.</param>
    /// <param name="x">X‑coordinate of the top‑left corner where the barcode will be placed.</param>
    /// <param name="y">Y‑coordinate of the top‑left corner where the barcode will be placed.</param>
    /// <param name="width">Width of the drawn barcode image.</param>
    /// <param name="height">Height of the drawn barcode image.</param>
    static void DrawBarcode(Graphics g, string text, BaseEncodeType encodeType, int x, int y, int width, int height)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, text))
        {
            // Basic generator settings
            generator.Parameters.Barcode.XDimension.Point = 2.0f;
            generator.Parameters.Barcode.FilledBars = false;
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Render barcode to a memory stream and draw it onto the canvas
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (Bitmap barcodeBmp = new Bitmap(ms))
                {
                    g.DrawImage(barcodeBmp, x, y, width, height);
                }
            }
        }
    }
}