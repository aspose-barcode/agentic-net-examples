using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string barcodeText = "Test123";
        // Generate clean barcode bitmap
        using (Bitmap cleanBmp = GenerateBarcodeBitmap(barcodeText))
        {
            // Define SNR levels in dB to test
            double[] snrDbLevels = new double[] { 30, 20, 15, 10, 5 };
            var results = new List<(double snrDb, double successRate)>();

            foreach (double snrDb in snrDbLevels)
            {
                int trials = 10; // small sample size as required
                int successes = 0;

                for (int i = 0; i < trials; i++)
                {
                    // Add Gaussian noise to the clean image
                    using (Bitmap noisyBmp = AddGaussianNoise(cleanBmp, snrDb))
                    {
                        // Recognize barcode from noisy image
                        using (BarCodeReader reader = new BarCodeReader(noisyBmp, DecodeType.Code128))
                        {
                            // Use high quality settings for better detection in noisy images
                            reader.QualitySettings = QualitySettings.HighQuality;

                            bool recognized = false;
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                if (result != null && result.CodeText == barcodeText)
                                {
                                    recognized = true;
                                    break;
                                }
                            }

                            if (recognized)
                                successes++;
                        }
                    }
                }

                double successRate = (double)successes / trials * 100.0;
                results.Add((snrDb, successRate));
                Console.WriteLine($"SNR: {snrDb} dB - Success Rate: {successRate:0.##}%");
            }

            // Summary
            Console.WriteLine("\n--- Summary ---");
            foreach (var r in results)
            {
                Console.WriteLine($"SNR {r.snrDb} dB => {r.successRate:0.##}% success");
            }
        }
    }

    // Generates a barcode bitmap for the given text using Code128 symbology
    private static Bitmap GenerateBarcodeBitmap(string text)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Save to memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                // Load bitmap from stream
                return (Bitmap)Image.FromStream(ms);
            }
        }
    }

    // Adds Gaussian noise to a bitmap based on the desired SNR (in dB)
    private static Bitmap AddGaussianNoise(Bitmap source, double snrDb)
    {
        // Calculate noise standard deviation from SNR (approximation)
        // Assuming signal peak = 255
        double noiseStdDev = 255.0 / Math.Pow(10.0, snrDb / 20.0);
        var rand = new Random();

        // Clone source to avoid modifying original
        Bitmap noisy = (Bitmap)source.Clone();

        for (int y = 0; y < noisy.Height; y++)
        {
            for (int x = 0; x < noisy.Width; x++)
            {
                Color pixel = noisy.GetPixel(x, y);

                int r = ClampToByte(pixel.R + (int)GaussianRandom(rand, 0, noiseStdDev));
                int g = ClampToByte(pixel.G + (int)GaussianRandom(rand, 0, noiseStdDev));
                int b = ClampToByte(pixel.B + (int)GaussianRandom(rand, 0, noiseStdDev));

                noisy.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }

        return noisy;
    }

    // Generates a Gaussian-distributed random number using Box-Muller transform
    private static double GaussianRandom(Random rand, double mean, double stdDev)
    {
        // Generate two uniform random numbers in (0,1]
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        // Box-Muller transform
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * randStdNormal;
    }

    // Clamps integer value to byte range 0-255
    private static int ClampToByte(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }
}