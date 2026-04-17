using System;
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
        // Define paths
        string outputDir = Directory.GetCurrentDirectory();
        string noisyImagePath = Path.Combine(outputDir, "barcode_noisy.png");

        // Generate a simple Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set a small XDimension (2 pixels) to make detection sensitive
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Add Gaussian noise to the bitmap
                AddGaussianNoise(bitmap, mean: 0.0, stdDev: 20.0);

                // Save the noisy image
                bitmap.Save(noisyImagePath, ImageFormat.Png);
            }
        }

        // Verify the noisy image exists
        if (!File.Exists(noisyImagePath))
        {
            Console.WriteLine("Failed to create the noisy barcode image.");
            return;
        }

        // Read and detect the barcode using MinimalXDimension filtering
        using (var reader = new BarCodeReader(noisyImagePath, DecodeType.Code128))
        {
            // Use normal quality preset and enable UseMinimalXDimension mode
            reader.QualitySettings = QualitySettings.NormalQuality;
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f; // pixels

            // Perform detection
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }
    }

    // Adds Gaussian noise to a bitmap using the Box-Muller transform
    private static void AddGaussianNoise(Bitmap bitmap, double mean, double stdDev)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        var rand = new Random();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Get original pixel
                var originalColor = bitmap.GetPixel(x, y);

                // Generate Gaussian noise for each channel
                int noiseR = (int)Math.Round(mean + stdDev * NextGaussian(rand));
                int noiseG = (int)Math.Round(mean + stdDev * NextGaussian(rand));
                int noiseB = (int)Math.Round(mean + stdDev * NextGaussian(rand));

                // Apply noise and clamp to [0,255]
                int r = Math.Clamp(originalColor.R + noiseR, 0, 255);
                int g = Math.Clamp(originalColor.G + noiseG, 0, 255);
                int b = Math.Clamp(originalColor.B + noiseB, 0, 255);

                // Set noisy pixel
                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
    }

    // Generates a standard normal distributed random value
    private static double NextGaussian(Random rand)
    {
        // Box-Muller transform
        double u1 = 1.0 - rand.NextDouble(); // avoid zero
        double u2 = 1.0 - rand.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    }
}