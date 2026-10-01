// Title: Detect QR Code in Gaussian‑Noisy Image Using Minimal X‑Dimension Filtering
// Description: Generates a QR code, adds Gaussian noise, and reads it back using Aspose.BarCode with MinimalXDimension mode to improve detection on degraded images.
// Category-Description: This example demonstrates barcode generation and recognition with Aspose.BarCode. It uses BarcodeGenerator to create a QR code, applies image processing to simulate noise, and employs BarCodeReader with QualitySettings.XDimension set to UseMinimalXDimension for robust detection. Developers working on scanning damaged or low‑quality barcodes can reference this pattern for handling noisy inputs.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: qr, gaussian, noise, minimalxdimension, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, adding Gaussian noise, and detecting the barcode
/// using Minimal X‑Dimension filtering with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, corrupts it with noise,
    /// and attempts to read it back using MinimalXDimension mode.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string noisyPath = Path.Combine(tempFolder, "barcode_noisy.png");

        // Generate a QR code barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a reasonable XDimension for visibility
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Save directly to file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the generated barcode image and add Gaussian noise
        using (var bitmap = new Bitmap(barcodePath))
        {
            AddGaussianNoise(bitmap, sigma: 30.0);
            // Save the noisy image
            bitmap.Save(noisyPath, ImageFormat.Png);
        }

        // Verify the noisy image was created
        if (!File.Exists(noisyPath))
        {
            Console.WriteLine("Failed to create noisy image.");
            return;
        }

        // Read the barcode from the noisy image using MinimalXDimension filtering
        using (var reader = new BarCodeReader(noisyPath, DecodeType.AllSupportedTypes))
        {
            // Enable Minimal XDimension mode for recognition
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Optionally set a minimal XDimension threshold (float, in points)
            // reader.QualitySettings.MinimalXDimension = 1f; // Uncomment if property exists in the version

            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the noisy image.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            File.Delete(noisyPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }

    // Adds Gaussian noise to a bitmap. sigma defines the standard deviation of the noise.
    private static void AddGaussianNoise(Bitmap bitmap, double sigma)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        var rand = new Random();

        // Precompute 2*PI for efficiency
        const double twoPi = 2.0 * Math.PI;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Generate Gaussian noise using Box‑Muller transform
                double u1 = 1.0 - rand.NextDouble(); // avoid 0
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(twoPi * u2);
                int noise = (int)(randStdNormal * sigma);

                // Get original pixel color
                var originalColor = bitmap.GetPixel(x, y);
                int r = Clamp(originalColor.R + noise);
                int g = Clamp(originalColor.G + noise);
                int b = Clamp(originalColor.B + noise);
                int a = originalColor.A; // preserve alpha

                var newColor = Color.FromArgb(a, r, g, b);
                bitmap.SetPixel(x, y, newColor);
            }
        }
    }

    // Clamp color component to byte range [0,255]
    private static int Clamp(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }
}