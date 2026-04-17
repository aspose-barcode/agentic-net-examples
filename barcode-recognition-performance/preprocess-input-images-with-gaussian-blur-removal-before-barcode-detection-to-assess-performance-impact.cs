using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    // Apply a 3x3 Gaussian blur to a bitmap and return a new bitmap
    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        float[,] kernel = {
            { 1f / 16f, 2f / 16f, 1f / 16f },
            { 2f / 16f, 4f / 16f, 2f / 16f },
            { 1f / 16f, 2f / 16f, 1f / 16f }
        };

        int width = source.Width;
        int height = source.Height;
        Bitmap result = new Bitmap(width, height);

        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                float r = 0, g = 0, b = 0;
                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color pixel = source.GetPixel(x + kx, y + ky);
                        float weight = kernel[ky + 1, kx + 1];
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }
                result.SetPixel(x, y, Color.FromArgb(
                    Math.Clamp((int)r, 0, 255),
                    Math.Clamp((int)g, 0, 255),
                    Math.Clamp((int)b, 0, 255)));
            }
        }

        // Copy edge pixels unchanged
        for (int x = 0; x < width; x++)
        {
            result.SetPixel(x, 0, source.GetPixel(x, 0));
            result.SetPixel(x, height - 1, source.GetPixel(x, height - 1));
        }
        for (int y = 0; y < height; y++)
        {
            result.SetPixel(0, y, source.GetPixel(0, y));
            result.SetPixel(width - 1, y, source.GetPixel(width - 1, y));
        }

        return result;
    }

    // Perform unsharp masking to reduce blur (simple implementation)
    static Bitmap ApplyUnsharpMask(Bitmap original, float amount = 1.5f)
    {
        using (Bitmap blurred = ApplyGaussianBlur(original))
        {
            int width = original.Width;
            int height = original.Height;
            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color orig = original.GetPixel(x, y);
                    Color blur = blurred.GetPixel(x, y);

                    int r = Math.Clamp((int)(orig.R + amount * (orig.R - blur.R)), 0, 255);
                    int g = Math.Clamp((int)(orig.G + amount * (orig.G - blur.G)), 0, 255);
                    int b = Math.Clamp((int)(orig.B + amount * (orig.B - blur.B)), 0, 255);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return result;
        }
    }

    // Recognize barcode from a bitmap and output result
    static void RecognizeAndPrint(string label, Bitmap image)
    {
        using (var reader = new BarCodeReader(image, DecodeType.Code128, DecodeType.QR))
        {
            // Use HighQuality preset to improve detection on degraded images
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{label} - Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
            }

            if (reader.FoundCount == 0)
            {
                Console.WriteLine($"{label} - No barcode detected.");
            }
        }
    }

    static void Main()
    {
        const string outputDir = "output";
        Directory.CreateDirectory(outputDir);

        string barcodePath = Path.Combine(outputDir, "barcode.png");
        string blurredPath = Path.Combine(outputDir, "barcode_blurred.png");
        string processedPath = Path.Combine(outputDir, "barcode_processed.png");

        // 1. Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath);
        }

        // Verify the generated file exists
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // 2. Load the generated barcode image
        using (Bitmap originalBmp = new Bitmap(barcodePath))
        {
            // 3. Create a blurred version to simulate a low‑quality capture
            using (Bitmap blurredBmp = ApplyGaussianBlur(originalBmp))
            {
                blurredBmp.Save(blurredPath, ImageFormat.Png);
            }

            // 4. Load the blurred image for processing
            using (Bitmap blurredBmp = new Bitmap(blurredPath))
            {
                // 5. Apply unsharp mask (Gaussian blur removal)
                using (Bitmap processedBmp = ApplyUnsharpMask(blurredBmp))
                {
                    processedBmp.Save(processedPath, ImageFormat.Png);

                    // 6. Recognize barcodes from original, blurred, and processed images
                    RecognizeAndPrint("Original", originalBmp);
                    RecognizeAndPrint("Blurred", blurredBmp);
                    RecognizeAndPrint("Processed", processedBmp);
                }
            }
        }
    }
}