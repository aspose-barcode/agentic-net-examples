// Title: Barcode Recognition with Gaussian Noise Test
// Description: Generates a Code128 barcode, adds Gaussian noise at various levels, and measures recognition success rates.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, manipulate images with Aspose.Drawing, and employ BarCodeReader to decode barcodes under adverse conditions. Developers often need to test robustness of barcode scanning in noisy environments, making this pattern useful for quality assurance and image preprocessing scenarios.
// Prompt: Test recognition on images with added Gaussian noise at varying signal‑to‑noise ratios and log success rates.
// Tags: barcode symbology, recognition, gaussian noise, code128, aspose.barcode, image processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, noise addition, and recognition success measurement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a base barcode, creates noisy variants, attempts recognition, and logs success rates.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Base barcode parameters
        string baseText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string baseImagePath = Path.Combine(tempFolder, "base.png");

        // Generate the base barcode image and save as PNG
        using (var generator = new BarcodeGenerator(encodeType, baseText))
        {
            generator.Save(baseImagePath, BarCodeImageFormat.Png);
        }

        // Define noise levels (standard deviation) to test
        float[] noiseLevels = new float[] { 0f, 5f, 10f, 20f, 30f };
        int attemptsPerLevel = 5;
        var random = new Random();

        // Iterate over each noise level
        foreach (float noiseStdDev in noiseLevels)
        {
            int successCount = 0;

            // Perform multiple attempts per noise level
            for (int attempt = 1; attempt <= attemptsPerLevel; attempt++)
            {
                string noisyPath = Path.Combine(tempFolder, $"noisy_{noiseStdDev}_{attempt}.png");

                // Load the base image, add Gaussian noise, and save the noisy image
                using (var bitmap = new Bitmap(baseImagePath))
                {
                    AddGaussianNoise(bitmap, noiseStdDev, random);
                    using (var stream = new FileStream(noisyPath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                    }
                }

                // Attempt to read the barcode from the noisy image
                BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                try
                {
                    using (var reader = new BarCodeReader(noisyPath, decodeType))
                    {
                        var results = reader.ReadBarCodes();
                        if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                        {
                            successCount++;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // Image loading failed; skip this attempt
                }
            }

            // Log the success rate for the current noise level
            Console.WriteLine($"NoiseStdDev {noiseStdDev}: {successCount}/{attemptsPerLevel} successes");
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    /// <summary>
    /// Adds Gaussian noise to a bitmap image.
    /// </summary>
    /// <param name="bitmap">The bitmap to modify.</param>
    /// <param name="stdDev">Standard deviation of the Gaussian noise.</param>
    /// <param name="random">Random number generator.</param>
    static void AddGaussianNoise(Bitmap bitmap, float stdDev, Random random)
    {
        if (stdDev <= 0f) return;

        int width = bitmap.Width;
        int height = bitmap.Height;

        // Process each pixel and apply noise to RGB channels
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color original = bitmap.GetPixel(x, y);

                byte r = AddNoiseToChannel(original.R, stdDev, random);
                byte g = AddNoiseToChannel(original.G, stdDev, random);
                byte b = AddNoiseToChannel(original.B, stdDev, random);
                byte a = original.A; // Preserve alpha channel

                Color noisy = Color.FromArgb(a, r, g, b);
                bitmap.SetPixel(x, y, noisy);
            }
        }
    }

    /// <summary>
    /// Adds Gaussian noise to a single color channel value.
    /// </summary>
    /// <param name="value">Original channel value (0‑255).</param>
    /// <param name="stdDev">Standard deviation of the noise.</param>
    /// <param name="random">Random number generator.</param>
    /// <returns>Noisy channel value clamped to 0‑255.</returns>
    static byte AddNoiseToChannel(byte value, float stdDev, Random random)
    {
        // Box‑Muller transform to generate normally distributed random value
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        double noise = randStdNormal * stdDev;

        int newVal = (int)Math.Round(value + noise);
        if (newVal < 0) newVal = 0;
        if (newVal > 255) newVal = 255;
        return (byte)newVal;
    }
}