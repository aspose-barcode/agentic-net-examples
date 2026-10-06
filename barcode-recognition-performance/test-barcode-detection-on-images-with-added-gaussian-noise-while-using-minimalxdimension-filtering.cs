// Title: Barcode detection with Gaussian noise and MinimalXDimension filtering
// Description: Demonstrates generating a Code128 barcode, adding Gaussian noise, and detecting it using MinimalXDimension settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeGenerator, BarCodeReader, and quality settings such as XDimensionMode.UseMinimalXDimension. Typical use cases include testing barcode robustness against image degradation and configuring detection parameters for low‑resolution or noisy scans. Developers often need to fine‑tune these settings to improve read rates in challenging environments.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: barcode, code128, gaussian noise, minimalxdimension, detection, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, noise addition, and detection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, adds Gaussian noise, and attempts detection with MinimalXDimension filtering.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the clean and noisy barcode images
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string noisyPath = Path.Combine(tempFolder, "barcode_noisy.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeTest123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Load the barcode image, add Gaussian noise, and save the noisy version
        using (var bitmap = (Bitmap)Image.FromFile(barcodePath))
        {
            AddGaussianNoise(bitmap, sigma: 20.0);
            bitmap.Save(noisyPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Verify that the noisy image was created
        if (!File.Exists(noisyPath))
        {
            Console.WriteLine("Failed to create noisy image.");
            return;
        }

        // Read the noisy barcode using MinimalXDimension filtering
        using (var reader = new BarCodeReader(noisyPath, DecodeType.AllSupportedTypes))
        {
            // Configure quality settings to use minimal X-dimension detection
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f; // pixels

            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
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

    // Adds Gaussian noise to a bitmap. sigma defines the standard deviation of the noise.
    static void AddGaussianNoise(Bitmap bitmap, double sigma)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        Random rng = new Random();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color original = bitmap.GetPixel(x, y);

                int r = AddGaussian(original.R, sigma, rng);
                int g = AddGaussian(original.G, sigma, rng);
                int b = AddGaussian(original.B, sigma, rng);

                Color noisy = Color.FromArgb(original.A, r, g, b);
                bitmap.SetPixel(x, y, noisy);
            }
        }
    }

    // Returns a channel value with added Gaussian noise, clamped to [0,255].
    static int AddGaussian(int value, double sigma, Random rng)
    {
        // Box-Muller transform to generate a standard normal value
        double u1 = 1.0 - rng.NextDouble(); // avoid zero
        double u2 = 1.0 - rng.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        double noise = sigma * randStdNormal;
        int result = (int)Math.Round(value + noise);
        if (result < 0) result = 0;
        if (result > 255) result = 255;
        return result;
    }
}