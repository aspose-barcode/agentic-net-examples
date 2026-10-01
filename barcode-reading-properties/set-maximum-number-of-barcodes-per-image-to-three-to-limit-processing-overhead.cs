// Title: Limit Barcode Detection to Three per Image
// Description: Demonstrates generating multiple Code128 barcodes, combining them into a single image, and reading up to three barcodes from that image to reduce processing overhead.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It shows how to use BarcodeGenerator to create barcodes, Aspose.Drawing to compose images, and BarCodeReader to detect barcodes. Developers often need to batch‑process barcodes while limiting the number of detections per image for performance reasons.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: barcode generation, barcode recognition, code128, limit detection, composite image, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates several Code128 barcodes, merges them into one image,
/// and reads a maximum of three barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample barcode texts
        List<string> texts = new List<string> { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };
        List<Bitmap> barcodeBitmaps = new List<Bitmap>();

        // Generate individual barcode images and keep them in memory
        foreach (string txt in texts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, txt))
            {
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    // Clone to keep after disposing generator
                    barcodeBitmaps.Add((Bitmap)bmp.Clone());
                }
            }
        }

        // Determine combined image size (place barcodes side by side)
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (var bmp in barcodeBitmaps)
        {
            totalWidth += bmp.Width;
            if (bmp.Height > maxHeight) maxHeight = bmp.Height;
        }

        // Create a composite image containing all barcodes
        string compositePath = Path.Combine(tempFolder, "Composite.png");
        using (var finalBitmap = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(finalBitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                int offsetX = 0;
                foreach (var bmp in barcodeBitmaps)
                {
                    graphics.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                    offsetX += bmp.Width;
                }
            }
            finalBitmap.Save(compositePath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Clean up individual barcode bitmaps
        foreach (var bmp in barcodeBitmaps)
        {
            bmp.Dispose();
        }

        // Verify that the composite image was created
        if (!File.Exists(compositePath))
        {
            Console.WriteLine("Composite image not found.");
            return;
        }

        // Read barcodes from the composite image, processing at most three
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(compositePath, decodeType))
        {
            int processed = 0;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode: Text = {result.CodeText}, Type = {result.CodeTypeName}");
                processed++;
                if (processed >= 3)
                {
                    // Stop after processing three barcodes to limit overhead
                    break;
                }
            }

            if (processed == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
        }

        // Optionally clean up the temporary folder (commented out to allow inspection)
        // Directory.Delete(tempFolder, true);
    }
}