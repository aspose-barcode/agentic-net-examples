// Title: Barcode recognition under Gaussian noise with varying SNR
// Description: Demonstrates generating a QR barcode, adding Gaussian noise at different signal‑to‑noise ratios, and measuring the success rate of barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating QR codes and the BarCodeReader class for detecting them in noisy images. Developers often need to evaluate robustness of barcode scanning under adverse imaging conditions, such as added noise, to fine‑tune preprocessing or error‑correction settings.
// Prompt: Test recognition on images with added Gaussian noise at varying signal‑to‑noise ratios and log success rates.
// Tags: qr, barcode, recognition, gaussian-noise, snr, aspose.barcode, image-processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a QR barcode, adds Gaussian noise at varying SNR levels, and logs the recognition success rate.
/// </summary>
class Program
{
    /// <summary>
    /// Adds Gaussian noise to a bitmap and returns a new bitmap.
    /// </summary>
    /// <param name="source">Source bitmap.</param>
    /// <param name="sigma">Standard deviation of the Gaussian noise.</param>
    /// <returns>Noisy bitmap.</returns>
    static Bitmap AddGaussianNoise(Bitmap source, double sigma)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap noisy = (Bitmap)source.Clone();

        Random rand = new Random();

        // Box-Muller transform to generate Gaussian distributed values.
        double NextGaussian()
        {
            double u1 = 1.0 - rand.NextDouble(); // avoid zero
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2);
            return randStdNormal;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color orig = noisy.GetPixel(x, y);

                // Apply noise to each channel.
                int r = (int)Math.Round(orig.R + sigma * NextGaussian());
                int g = (int)Math.Round(orig.G + sigma * NextGaussian());
                int b = (int)Math.Round(orig.B + sigma * NextGaussian());

                // Clamp to valid byte range.
                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));

                Color noisyColor = Color.FromArgb(r, g, b);
                noisy.SetPixel(x, y, noisyColor);
            }
        }

        return noisy;
    }

    /// <summary>
    /// Generates a QR barcode image with the specified text.
    /// </summary>
    /// <param name="text">Text to encode.</param>
    /// <returns>Bitmap containing the QR code.</returns>
    static Bitmap GenerateBarcode(string text)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Use default settings; size adapts to content.
            // Save to a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                // Load the image back into a bitmap.
                return (Bitmap)Bitmap.FromStream(ms);
            }
        }
    }

    /// <summary>
    /// Attempts to read a barcode from a bitmap. Returns true if a result is found.
    /// </summary>
    /// <param name="image">Bitmap to scan.</param>
    /// <param name="expectedText">Expected barcode text (not used in logic but kept for signature compatibility).</param>
    /// <returns>True if a barcode is successfully read.</returns>
    static bool TryReadBarcode(Bitmap image, string expectedText)
    {
        // Save bitmap to a temporary file for the reader (reader works with file paths).
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");
        try
        {
            using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                image.Save(fs, Aspose.Drawing.Imaging.ImageFormat.Png);
            }

            using (var reader = new BarCodeReader(tempPath, DecodeType.AllSupportedTypes))
            {
                // Default checksum validation is on; no extra settings needed.
                BarCodeResult[] results = reader.ReadBarCodes();
                // Success is defined by having at least one result with non‑empty CodeText.
                return results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            }
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }

    /// <summary>
    /// Entry point that generates a barcode, adds noise at various SNR levels, and logs recognition success rates.
    /// </summary>
    static void Main()
    {
        // Sample barcode text.
        const string barcodeText = "AsposeTest123";

        // Generate a clean barcode image.
        using (Bitmap cleanBarcode = GenerateBarcode(barcodeText))
        {
            // Define SNR levels (in dB) to test.
            double[] snrValues = { 30, 20, 10, 5 };
            // Number of attempts per SNR level.
            const int attemptsPerSNR = 5;

            foreach (double snr in snrValues)
            {
                int successCount = 0;

                // Approximate sigma from SNR: higher SNR => lower sigma.
                // Simple heuristic: sigma = 255 / (snr / 5)
                double sigma = 255.0 / (snr / 5.0);

                for (int i = 0; i < attemptsPerSNR; i++)
                {
                    using (Bitmap noisy = AddGaussianNoise(cleanBarcode, sigma))
                    {
                        bool success = TryReadBarcode(noisy, barcodeText);
                        if (success) successCount++;
                    }
                }

                double successRate = (double)successCount / attemptsPerSNR * 100.0;
                Console.WriteLine($"SNR: {snr} dB - Success Rate: {successRate:0.##}% ({successCount}/{attemptsPerSNR})");
            }
        }

        // End of program.
    }
}