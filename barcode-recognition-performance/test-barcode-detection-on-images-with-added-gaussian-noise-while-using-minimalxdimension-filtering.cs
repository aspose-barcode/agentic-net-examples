// Title: Barcode detection with Gaussian noise and MinimalXDimension filtering
// Description: Generates a Code128 barcode, adds Gaussian noise, and detects it using MinimalXDimension settings.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create barcodes, Aspose.Drawing for image manipulation, and BarCodeReader with QualitySettings to fine‑tune detection of small‑dimension barcodes. Typical scenarios include preprocessing noisy scans and configuring XDimension for robust recognition in industrial or retail applications.
// Prompt: Test barcode detection on images with added Gaussian noise while using MinimalXDimension filtering.
// Tags: code128, gaussian noise, minimalxdimension, barcode detection, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, corrupting it with Gaussian noise,
/// and recognizing it using MinimalXDimension filtering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, adds noise, and attempts detection.
    /// </summary>
    static void Main()
    {
        // Create a Code128 barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Store the generated barcode in a memory stream as PNG
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Load the barcode image into a bitmap for pixel manipulation
                using (var bitmap = new Bitmap(barcodeStream))
                {
                    // Apply Gaussian noise (mean=0, sigma=20) to simulate a noisy scan
                    AddGaussianNoise(bitmap, 0f, 20f);

                    // Save the noisy image to a temporary file for recognition
                    string noisyImagePath = "noisy_barcode.png";
                    bitmap.Save(noisyImagePath, ImageFormat.Png);

                    // Ensure the file was created before proceeding
                    if (!File.Exists(noisyImagePath))
                    {
                        Console.WriteLine("Failed to create the noisy image file.");
                        return;
                    }

                    // Initialize the barcode reader for Code128 with the noisy image
                    using (var reader = new BarCodeReader(noisyImagePath, DecodeType.Code128))
                    {
                        // Configure quality settings to improve detection of small XDimension barcodes
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = 5f; // Minimum XDimension in pixels
                        reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                        // Iterate through detected barcodes and output results
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Detected Text: {result.CodeText}");
                        }
                    }

                    // Attempt to delete the temporary file; ignore any errors
                    try
                    {
                        File.Delete(noisyImagePath);
                    }
                    catch
                    {
                        // Suppress cleanup exceptions
                    }
                }
            }
        }
    }

    // Adds Gaussian noise to a bitmap. Mean and sigma are expressed in pixel intensity (0‑255).
    private static void AddGaussianNoise(Bitmap bitmap, float mean, float sigma)
    {
        var rand = new Random();
        int width = bitmap.Width;
        int height = bitmap.Height;

        // Iterate over each pixel and apply noise using the Box‑Muller transform
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Retrieve the original pixel color
                var originalColor = bitmap.GetPixel(x, y);

                // Generate independent Gaussian noise for each color channel
                int noiseR = (int)GaussianRandom(rand, mean, sigma);
                int noiseG = (int)GaussianRandom(rand, mean, sigma);
                int noiseB = (int)GaussianRandom(rand, mean, sigma);

                // Apply noise and clamp channel values to the valid range [0,255]
                int r = Math.Clamp(originalColor.R + noiseR, 0, 255);
                int g = Math.Clamp(originalColor.G + noiseG, 0, 255);
                int b = Math.Clamp(originalColor.B + noiseB, 0, 255);

                // Set the modified pixel back into the bitmap
                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
    }

    // Generates a single Gaussian‑distributed random number using the Box‑Muller method.
    private static double GaussianRandom(Random rand, float mean, float sigma)
    {
        // Generate two uniform random numbers in (0,1]
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();

        // Apply the Box‑Muller transform to obtain a standard normal value
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2);

        // Scale and shift to the desired mean and standard deviation
        return mean + sigma * randStdNormal;
    }
}