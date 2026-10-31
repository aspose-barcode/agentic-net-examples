// Title: QR Code recognition under Gaussian noise with varying SNR
// Description: Demonstrates generating QR barcodes, adding Gaussian noise at different signal‑to‑noise ratios, and measuring recognition success rates.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, manipulate images with Aspose.Drawing, and employ BarCodeReader to decode barcodes. Typical use cases include robustness testing, image preprocessing evaluation, and automated quality assessment of barcode scanning under noisy conditions. Developers often need to generate test images, apply noise or distortion, and programmatically evaluate decoding performance.
/// Prompt: Test recognition on images with added Gaussian noise at varying signal‑to‑noise ratios and log success rates.
/// Tags: qr, barcode, recognition, gaussian noise, snr, aspose.barcode, image processing, testing

using System;
using System.IO;
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
    /// Entry point. Generates QR codes, adds Gaussian noise at several SNR levels, and logs the recognition success rate for each level.
    /// </summary>
    static void Main()
    {
        // Define the SNR values (in dB) to test and the number of samples per SNR.
        double[] snrValues = { 30.0, 20.0, 10.0 };
        int samplesPerSNR = 5;

        // Iterate over each SNR value.
        foreach (double snr in snrValues)
        {
            int successCount = 0;

            // Generate multiple noisy samples for the current SNR.
            for (int i = 0; i < samplesPerSNR; i++)
            {
                // Create a unique code text for each sample.
                string codeText = "Test" + i;

                // Generate a QR barcode image.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    using (var originalBitmap = generator.GenerateBarCodeImage())
                    {
                        // Convert SNR to noise amplitude.
                        double amplitude = GetAmplitudeFromSNR(snr);

                        // Add Gaussian noise to the barcode image.
                        using (var noisyBitmap = AddGaussianNoise(originalBitmap, amplitude))
                        {
                            // Encode the noisy image to a memory stream (PNG format) for decoding.
                            using (var ms = new MemoryStream())
                            {
                                noisyBitmap.Save(ms, ImageFormat.Png);
                                ms.Position = 0;

                                // Set up the barcode reader to recognize all supported types.
                                BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                                using (var reader = new BarCodeReader(ms, decodeType))
                                {
                                    // Attempt to read barcodes from the noisy image.
                                    var results = reader.ReadBarCodes();
                                    bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                                    if (success) successCount++;
                                }
                            }
                        }
                    }
                }
            }

            // Calculate and display the success rate for the current SNR.
            double successRate = (double)successCount / samplesPerSNR * 100.0;
            Console.WriteLine($"SNR {snr} dB: Success rate {successRate:F1}% ({successCount}/{samplesPerSNR})");
        }
    }

    /// <summary>
    /// Estimates the noise amplitude required to achieve the specified signal‑to‑noise ratio (SNR) in decibels.
    /// </summary>
    /// <param name="snrDb">Desired SNR in decibels.</param>
    /// <returns>Amplitude value for Gaussian noise.</returns>
    static double GetAmplitudeFromSNR(double snrDb)
    {
        // Approximate noise amplitude based on SNR (signal assumed max 255).
        return 255.0 / Math.Pow(10.0, snrDb / 20.0);
    }

    /// <summary>
    /// Creates a new bitmap by adding Gaussian noise to the source image.
    /// </summary>
    /// <param name="source">Original barcode image.</param>
    /// <param name="amplitude">Noise amplitude.</param>
    /// <returns>Bitmap containing the noisy image.</returns>
    static Bitmap AddGaussianNoise(Bitmap source, double amplitude)
    {
        var noisy = new Bitmap(source.Width, source.Height);
        var rand = new Random();

        // Process each pixel and add noise.
        for (int y = 0; y < source.Height; y++)
        {
            for (int x = 0; x < source.Width; x++)
            {
                Color original = source.GetPixel(x, y);
                int r = Clamp(original.R + (int)(Gaussian(rand) * amplitude));
                int g = Clamp(original.G + (int)(Gaussian(rand) * amplitude));
                int b = Clamp(original.B + (int)(Gaussian(rand) * amplitude));
                Color noisyColor = Color.FromArgb(r, g, b);
                noisy.SetPixel(x, y, noisyColor);
            }
        }

        return noisy;
    }

    /// <summary>
    /// Generates a random value following a standard normal distribution using the Box‑Muller transform.
    /// </summary>
    /// <param name="rand">Random number generator.</param>
    /// <returns>Gaussian-distributed double with mean 0 and standard deviation 1.</returns>
    static double Gaussian(Random rand)
    {
        // Box-Muller transform for standard normal distribution (mean 0, std dev 1).
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    }

    /// <summary>
    /// Clamps an integer value to the 0‑255 range.
    /// </summary>
    /// <param name="value">Value to clamp.</param>
    /// <returns>Clamped integer.</returns>
    static int Clamp(int value)
    {
        return value < 0 ? 0 : (value > 255 ? 255 : value);
    }
}