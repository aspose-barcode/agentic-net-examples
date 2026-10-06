// Title: Barcode recognition under Gaussian noise with success rate logging
// Description: Generates a QR code, adds Gaussian noise at several standard deviations, attempts to read the barcode, and logs success or failure for each noise level.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create a barcode image, manipulate pixel data, apply image noise, and use BarCodeReader with high‑quality settings. Developers working on image‑preprocessing, robustness testing, or automated scanning solutions often need to evaluate recognition performance under varying signal‑to‑noise ratios.
// Prompt: Test recognition on images with added Gaussian noise at varying signal‑to‑noise ratios and log success rates.
// Tags: qr, barcode, recognition, gaussian noise, signal-to-noise ratio, aspose.barcode, image processing, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode recognition on images with added Gaussian noise and logs success rates.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, adds noise at multiple levels, reads the barcode, and outputs results.
    /// </summary>
    static void Main()
    {
        // Generate a base QR code image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            using (var baseStream = new MemoryStream())
            {
                generator.Save(baseStream, BarCodeImageFormat.Png);
                baseStream.Position = 0;

                // Load the generated QR code into a bitmap for pixel manipulation.
                using (var baseBitmap = new Bitmap(baseStream))
                {
                    // Define the noise levels (standard deviations) to test.
                    float[] noiseStdDevs = new float[] { 0f, 5f, 10f, 20f, 30f };
                    var random = new Random();

                    var resultsLog = new List<string>();

                    // Iterate over each noise level, create a noisy copy, and attempt recognition.
                    foreach (float stdDev in noiseStdDevs)
                    {
                        // Create a new bitmap to hold the noisy image.
                        using (var noisyBitmap = new Bitmap(baseBitmap.Width, baseBitmap.Height, baseBitmap.PixelFormat))
                        {
                            // Apply Gaussian noise to each pixel.
                            for (int y = 0; y < baseBitmap.Height; y++)
                            {
                                for (int x = 0; x < baseBitmap.Width; x++)
                                {
                                    Color orig = baseBitmap.GetPixel(x, y);
                                    int r = Clamp(orig.R + (int)NextGaussian(random, stdDev), 0, 255);
                                    int g = Clamp(orig.G + (int)NextGaussian(random, stdDev), 0, 255);
                                    int b = Clamp(orig.B + (int)NextGaussian(random, stdDev), 0, 255);
                                    Color noisy = Color.FromArgb(r, g, b);
                                    noisyBitmap.SetPixel(x, y, noisy);
                                }
                            }

                            // Save the noisy bitmap to a memory stream for recognition.
                            using (var noisyStream = new MemoryStream())
                            {
                                noisyBitmap.Save(noisyStream, ImageFormat.Png);
                                noisyStream.Position = 0;

                                // Initialize the barcode reader for QR codes.
                                using (var reader = new BarCodeReader(noisyStream, DecodeType.QR))
                                {
                                    // Use high‑quality settings to improve detection on noisy images.
                                    reader.QualitySettings = QualitySettings.HighQuality;
                                    var results = reader.ReadBarCodes();

                                    // Determine success based on whether a code was read.
                                    bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                                    resultsLog.Add($"StdDev {stdDev}: {(success ? "Success" : "Failure")}");
                                }
                            }
                        }
                    }

                    // Output the aggregated recognition results.
                    Console.WriteLine("Recognition results with Gaussian noise:");
                    foreach (var line in resultsLog)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Generates a Gaussian‑distributed random value using the Box‑Muller transform.
    /// </summary>
    /// <param name="rng">Random number generator.</param>
    /// <param name="sigma">Standard deviation of the distribution.</param>
    /// <returns>Random value with the specified standard deviation.</returns>
    static double NextGaussian(Random rng, double sigma)
    {
        // Box-Muller transform
        double u1 = 1.0 - rng.NextDouble(); // avoid 0
        double u2 = 1.0 - rng.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        return sigma * randStdNormal;
    }

    /// <summary>
    /// Clamps an integer value to the inclusive range defined by <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}