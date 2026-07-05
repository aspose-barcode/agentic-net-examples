// Title: Barcode recognition under Gaussian noise with success rate logging
// Description: Demonstrates generating a QR barcode, adding Gaussian noise at various SNR levels, and measuring recognition success rates.
// Prompt: Test recognition on images with added Gaussian noise at varying signal‑to‑noise ratios and log success rates.
// Tags: qr, barcode, recognition, gaussian noise, success rate, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR barcode, applies Gaussian noise at different
/// signal‑to‑noise ratios, and logs the recognition success rates.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR barcode image in memory and returns both the bitmap and the original text.
    /// </summary>
    /// <returns>A tuple containing the generated <see cref="Bitmap"/> and the barcode text.</returns>
    static (Bitmap bitmap, string text) GenerateBarcode()
    {
        string barcodeText = "Test12345";
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, barcodeText))
        {
            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                var bmp = new Bitmap(ms);
                // Return bitmap (caller must dispose) and the original text.
                return (bmp, barcodeText);
            }
        }
    }

    /// <summary>
    /// Adds Gaussian noise to a bitmap. The <paramref name="sigma"/> parameter controls the noise intensity.
    /// </summary>
    /// <param name="source">Source bitmap to which noise will be added.</param>
    /// <param name="sigma">Standard deviation of the Gaussian noise.</param>
    /// <returns>A new <see cref="Bitmap"/> containing the noisy image.</returns>
    static Bitmap AddGaussianNoise(Bitmap source, double sigma)
    {
        // Clone source to avoid modifying the original bitmap.
        var noisy = (Bitmap)source.Clone();
        var rand = new Random();

        // Helper to generate a Gaussian-distributed value using the Box‑Muller transform.
        double NextGaussian()
        {
            double u1 = 1.0 - rand.NextDouble(); // avoid 0
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                  Math.Sin(2.0 * Math.PI * u2);
            return randStdNormal;
        }

        // Apply noise to each pixel channel.
        for (int y = 0; y < noisy.Height; y++)
        {
            for (int x = 0; x < noisy.Width; x++)
            {
                var pixel = noisy.GetPixel(x, y);
                int r = Clamp(pixel.R + (int)(sigma * NextGaussian()));
                int g = Clamp(pixel.G + (int)(sigma * NextGaussian()));
                int b = Clamp(pixel.B + (int)(sigma * NextGaussian()));
                noisy.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
        return noisy;
    }

    /// <summary>
    /// Clamps an integer value to the 0‑255 range.
    /// </summary>
    static int Clamp(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }

    /// <summary>
    /// Attempts to recognize a barcode in the provided bitmap and returns the decoded text
    /// if it matches the expected value.
    /// </summary>
    /// <param name="image">Bitmap containing the barcode to recognize.</param>
    /// <param name="expectedText">The text that is expected to be decoded.</param>
    /// <returns>The decoded text if successful; otherwise, <c>null</c>.</returns>
    static string RecognizeBarcode(Bitmap image, string expectedText)
    {
        using (var reader = new BarCodeReader(image, DecodeType.AllSupportedTypes))
        {
            // Use high‑quality settings to improve detection on noisy images.
            reader.QualitySettings = QualitySettings.HighQuality;
            foreach (var result in reader.ReadBarCodes())
            {
                // Successful decode if text matches the expected value.
                if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == expectedText)
                {
                    return result.CodeText;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Entry point of the program. Generates a barcode, adds Gaussian noise at various SNR levels,
    /// and logs the recognition success rates.
    /// </summary>
    static void Main()
    {
        // Generate a clean barcode image.
        var (cleanBitmap, originalText) = GenerateBarcode();

        // Define SNR levels (higher sigma = lower SNR). Values chosen empirically.
        var snrLevels = new Dictionary<string, double>
        {
            { "30dB", 5.0 },
            { "20dB", 15.0 },
            { "10dB", 30.0 },
            { "5dB",  60.0 }
        };

        // Number of repetitions per SNR level to compute success rate.
        int repetitions = 5;

        // Initialize success counters for each SNR label.
        var successCounts = new Dictionary<string, int>();
        foreach (var key in snrLevels.Keys)
        {
            successCounts[key] = 0;
        }

        // Iterate over each SNR level and perform recognition attempts.
        foreach (var kvp in snrLevels)
        {
            string snrLabel = kvp.Key;
            double sigma = kvp.Value;

            for (int i = 0; i < repetitions; i++)
            {
                // Add noise to a fresh copy of the clean bitmap.
                using (var noisyBitmap = AddGaussianNoise(cleanBitmap, sigma))
                {
                    string decoded = RecognizeBarcode(noisyBitmap, originalText);
                    if (decoded != null)
                    {
                        successCounts[snrLabel]++;
                    }
                }
            }
        }

        // Dispose the original clean bitmap.
        cleanBitmap.Dispose();

        // Log success rates to the console.
        Console.WriteLine("Barcode Recognition Success Rates with Gaussian Noise:");
        foreach (var kvp in snrLevels)
        {
            string label = kvp.Key;
            int successes = successCounts[label];
            double rate = (double)successes / repetitions * 100.0;
            Console.WriteLine($"{label}: {successes}/{repetitions} ({rate:F1}%)");
        }
    }
}