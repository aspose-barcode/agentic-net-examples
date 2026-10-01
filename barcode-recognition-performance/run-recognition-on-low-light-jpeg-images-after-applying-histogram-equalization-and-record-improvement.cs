// Title: Barcode recognition improvement on low‑light images using histogram equalization
// Description: Demonstrates generating a QR code, simulating a low‑light JPEG, enhancing it with histogram equalization, and comparing barcode recognition results before and after enhancement.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and image manipulation classes (Bitmap, Color) to improve detection of barcodes in challenging lighting conditions. Developers often need to preprocess captured images—adjust brightness, apply contrast enhancement, or histogram equalization—to increase decoding success rates in real‑world scanning applications.
// Prompt: Run recognition on low‑light JPEG images after applying histogram equalization and record improvement.
// Tags: qr, low-light, histogram-equalization, barcode-recognition, image-preprocessing, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, low‑light simulation, histogram equalization, and recognition comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the workflow: generate barcode, darken image, enhance, and recognize.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeLowLightDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Paths for images
        string originalBarcodePath = Path.Combine(workFolder, "barcode_original.png");
        string lowLightPath = Path.Combine(workFolder, "barcode_lowlight.jpg");
        string enhancedPath = Path.Combine(workFolder, "barcode_enhanced.jpg");

        // 1. Generate a sample barcode
        GenerateSampleBarcode(originalBarcodePath);

        // 2. Simulate low‑light condition by darkening the image
        ApplyDarkening(originalBarcodePath, lowLightPath, 0.3f); // 30% brightness

        // 3. Apply histogram equalization to improve visibility
        ApplyHistogramEqualization(lowLightPath, enhancedPath);

        // 4. Recognize barcodes before and after enhancement
        Console.WriteLine("=== Recognition on low‑light image ===");
        RecognizeAndReport(lowLightPath);

        Console.WriteLine("\n=== Recognition on enhanced image ===");
        RecognizeAndReport(enhancedPath);

        // Cleanup (optional)
        // Directory.Delete(workFolder, true);
    }

    // Generates a simple QR code and saves it as PNG
    private static void GenerateSampleBarcode(string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a modest size
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    // Darkens an image by multiplying each RGB component by a factor (0..1)
    private static void ApplyDarkening(string inputPath, string outputPath, float factor)
    {
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (var bitmap = new Bitmap(inputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    int r = (int)(pixel.R * factor);
                    int g = (int)(pixel.G * factor);
                    int b = (int)(pixel.B * factor);
                    var darkPixel = Color.FromArgb(r, g, b);
                    bitmap.SetPixel(x, y, darkPixel);
                }
            }

            // Save as JPEG to emulate low‑light photo capture
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }

    // Performs histogram equalization on a JPEG image and saves the result
    private static void ApplyHistogramEqualization(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (var bitmap = new Bitmap(inputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int pixelCount = width * height;

            // Compute histogram of luminance (0‑255)
            int[] histogram = new int[256];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    // Convert to grayscale using luminance formula
                    int lum = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    histogram[lum]++;
                }
            }

            // Compute cumulative distribution function (CDF)
            int[] cdf = new int[256];
            cdf[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            // Build lookup table
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                // Normalize to [0,255]
                lut[i] = (byte)Math.Round(((double)(cdf[i] - cdf[0]) / (pixelCount - cdf[0])) * 255.0);
            }

            // Apply mapping
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    int lum = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    byte newLum = lut[lum];
                    // Preserve original color hue by scaling RGB proportionally
                    float scale = newLum / (float)lum;
                    if (float.IsNaN(scale) || float.IsInfinity(scale)) scale = 0f;
                    int r = Math.Clamp((int)(pixel.R * scale), 0, 255);
                    int g = Math.Clamp((int)(pixel.G * scale), 0, 255);
                    int b = Math.Clamp((int)(pixel.B * scale), 0, 255);
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            // Save enhanced image
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }

    // Reads barcodes from an image and prints details
    private static void RecognizeAndReport(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use all supported decode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Optional: set high‑performance quality preset
            reader.QualitySettings = QualitySettings.HighPerformance;

            BarCodeResult[] results = reader.ReadBarCodes();

            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            foreach (var result in results)
            {
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Symbology   : {result.CodeTypeName}");
                Console.WriteLine($"Quality (0‑100): {result.ReadingQuality}");
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}