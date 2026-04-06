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
        // 1. Generate a barcode image (Code128, text "123456")
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (Bitmap originalBarcode = generator.GenerateBarCodeImage())
            {
                // 2. Create a low‑light version by darkening the image
                using (Bitmap lowLight = DarkenImage(originalBarcode, 0.2f))
                {
                    // Save low‑light image for reference (optional)
                    lowLight.Save("lowlight.jpg", ImageFormat.Jpeg);

                    // 3. Recognize low‑light image with default quality
                    var lowResult = RecognizeBarcode("lowlight.jpg", QualitySettings.NormalQuality);

                    // 4. Apply histogram equalization to improve visibility
                    using (Bitmap equalized = HistogramEqualization(lowLight))
                    {
                        // Save equalized image for reference (optional)
                        equalized.Save("equalized.jpg", ImageFormat.Jpeg);

                        // 5. Recognize equalized image with high‑quality preset
                        var highResult = RecognizeBarcode(equalized, QualitySettings.HighQuality);

                        // 6. Output comparison
                        Console.WriteLine("=== Recognition Comparison ===");
                        Console.WriteLine("Low‑light image:");
                        PrintResult(lowResult);
                        Console.WriteLine("\nAfter histogram equalization:");
                        PrintResult(highResult);
                    }
                }
            }
        }
    }

    // Darkens an image by multiplying each channel with the given factor (0..1)
    private static Bitmap DarkenImage(Bitmap source, float factor)
    {
        Bitmap result = new Bitmap(source.Width, source.Height, source.PixelFormat);
        using (Graphics g = Graphics.FromImage(result))
        {
            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height));
        }

        for (int y = 0; y < result.Height; y++)
        {
            for (int x = 0; x < result.Width; x++)
            {
                Color c = result.GetPixel(x, y);
                int r = (int)(c.R * factor);
                int g = (int)(c.G * factor);
                int b = (int)(c.B * factor);
                result.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
        return result;
    }

    // Performs simple histogram equalization on a bitmap (grayscale output)
    private static Bitmap HistogramEqualization(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        int total = width * height;
        int[] histogram = new int[256];

        // Build histogram based on luminance
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = source.GetPixel(x, y);
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                histogram[lum]++;
            }
        }

        // Compute cumulative distribution
        int[] cdf = new int[256];
        cdf[0] = histogram[0];
        for (int i = 1; i < 256; i++)
            cdf[i] = cdf[i - 1] + histogram[i];

        // Find first non‑zero cdf value
        int cdfMin = 0;
        for (int i = 0; i < 256; i++)
        {
            if (cdf[i] != 0)
            {
                cdfMin = cdf[i];
                break;
            }
        }

        // Build lookup table
        byte[] lut = new byte[256];
        for (int i = 0; i < 256; i++)
        {
            float val = (float)(cdf[i] - cdfMin) / (total - cdfMin);
            lut[i] = (byte)Math.Round(val * 255);
        }

        // Apply LUT to create equalized image (grayscale)
        Bitmap equalized = new Bitmap(width, height, source.PixelFormat);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = source.GetPixel(x, y);
                int lum = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                byte newVal = lut[lum];
                equalized.SetPixel(x, y, Color.FromArgb(newVal, newVal, newVal));
            }
        }
        return equalized;
    }

    // Recognizes a barcode from a file path using the specified quality preset
    private static BarCodeResult RecognizeBarcode(string filePath, QualitySettings preset)
    {
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128, DecodeType.Code39, DecodeType.QR))
        {
            reader.QualitySettings = preset;
            foreach (BarCodeResult result in reader.ReadBarCodes())
                return result; // Return first result (if any)
        }
        return null;
    }

    // Overload to recognize from an in‑memory bitmap
    private static BarCodeResult RecognizeBarcode(Bitmap bitmap, QualitySettings preset)
    {
        using (var reader = new BarCodeReader())
        {
            reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.Code39, DecodeType.QR);
            reader.SetBarCodeImage(bitmap);
            reader.QualitySettings = preset;
            foreach (BarCodeResult result in reader.ReadBarCodes())
                return result;
        }
        return null;
    }

    // Helper to print recognition result
    private static void PrintResult(BarCodeResult result)
    {
        if (result == null)
        {
            Console.WriteLine("No barcode detected.");
        }
        else
        {
            Console.WriteLine($"Code Text   : {result.CodeText}");
            Console.WriteLine($"Code Type   : {result.CodeTypeName}");
            Console.WriteLine($"Confidence  : {result.Confidence}");
        }
    }
}