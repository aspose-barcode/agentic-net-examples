using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Create a high‑resolution barcode image (300 dpi)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            generator.Parameters.Resolution = 300f; // high resolution

            // Generate the barcode bitmap
            using (var highResBitmap = generator.GenerateBarCodeImage())
            {
                // Determine scaled size (e.g., 50 % of original)
                int scaledWidth = highResBitmap.Width / 2;
                int scaledHeight = highResBitmap.Height / 2;

                // Create a new bitmap for the scaled image
                using (var scaledBitmap = new Bitmap(scaledWidth, scaledHeight))
                {
                    // Draw the high‑resolution image onto the scaled bitmap
                    using (var graphics = Graphics.FromImage(scaledBitmap))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(highResBitmap, 0, 0, scaledWidth, scaledHeight);
                    }

                    // Read the barcode from the scaled image using high‑performance settings
                    using (var reader = new BarCodeReader(scaledBitmap, DecodeType.Code128))
                    {
                        reader.QualitySettings = QualitySettings.HighPerformance;
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine("Detected CodeText: " + result.CodeText);
                        }
                    }
                }
            }
        }
    }
}