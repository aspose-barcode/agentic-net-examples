// Title: Gaussian Blur Effect on Barcode Readability
// Description: Generates a Code128 barcode, applies a Gaussian blur to simulate printing defects, and attempts to read the blurred barcode.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create a barcode image, Aspose.Drawing for image manipulation, and BarCodeReader to decode the result. Typical scenarios include testing barcode robustness against printing imperfections, image processing pipelines, and quality assurance of barcode scanning systems.
// Prompt: Test barcode readability after applying Gaussian blur to the generated image to simulate printing defects.
// Tags: code128, barcode generation, barcode recognition, gaussian blur, image processing, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, applying a Gaussian blur, and reading the blurred image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, blurs it, and attempts to decode it.
    /// </summary>
    static void Main()
    {
        // Create a Code128 barcode with the value "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode image to a memory stream in PNG format
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalStream.Position = 0; // Reset stream position for reading

                // Load the PNG image into a Bitmap for processing
                using (var originalBitmap = (Bitmap)Image.FromStream(originalStream))
                {
                    // Apply a simple 3x3 Gaussian blur to simulate printing defects
                    using (var blurredBitmap = ApplyGaussianBlur(originalBitmap))
                    {
                        // Store the blurred image in another memory stream
                        using (var blurredStream = new MemoryStream())
                        {
                            blurredBitmap.Save(blurredStream, ImageFormat.Png);
                            blurredStream.Position = 0; // Reset for barcode reading

                            // Initialize a barcode reader that supports all barcode types
                            using (var reader = new BarCodeReader(blurredStream, DecodeType.AllSupportedTypes))
                            {
                                // Allow detection of slightly damaged or imperfect barcodes
                                reader.QualitySettings.AllowIncorrectBarcodes = true;

                                bool found = false;
                                // Iterate through all detected barcodes in the blurred image
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                                    Console.WriteLine($"Detected Code Text: {result.CodeText}");
                                    found = true;
                                }

                                if (!found)
                                {
                                    Console.WriteLine("No barcode could be detected in the blurred image.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Simple 3x3 Gaussian blur implementation (kernel 1 2 1 / 2 4 2 / 1 2 1)
    private static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        var blurred = new Bitmap(width, height);

        // Kernel weights for Gaussian blur
        int[,] kernel = new int[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
        int divisor = 16;

        // Process interior pixels (exclude edges)
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                int sumR = 0, sumG = 0, sumB = 0;

                // Apply kernel to surrounding pixels
                for (int ky = -1; ky <= 1; ky++)
                {
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        Color pixel = source.GetPixel(x + kx, y + ky);
                        int weight = kernel[ky + 1, kx + 1];
                        sumR += pixel.R * weight;
                        sumG += pixel.G * weight;
                        sumB += pixel.B * weight;
                    }
                }

                // Normalize and clamp color values
                int r = Math.Clamp(sumR / divisor, 0, 255);
                int g = Math.Clamp(sumG / divisor, 0, 255);
                int b = Math.Clamp(sumB / divisor, 0, 255);
                blurred.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        // Copy edge pixels unchanged to preserve image dimensions
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