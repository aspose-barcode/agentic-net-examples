// Title: Barcode detection with Gaussian noise and MinimalXDimension filtering
// Description: Demonstrates generating a Code128 barcode, adding Gaussian noise, and recognizing it using MinimalXDimension filtering to improve detection on noisy images.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: barcode, code128, gaussian noise, minimalxdimension, qualitysettings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates a barcode, adds Gaussian noise, and attempts to read it using MinimalXDimension filtering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, adds noise, and reads it with configured quality settings.
    /// </summary>
    static void Main()
    {
        // Step 1: Create a simple Code128 barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test12345"))
        {
            // Use interpolation mode for flexible sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image into a bitmap.
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Step 2: Add Gaussian noise to the bitmap.
                AddGaussianNoise(barcodeBitmap, 20.0); // Standard deviation of 20.

                // Step 3: Recognize the noisy barcode with MinimalXDimension filtering.
                using (var reader = new BarCodeReader(barcodeBitmap, DecodeType.AllSupportedTypes))
                {
                    // Configure quality settings for robust detection.
                    reader.QualitySettings = QualitySettings.HighQuality;
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 5f; // pixels
                    reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform recognition.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output results.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                            Console.WriteLine($"Region: {result.Region.Rectangle}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }

    // Adds Gaussian noise to each pixel of the bitmap.
    private static void AddGaussianNoise(Bitmap bitmap, double sigma)
    {
        Random rand = new Random();

        int width = bitmap.Width;
        int height = bitmap.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Get original pixel color.
                Color original = bitmap.GetPixel(x, y);

                // Generate Gaussian noise for each channel.
                int noiseR = (int)Math.Round(NextGaussian(rand) * sigma);
                int noiseG = (int)Math.Round(NextGaussian(rand) * sigma);
                int noiseB = (int)Math.Round(NextGaussian(rand) * sigma);

                // Apply noise and clamp to [0,255].
                int r = Clamp(original.R + noiseR, 0, 255);
                int g = Clamp(original.G + noiseG, 0, 255);
                int b = Clamp(original.B + noiseB, 0, 255);

                // Set the new pixel color.
                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
    }

    // Generates a normally distributed random value using Box-Muller transform.
    private static double NextGaussian(Random rand)
    {
        // Generate two uniform random numbers in (0,1].
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();

        // Apply Box-Muller transform.
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        return randStdNormal;
    }

    // Helper to clamp integer values.
    private static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}