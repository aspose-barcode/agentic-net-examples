using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, adding Gaussian noise, and recognition at various SNR levels.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a random alphanumeric string of the specified length.
    /// </summary>
    /// <param name="length">Desired length of the string.</param>
    /// <param name="rnd">Random number generator.</param>
    /// <returns>Random alphanumeric string.</returns>
    static string GenerateRandomText(int length, Random rnd)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] buffer = new char[length];
        for (int i = 0; i < length; i++)
        {
            // Pick a random character from the allowed set
            buffer[i] = chars[rnd.Next(chars.Length)];
        }
        return new string(buffer);
    }

    /// <summary>
    /// Generates a Gaussian‑distributed random value using the Box‑Muller transform.
    /// </summary>
    /// <param name="rnd">Random number generator.</param>
    /// <param name="mean">Mean of the distribution (default 0).</param>
    /// <param name="stdDev">Standard deviation of the distribution (default 1).</param>
    /// <returns>Gaussian random value.</returns>
    static double NextGaussian(Random rnd, double mean = 0, double stdDev = 1)
    {
        // Generate two uniform random numbers in (0,1]
        double u1 = 1.0 - rnd.NextDouble(); // avoid zero
        double u2 = 1.0 - rnd.NextDouble();

        // Apply Box‑Muller transform
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * randStdNormal;
    }

    /// <summary>
    /// Adds Gaussian noise to the supplied bitmap in‑place.
    /// </summary>
    /// <param name="bitmap">Bitmap to modify.</param>
    /// <param name="stdDev">Standard deviation of the noise.</param>
    /// <param name="rnd">Random number generator.</param>
    static void AddGaussianNoise(Bitmap bitmap, double stdDev, Random rnd)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                // Retrieve original pixel color
                Color orig = bitmap.GetPixel(x, y);

                // Apply Gaussian noise to each channel
                int r = (int)Math.Round(orig.R + NextGaussian(rnd, 0, stdDev));
                int g = (int)Math.Round(orig.G + NextGaussian(rnd, 0, stdDev));
                int b = (int)Math.Round(orig.B + NextGaussian(rnd, 0, stdDev));

                // Clamp values to valid byte range
                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));

                // Set the noisy pixel back into the bitmap
                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
    }

    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // Define SNR levels (higher value = less noise)
        var snrLevels = new Dictionary<string, double>
        {
            { "30dB", 5.0 },   // low noise
            { "20dB", 15.0 },  // moderate noise
            { "10dB", 30.0 },  // high noise
            { "5dB",  50.0 }   // very high noise
        };

        const int samplesPerLevel = 5; // Number of barcodes to test per SNR level
        Random rnd = new Random();

        // Iterate over each SNR level
        foreach (var kvp in snrLevels)
        {
            string snrLabel = kvp.Key;
            double noiseStdDev = kvp.Value;
            int successCount = 0;

            // Generate and test a set of barcodes for the current noise level
            for (int i = 0; i < samplesPerLevel; i++)
            {
                // Create random barcode text
                string codeText = GenerateRandomText(10, rnd);

                // Generate barcode image into a memory stream
                using (var genStream = new MemoryStream())
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        generator.Save(genStream, BarCodeImageFormat.Png);
                    }

                    // Reset stream position for reading
                    genStream.Position = 0;

                    // Load bitmap from the generated image
                    using (var bitmap = new Bitmap(genStream))
                    {
                        // Add Gaussian noise to simulate degradation
                        AddGaussianNoise(bitmap, noiseStdDev, rnd);

                        // Attempt to read the barcode from the noisy image
                        using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                        {
                            // Use high‑quality preset for damaged images
                            reader.QualitySettings = QualitySettings.HighQuality;

                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Verify that the decoded text matches the original
                                if (!string.IsNullOrEmpty(result.CodeText) &&
                                    result.CodeText.Equals(codeText, StringComparison.Ordinal))
                                {
                                    successCount++;
                                    break; // Stop after first successful read
                                }
                            }
                        }
                    }
                }
            }

            // Calculate and display success rate for the current SNR level
            double successRate = (double)successCount / samplesPerLevel * 100.0;
            Console.WriteLine($"SNR {snrLabel}: Success {successCount}/{samplesPerLevel} ({successRate:F1}%)");
        }
    }
}