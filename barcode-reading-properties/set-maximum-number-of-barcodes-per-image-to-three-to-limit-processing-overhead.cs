// Title: Barcode generation, combination, and limited decoding demonstration
// Description: This example creates three different barcodes, merges them into a single image, and then reads up to three barcodes from that image to limit processing overhead.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: barcode generation, barcode recognition, limit processing, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcodes, combining them into one image,
/// and reading a limited number of barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates three barcodes, combines them,
    /// saves the combined image, and reads up to three barcodes from it.
    /// </summary>
    static void Main()
    {
        // Generate three sample barcodes of different symbologies
        Bitmap bmp1;
        Bitmap bmp2;
        Bitmap bmp3;

        using (var gen1 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            bmp1 = gen1.GenerateBarCodeImage();
        }

        using (var gen2 = new BarcodeGenerator(EncodeTypes.QR, "ABC"))
        {
            bmp2 = gen2.GenerateBarCodeImage();
        }

        using (var gen3 = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            bmp3 = gen3.GenerateBarCodeImage();
        }

        // Combine the three barcode images side by side into a single bitmap
        int totalWidth = bmp1.Width + bmp2.Width + bmp3.Width;
        int maxHeight = Math.Max(bmp1.Height, Math.Max(bmp2.Height, bmp3.Height));

        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                // Draw each barcode at the appropriate horizontal offset
                graphics.DrawImage(bmp1, 0, 0);
                graphics.DrawImage(bmp2, bmp1.Width, 0);
                graphics.DrawImage(bmp3, bmp1.Width + bmp2.Width, 0);
            }

            // Save the combined image to disk as PNG
            string combinedPath = "combined.png";
            combined.Save(combinedPath, ImageFormat.Png);
        }

        // Release resources held by the individual barcode bitmaps
        bmp1.Dispose();
        bmp2.Dispose();
        bmp3.Dispose();

        // Verify that the combined image file exists before attempting to read it
        string imagePath = "combined.png";
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read barcodes from the combined image, processing at most three to limit overhead
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();
            int processed = 0;
            foreach (var result in results)
            {
                if (processed >= 3)
                    break;

                Console.WriteLine($"Barcode {processed + 1}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                processed++;
            }
        }
    }
}