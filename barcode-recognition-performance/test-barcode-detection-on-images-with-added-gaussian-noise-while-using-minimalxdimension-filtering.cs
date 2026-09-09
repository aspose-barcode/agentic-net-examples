// Title: Detect QR barcode in a Gaussian‑noisy image using MinimalXDimension filtering
// Description: This example generates a QR code, adds Gaussian noise to the image, and then detects the barcode using Aspose.BarCode with MinimalXDimension filtering.
// Category-Description: Demonstrates Aspose.BarCode image preprocessing and high‑performance barcode recognition. It covers generating barcodes (BarcodeGenerator), image manipulation (Aspose.Drawing), adding noise, and configuring QualitySettings (HighPerformance, XDimensionMode.UseMinimalXDimension). Ideal for developers needing robust detection on degraded images, such as scanned documents or camera captures.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: barcode, qr, gaussian noise, minimalxdimension, detection, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, noise addition, and detection with MinimalXDimension filtering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, adds Gaussian noise, and attempts to read it using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the clean and noisy barcode images
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string noisyPath = Path.Combine(tempFolder, "barcode_noisy.png");

        // Generate a simple QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Load the clean barcode image and add Gaussian noise pixel by pixel
        using (var originalImage = (Bitmap)Image.FromFile(barcodePath))
        {
            int width = originalImage.Width;
            int height = originalImage.Height;
            var noisyImage = new Bitmap(width, height, originalImage.PixelFormat);

            Random rng = new Random();
            double sigma = 20.0; // Standard deviation for Gaussian noise

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color origColor = originalImage.GetPixel(x, y);

                    // Generate Gaussian noise for each color channel
                    double noiseR = GenerateGaussian(rng, sigma);
                    double noiseG = GenerateGaussian(rng, sigma);
                    double noiseB = GenerateGaussian(rng, sigma);

                    // Apply noise and clamp the result to valid byte range
                    int r = Clamp(origColor.R + (int)Math.Round(noiseR), 0, 255);
                    int g = Clamp(origColor.G + (int)Math.Round(noiseG), 0, 255);
                    int b = Clamp(origColor.B + (int)Math.Round(noiseB), 0, 255);

                    Color noisyColor = Color.FromArgb(r, g, b);
                    noisyImage.SetPixel(x, y, noisyColor);
                }
            }

            // Save the noisy image to disk
            noisyImage.Save(noisyPath, ImageFormat.Png);
            noisyImage.Dispose();
        }

        // Initialize a barcode reader for the noisy image with all supported types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(noisyPath, decodeType))
        {
            // Use the high‑performance preset to speed up processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Enable MinimalXDimension mode to improve detection on degraded images
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f; // Minimum X‑dimension in pixels

            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcodes were detected in the noisy image.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            File.Delete(noisyPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    // Generates Gaussian-distributed random noise using the Box‑Muller transform
    static double GenerateGaussian(Random rng, double sigma)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return randStdNormal * sigma;
    }

    // Clamps an integer value to the specified inclusive range
    static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}