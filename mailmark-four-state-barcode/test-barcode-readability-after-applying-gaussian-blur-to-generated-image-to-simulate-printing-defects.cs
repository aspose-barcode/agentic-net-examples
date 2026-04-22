using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for temporary images
        string originalPath = Path.Combine(Path.GetTempPath(), "barcode_original.png");
        string blurredPath = Path.Combine(Path.GetTempPath(), "barcode_blurred.png");

        // Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Save original image
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Generate bitmap for processing
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Apply a simple Gaussian-like blur (radius 1)
                using (Bitmap blurredBitmap = ApplyGaussianBlur(originalBitmap, 1, 1.0))
                {
                    // Save blurred image
                    blurredBitmap.Save(blurredPath, ImageFormat.Png);
                }
            }
        }

        // Verify that the blurred image exists
        if (!File.Exists(blurredPath))
        {
            Console.WriteLine("Blurred image was not created.");
            return;
        }

        // Read the blurred barcode and output confidence
        using (var reader = new BarCodeReader(blurredPath, DecodeType.Code128))
        {
            // Use high quality settings to improve detection of damaged barcodes
            reader.QualitySettings = QualitySettings.HighQuality;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Type: " + result.CodeTypeName);
                Console.WriteLine("Detected Text: " + result.CodeText);
                Console.WriteLine("Confidence: " + result.Confidence);
                Console.WriteLine("Reading Quality: " + result.ReadingQuality);
            }
        }

        // Clean up temporary files (optional)
        try { File.Delete(originalPath); } catch { }
        try { File.Delete(blurredPath); } catch { }
    }

    // Simple Gaussian blur implementation using a 3x3 kernel
    static Bitmap ApplyGaussianBlur(Bitmap source, int radius, double sigma)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap blurred = new Bitmap(width, height);

        // Precompute Gaussian kernel
        double[,] kernel = new double[3, 3];
        double sum = 0;
        int kCenter = 1;
        for (int y = -kCenter; y <= kCenter; y++)
        {
            for (int x = -kCenter; x <= kCenter; x++)
            {
                double exponent = -(x * x + y * y) / (2 * sigma * sigma);
                double value = Math.Exp(exponent);
                kernel[y + kCenter, x + kCenter] = value;
                sum += value;
            }
        }
        // Normalize kernel
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                kernel[y, x] /= sum;

        // Apply convolution
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                double r = 0, g = 0, b = 0, a = 0;
                for (int ky = -kCenter; ky <= kCenter; ky++)
                {
                    int py = Math.Min(height - 1, Math.Max(0, y + ky));
                    for (int kx = -kCenter; kx <= kCenter; kx++)
                    {
                        int px = Math.Min(width - 1, Math.Max(0, x + kx));
                        Color pixel = source.GetPixel(px, py);
                        double weight = kernel[ky + kCenter, kx + kCenter];
                        a += pixel.A * weight;
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }
                blurred.SetPixel(x, y, Color.FromArgb(
                    (int)Math.Round(a),
                    (int)Math.Round(r),
                    (int)Math.Round(g),
                    (int)Math.Round(b)));
            }
        }

        return blurred;
    }
}