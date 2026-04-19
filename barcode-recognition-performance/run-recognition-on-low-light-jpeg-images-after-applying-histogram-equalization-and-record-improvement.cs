using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        string[] jpegFiles = Directory.GetFiles(imagesFolder, "*.jpg");
        if (jpegFiles.Length == 0)
        {
            Console.WriteLine("No JPEG images found.");
            return;
        }

        foreach (string filePath in jpegFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing: {Path.GetFileName(filePath)}");

            using (Bitmap originalBmp = new Bitmap(filePath))
            {
                using (Bitmap equalizedBmp = EqualizeHistogram(originalBmp))
                {
                    int originalCount = RecognizeBarcodes(originalBmp);
                    int equalizedCount = RecognizeBarcodes(equalizedBmp);

                    Console.WriteLine($"  Original detection count : {originalCount}");
                    Console.WriteLine($"  After equalization count : {equalizedCount}");
                    Console.WriteLine($"  Improvement               : {equalizedCount - originalCount}");
                }
            }
        }
    }

    private static Bitmap EqualizeHistogram(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;

        Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        int[] histogram = new int[256];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color clr = source.GetPixel(x, y);
                int lum = (clr.R + clr.G + clr.B) / 3;
                histogram[lum]++;
            }
        }

        int[] cdf = new int[256];
        cdf[0] = histogram[0];
        for (int i = 1; i < 256; i++)
        {
            cdf[i] = cdf[i - 1] + histogram[i];
        }

        int totalPixels = width * height;
        byte[] lut = new byte[256];
        for (int i = 0; i < 256; i++)
        {
            lut[i] = (byte)Math.Round(((float)(cdf[i] - cdf[0]) / (totalPixels - cdf[0])) * 255f);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color clr = source.GetPixel(x, y);
                int lum = (clr.R + clr.G + clr.B) / 3;
                byte newVal = lut[lum];
                float ratio = lum == 0 ? 0f : (float)newVal / lum;
                int r = Math.Clamp((int)(clr.R * ratio), 0, 255);
                int g = Math.Clamp((int)(clr.G * ratio), 0, 255);
                int b = Math.Clamp((int)(clr.B * ratio), 0, 255);
                result.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        return result;
    }

    private static int RecognizeBarcodes(Bitmap bmp)
    {
        int count = 0;
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;
            reader.SetBarCodeImage(bmp);
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                count++;
                Console.WriteLine($"    Detected: Type={result.CodeTypeName}, Text={result.CodeText}, Confidence={result.Confidence}");
            }
        }
        return count;
    }
}