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
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                using (Bitmap originalBitmap = new Bitmap(ms))
                {
                    Console.WriteLine("Detecting barcode in original image:");
                    DetectAndPrint(originalBitmap);

                    using (Bitmap blurredBitmap = ApplyGaussianBlur(originalBitmap))
                    {
                        Console.WriteLine("\nDetecting barcode in blurred image:");
                        DetectAndPrint(blurredBitmap);
                    }
                }
            }
        }
    }

    static void DetectAndPrint(Bitmap bitmap)
    {
        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            reader.QualitySettings = QualitySettings.HighPerformance;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            if (reader.FoundCount == 0)
            {
                Console.WriteLine("  No barcode detected.");
            }
        }
    }

    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        float[,] kernel = {
            { 1f / 16f, 2f / 16f, 1f / 16f },
            { 2f / 16f, 4f / 16f, 2f / 16f },
            { 1f / 16f, 2f / 16f, 1f / 16f }
        };

        int width = source.Width;
        int height = source.Height;
        Bitmap blurred = new Bitmap(width, height, source.PixelFormat);

        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                float sumR = 0f, sumG = 0f, sumB = 0f;

                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color neighbor = source.GetPixel(x + kx, y + ky);
                        float weight = kernel[ky + 1, kx + 1];
                        sumR += neighbor.R * weight;
                        sumG += neighbor.G * weight;
                        sumB += neighbor.B * weight;
                    }
                }

                int r = Math.Clamp((int)Math.Round(sumR), 0, 255);
                int g = Math.Clamp((int)Math.Round(sumG), 0, 255);
                int b = Math.Clamp((int)Math.Round(sumB), 0, 255);
                blurred.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        for (int x = 0; x < width; x++)
        {
            blurred.SetPixel(x, 0, source.GetPixel(x, 0));
            blurred.SetPixel(x, height - 1, source.GetPixel(x, height - 1));
        }

        for (int y = 0; y < height; y++)
        {
            blurred.SetPixel(0, y, source.GetPixel(0, y));
            blurred.SetPixel(width - 1, y, source.GetPixel(width - 1, y));
        }

        return blurred;
    }
}