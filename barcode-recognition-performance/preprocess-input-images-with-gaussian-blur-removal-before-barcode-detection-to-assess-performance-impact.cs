// Title: Gaussian Blur Preprocessing Impact on Barcode Detection
// Description: Demonstrates how applying a Gaussian blur to a barcode image affects detection performance using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader for detection. It illustrates typical scenarios where developers need to preprocess images (e.g., blur removal, noise reduction) before recognition to evaluate or improve accuracy and speed.
// Prompt: Preprocess input images with Gaussian blur removal before barcode detection to assess performance impact.
// Tags: barcode generation, barcode recognition, gaussian blur, image preprocessing, code128, performance measurement, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates Gaussian blur preprocessing on a barcode image and measures detection performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, applies Gaussian blur, and compares detection times with and without preprocessing.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for original and processed images
        string originalPath = Path.Combine(tempFolder, "original.png");
        string processedPath = Path.Combine(tempFolder, "processed.png");

        // Generate a sample barcode image (Code128)
        string codeText = "1234567890";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use default sizing (auto adapts to code length)
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Verify the original image exists
        if (!File.Exists(originalPath))
        {
            Console.WriteLine("Failed to create the original barcode image.");
            return;
        }

        // Detect barcode without preprocessing and measure time
        Stopwatch swNoPre = Stopwatch.StartNew();
        DetectBarcodes(originalPath);
        swNoPre.Stop();

        // Apply Gaussian blur (as a preprocessing step)
        using (var originalBitmap = new Bitmap(originalPath))
        {
            using (var blurredBitmap = ApplyGaussianBlur(originalBitmap))
            {
                blurredBitmap.Save(processedPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // Verify the processed image exists
        if (!File.Exists(processedPath))
        {
            Console.WriteLine("Failed to create the processed barcode image.");
            return;
        }

        // Detect barcode with preprocessing and measure time
        Stopwatch swPre = Stopwatch.StartNew();
        DetectBarcodes(processedPath);
        swPre.Stop();

        // Output performance comparison
        Console.WriteLine();
        Console.WriteLine($"Detection time without preprocessing: {swNoPre.ElapsedMilliseconds} ms");
        Console.WriteLine($"Detection time with Gaussian blur preprocessing: {swPre.ElapsedMilliseconds} ms");
    }

    // Reads barcodes from the specified image file and prints results
    static void DetectBarcodes(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image not found: {imagePath}");
            return;
        }

        try
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                bool anyFound = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Detected: {result.CodeText} (Type: {result.CodeTypeName})");
                }

                if (!anyFound)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
        {
            Console.WriteLine($"Failed to load image for reading: {ex.Message}");
        }
    }

    // Applies a simple 3x3 Gaussian blur to the input bitmap and returns a new bitmap
    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        // Define a 3x3 Gaussian kernel
        double[,] kernel = {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };
        double kernelSum = 16.0; // Sum of kernel elements

        int width = source.Width;
        int height = source.Height;

        // Create a new bitmap for the result
        Bitmap result = new Bitmap(width, height);

        // Process each pixel (skip borders for simplicity)
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                double r = 0, g = 0, b = 0;

                // Convolution with the kernel
                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color pixel = source.GetPixel(x + kx, y + ky);
                        double weight = kernel[ky + 1, kx + 1];
                        r += pixel.R * weight;
                        g += pixel.G * weight;
                        b += pixel.B * weight;
                    }
                }

                // Normalize and clamp
                int nr = Math.Min(255, Math.Max(0, (int)(r / kernelSum)));
                int ng = Math.Min(255, Math.Max(0, (int)(g / kernelSum)));
                int nb = Math.Min(255, Math.Max(0, (int)(b / kernelSum)));

                result.SetPixel(x, y, Color.FromArgb(nr, ng, nb));
            }
        }

        // Copy border pixels unchanged
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
}