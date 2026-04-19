using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Generate QR code image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to a PNG file
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
                bitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"QR code saved to: {outputPath}");
            }
        }
    }
}