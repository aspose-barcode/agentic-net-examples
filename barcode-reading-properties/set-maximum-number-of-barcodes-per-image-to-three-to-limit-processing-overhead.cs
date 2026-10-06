// Title: Limit Barcode Decoding to Three per Image
// Description: Demonstrates generating multiple Code128 barcodes, combining them into a single image, and decoding up to three barcodes from that image to reduce processing overhead.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, Aspose.Drawing to compose a combined image, and BarCodeReader to extract barcode data. Typical use cases include batch barcode creation, image composition, and performance‑optimized scanning where only a subset of barcodes needs to be processed. Developers often need to limit the number of decoded symbols to avoid unnecessary computation, especially in high‑throughput scenarios.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: barcode, code128, generation, recognition, limit, aspose.barcode, image processing, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates several Code128 barcodes, merges them into one image, and reads a maximum of three barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, builds a combined barcode image, reads up to three barcodes, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string combinedPath = Path.Combine(tempDir, "combined.png");

        // Sample texts for multiple barcodes
        List<string> texts = new List<string> { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };
        List<Bitmap> barcodeBitmaps = new List<Bitmap>();

        // Generate individual barcode images
        foreach (string txt in texts)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, txt))
            {
                // Set X-dimension to control barcode width
                generator.Parameters.Barcode.XDimension.Point = 2f;
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeBitmaps.Add(bmp);
            }
        }

        // Determine combined image size (max width, total height with spacing)
        int maxWidth = 0;
        int totalHeight = 0;
        int spacing = 10;
        foreach (Bitmap bmp in barcodeBitmaps)
        {
            if (bmp.Width > maxWidth) maxWidth = bmp.Width;
            totalHeight += bmp.Height + spacing;
        }
        totalHeight -= spacing; // remove extra spacing after last barcode

        // Create combined bitmap and draw individual barcodes onto it
        using (Bitmap combined = new Bitmap(maxWidth, totalHeight, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(combined))
            {
                g.Clear(Aspose.Drawing.Color.White);
                int y = 0;
                foreach (Bitmap bmp in barcodeBitmaps)
                {
                    g.DrawImage(bmp, 0, y);
                    y += bmp.Height + spacing;
                }
            }

            // Save combined image to temporary file
            combined.Save(combinedPath, ImageFormat.Png);
        }

        // Dispose individual barcode bitmaps now that they are no longer needed
        foreach (Bitmap bmp in barcodeBitmaps)
        {
            bmp.Dispose();
        }

        // Read barcodes from the combined image, limiting to a maximum of three
        BaseDecodeType decode = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(combinedPath, decode))
        {
            int processed = 0;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                processed++;
                if (processed >= 3)
                {
                    // Stop after processing three barcodes to meet the limit
                    break;
                }
            }
            Console.WriteLine($"Processed {processed} barcode(s) (maximum 3).");
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(combinedPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}