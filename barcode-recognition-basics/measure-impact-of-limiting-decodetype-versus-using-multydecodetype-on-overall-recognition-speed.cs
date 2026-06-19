using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of barcode images, combining them into a single bitmap,
/// and measuring recognition performance using different decode type configurations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates Code128 and QR barcodes, combines them, and measures recognition times.
    /// </summary>
    static void Main()
    {
        // Prepare sample barcode images (Code128 and QR) in memory streams.
        using (var code128Stream = new MemoryStream())
        using (var qrStream = new MemoryStream())
        {
            // ----- Generate Code128 barcode and write to memory stream -----
            using (var generator128 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128_SAMPLE"))
            {
                generator128.Save(code128Stream, BarCodeImageFormat.Png);
            }
            // Reset stream position for reading.
            code128Stream.Position = 0;

            // ----- Generate QR barcode and write to memory stream -----
            using (var generatorQr = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generatorQr.Save(qrStream, BarCodeImageFormat.Png);
            }
            // Reset stream position for reading.
            qrStream.Position = 0;

            // ----- Load generated images into Bitmap objects -----
            using (var bmp128 = new Bitmap(code128Stream))
            using (var bmpQr = new Bitmap(qrStream))
            {
                // ----- Create a combined image that places the two barcodes side by side -----
                int padding = 20; // Space between the two barcodes.
                int combinedWidth = bmp128.Width + bmpQr.Width + padding;
                int combinedHeight = Math.Max(bmp128.Height, bmpQr.Height);

                using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
                {
                    // Draw the two barcodes onto the combined bitmap.
                    using (var graphics = Graphics.FromImage(combinedBmp))
                    {
                        graphics.Clear(Color.White);
                        graphics.DrawImage(bmp128, 0, 0, bmp128.Width, bmp128.Height);
                        graphics.DrawImage(bmpQr, bmp128.Width + padding, 0, bmpQr.Width, bmpQr.Height);
                    }

                    // ----- Measure recognition time using a single DecodeType (Code128 only) -----
                    long singleTime = MeasureRecognition(combinedBmp, DecodeType.Code128);

                    // ----- Measure recognition time using a MultiDecodeType (Code128 + QR) -----
                    long multiTime = MeasureRecognition(combinedBmp, new MultiDecodeType(DecodeType.Code128, DecodeType.QR));

                    // Output the measured times.
                    Console.WriteLine($"Recognition time with single DecodeType (Code128): {singleTime} ms");
                    Console.WriteLine($"Recognition time with MultiDecodeType (Code128 + QR): {multiTime} ms");
                }
            }
        }
    }

    /// <summary>
    /// Measures the time required to recognize barcodes in the provided image using the specified decode type.
    /// </summary>
    /// <param name="image">The bitmap containing barcodes to be recognized.</param>
    /// <param name="decodeType">The decode type configuration (single or multi) to use for recognition.</param>
    /// <returns>The elapsed time in milliseconds.</returns>
    static long MeasureRecognition(Bitmap image, BaseDecodeType decodeType)
    {
        var stopwatch = Stopwatch.StartNew();

        // Initialize the barcode reader with the image and decode type.
        using (var reader = new BarCodeReader(image, decodeType))
        {
            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detected barcode information (optional).
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}