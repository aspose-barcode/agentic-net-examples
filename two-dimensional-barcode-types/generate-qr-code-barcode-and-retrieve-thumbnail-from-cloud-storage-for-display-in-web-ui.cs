using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string qrPath = Path.Combine(outputDir, "qr.png");
        string thumbPath = Path.Combine(outputDir, "qr_thumb.png");

        // Generate QR Code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Create thumbnail from generated QR Code
        if (File.Exists(qrPath))
        {
            using (var original = new Bitmap(qrPath))
            {
                int thumbWidth = 100;
                int thumbHeight = (int)(original.Height * thumbWidth / (float)original.Width);
                using (var thumbnail = new Bitmap(thumbWidth, thumbHeight))
                {
                    using (var graphics = Graphics.FromImage(thumbnail))
                    {
                        graphics.DrawImage(original, 0, 0, thumbWidth, thumbHeight);
                    }
                    thumbnail.Save(thumbPath, ImageFormat.Png);
                }
            }
            Console.WriteLine($"QR code saved to: {qrPath}");
            Console.WriteLine($"Thumbnail saved to: {thumbPath}");
        }
        else
        {
            Console.WriteLine("Failed to generate QR code image.");
        }
    }
}