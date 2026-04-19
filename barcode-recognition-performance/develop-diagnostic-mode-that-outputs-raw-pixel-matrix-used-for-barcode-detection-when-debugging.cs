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
        // Create a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Output raw pixel matrix (1 = dark pixel, 0 = light pixel)
                int width = barcodeImage.Width;
                int height = barcodeImage.Height;

                Console.WriteLine($"Barcode image size: {width}x{height}");
                Console.WriteLine("Raw pixel matrix (1 = black, 0 = white):");

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = barcodeImage.GetPixel(x, y);
                        // Simple luminance check
                        int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                        Console.Write(luminance < 128 ? "1" : "0");
                    }
                    Console.WriteLine();
                }

                // Optional: demonstrate detection using BarCodeReader
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}