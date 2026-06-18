using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcodes, combining them into a single image,
/// saving the result, and then recognizing the barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates three sample barcodes, merges them side‑by‑side,
    /// saves the combined image, and reads back the barcodes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Define barcode types and corresponding text values.
        // --------------------------------------------------------------------
        var barcodeInfos = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // --------------------------------------------------------------------
        // 2. Generate individual barcode images in memory.
        // --------------------------------------------------------------------
        var barcodeBitmaps = new Bitmap[barcodeInfos.Length];
        for (int i = 0; i < barcodeInfos.Length; i++)
        {
            // Create a generator for the current barcode definition.
            using (var generator = new BarcodeGenerator(barcodeInfos[i].type, barcodeInfos[i].text))
            {
                // Save the barcode to a memory stream as PNG.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.
                    barcodeBitmaps[i] = new Bitmap(ms); // Load bitmap from stream.
                }
            }
        }

        // --------------------------------------------------------------------
        // 3. Calculate dimensions for the combined image (horizontal layout).
        // --------------------------------------------------------------------
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (var bmp in barcodeBitmaps)
        {
            totalWidth += bmp.Width;               // Accumulate widths.
            if (bmp.Height > maxHeight) maxHeight = bmp.Height; // Track tallest bitmap.
        }

        // --------------------------------------------------------------------
        // 4. Create a new bitmap to hold all barcodes side by side.
        // --------------------------------------------------------------------
        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                int offsetX = 0; // Horizontal offset for drawing each barcode.
                foreach (var bmp in barcodeBitmaps)
                {
                    // Draw the current barcode at the current offset.
                    graphics.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                    offsetX += bmp.Width; // Move offset for next barcode.
                }
            }

            // ----------------------------------------------------------------
            // 5. Save the combined image to disk.
            // ----------------------------------------------------------------
            const string combinedPath = "combined.png";
            combined.Save(combinedPath, ImageFormat.Png);
            Console.WriteLine($"Combined barcode image saved to {combinedPath}");

            // ----------------------------------------------------------------
            // 6. Recognize barcodes from the saved combined image.
            // ----------------------------------------------------------------
            using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();
                int limit = Math.Min(3, results.Length); // Process up to three results.
                Console.WriteLine($"Processing up to {limit} barcodes:");
                for (int i = 0; i < limit; i++)
                {
                    var result = results[i];
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // 7. Release resources held by individual barcode bitmaps.
        // --------------------------------------------------------------------
        foreach (var bmp in barcodeBitmaps)
        {
            bmp.Dispose();
        }
    }
}