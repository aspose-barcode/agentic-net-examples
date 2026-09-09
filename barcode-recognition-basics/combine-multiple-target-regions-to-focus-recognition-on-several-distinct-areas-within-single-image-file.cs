// Title: Combine Multiple Target Regions for Barcode Recognition
// Description: Demonstrates how to generate two barcodes, combine them into a single image, and read each barcode by specifying separate target regions.
// Category-Description: Shows Aspose.BarCode image generation and recognition using BarcodeGenerator, BarCodeReader, and target region selection. Useful for scenarios where multiple barcodes are present in one image and you need to focus recognition on specific areas. Developers often need to define Rectangle regions to limit decoding to particular parts of an image.
// Prompt: Combine multiple target regions to focus recognition on several distinct areas within a single image file.
// Tags: barcode generation, barcode recognition, target region, multiregion, code128, qr, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates two different barcodes, merges them into one image,
/// and reads each barcode by specifying distinct target regions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, combines them, and performs region‑based recognition.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the individual and combined images
        string barcode1Path = Path.Combine(tempDir, "code128.png");
        string barcode2Path = Path.Combine(tempDir, "qr.png");
        string combinedPath = Path.Combine(tempDir, "combined.png");

        // -------------------------------------------------
        // Generate a Code128 barcode and save it as PNG
        // -------------------------------------------------
        using (var gen1 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var stream1 = new MemoryStream())
            {
                gen1.Save(stream1, BarCodeImageFormat.Png);
                stream1.Position = 0;
                using (var bmp1 = new Bitmap(stream1))
                {
                    bmp1.Save(barcode1Path, ImageFormat.Png);
                }
            }
        }

        // -------------------------------------------------
        // Generate a QR barcode and save it as PNG
        // -------------------------------------------------
        using (var gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            using (var stream2 = new MemoryStream())
            {
                gen2.Save(stream2, BarCodeImageFormat.Png);
                stream2.Position = 0;
                using (var bmp2 = new Bitmap(stream2))
                {
                    bmp2.Save(barcode2Path, ImageFormat.Png);
                }
            }
        }

        // -------------------------------------------------
        // Combine the two barcode images onto a larger canvas
        // -------------------------------------------------
        using (var bmp1 = new Bitmap(barcode1Path))
        using (var bmp2 = new Bitmap(barcode2Path))
        {
            int canvasWidth = 800;
            int canvasHeight = 600;

            using (var canvas = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format24bppRgb))
            {
                using (var graphics = Graphics.FromImage(canvas))
                {
                    graphics.Clear(Color.White);
                    // Draw the Code128 barcode at (50, 50)
                    graphics.DrawImage(bmp1, 50, 50, bmp1.Width, bmp1.Height);
                    // Draw the QR barcode at (400, 300)
                    graphics.DrawImage(bmp2, 400, 300, bmp2.Width, bmp2.Height);
                }

                // Save the combined image to disk
                canvas.Save(combinedPath, ImageFormat.Png);
            }
        }

        // -------------------------------------------------
        // Define target regions that correspond to each barcode's location
        // -------------------------------------------------
        using (var combinedBmp = new Bitmap(combinedPath))
        {
            // Region covering the Code128 barcode
            Rectangle rectCode128;
            using (var tempBmp = new Bitmap(barcode1Path))
            {
                rectCode128 = new Rectangle(50, 50, tempBmp.Width, tempBmp.Height);
            }

            // Region covering the QR barcode
            Rectangle rectQR;
            using (var tempBmp = new Bitmap(barcode2Path))
            {
                rectQR = new Rectangle(400, 300, tempBmp.Width, tempBmp.Height);
            }

            // -------------------------------------------------
            // Read barcodes from the specified regions
            // -------------------------------------------------
            using (var reader = new BarCodeReader())
            {
                // Provide the combined image and the array of target rectangles
                reader.SetBarCodeImage(combinedBmp, new Rectangle[] { rectCode128, rectQR });
                // Limit decoding to the expected symbologies
                reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR);

                Console.WriteLine("Reading barcodes from multiple target regions:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // -------------------------------------------------
        // Cleanup temporary files (optional)
        // -------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}