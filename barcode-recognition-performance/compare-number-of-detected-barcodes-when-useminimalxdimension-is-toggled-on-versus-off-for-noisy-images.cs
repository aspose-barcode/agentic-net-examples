// Title: Effect of UseMinimalXDimension on Barcode Detection in Noisy Images
// Description: Demonstrates how toggling the UseMinimalXDimension setting influences the number of barcodes detected in a noisy image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode, Bitmap manipulation to add noise, and BarCodeReader with QualitySettings (XDimensionMode) to improve detection in challenging images. Developers often need to compare detection settings for noisy scans, making this a useful reference for image preprocessing and barcode quality tuning.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: barcode, detection, noise, useminimalxdimension, aspose.barcode, code128, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the impact of the UseMinimalXDimension setting on barcode detection
/// in noisy images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, adds noise, and compares detection counts
    /// with and without the UseMinimalXDimension quality setting.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeNoiseDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original and noisy barcode images
        string originalPath = Path.Combine(workFolder, "original.png");
        string noisyPath = Path.Combine(workFolder, "noisy.png");

        // Generate a simple Code128 barcode image
        GenerateBarcodeImage("1234567890", originalPath);

        // Introduce random noise into the barcode image
        AddNoise(originalPath, noisyPath, noisePixelCount: 5000);

        // Detect barcodes without enabling UseMinimalXDimension
        int countDefault = CountBarcodes(noisyPath, useMinimalXDimension: false);

        // Detect barcodes with UseMinimalXDimension enabled
        int countMinimal = CountBarcodes(noisyPath, useMinimalXDimension: true);

        // Output the comparison results to the console
        Console.WriteLine($"Detected barcodes without UseMinimalXDimension: {countDefault}");
        Console.WriteLine($"Detected barcodes with UseMinimalXDimension:    {countMinimal}");

        // Clean up temporary files (optional)
        try { File.Delete(originalPath); } catch { }
        try { File.Delete(noisyPath); } catch { }
        try { Directory.Delete(workFolder, true); } catch { }
    }

    /// <summary>
    /// Generates a Code128 barcode and saves it as a PNG image.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    static void GenerateBarcodeImage(string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Default generation settings are sufficient for this demo
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Adds random colored pixels to an image to simulate visual noise.
    /// </summary>
    /// <param name="inputPath">Path to the source image.</param>
    /// <param name="outputPath">Path where the noisy image will be saved.</param>
    /// <param name="noisePixelCount">Number of random pixels to modify.</param>
    static void AddNoise(string inputPath, string outputPath, int noisePixelCount)
    {
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (var bitmap = new Bitmap(inputPath))
        {
            Random rnd = new Random();
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Apply random light-gray pixels to simulate noise
            for (int i = 0; i < noisePixelCount; i++)
            {
                int x = rnd.Next(width);
                int y = rnd.Next(height);
                int gray = rnd.Next(200, 256);
                Color noiseColor = Color.FromArgb(gray, gray, gray);
                bitmap.SetPixel(x, y, noiseColor);
            }

            // Save the noisy image
            bitmap.Save(outputPath, ImageFormat.Png);
        }
    }

    /// <summary>
    /// Reads barcodes from an image and returns the number of detected codes.
    /// </summary>
    /// <param name="imagePath">Path to the image containing barcodes.</param>
    /// <param name="useMinimalXDimension">Whether to enable the minimal X-dimension mode.</param>
    /// <returns>Count of detected barcodes.</returns>
    static int CountBarcodes(string imagePath, bool useMinimalXDimension)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return 0;
        }

        int count = 0;
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            if (useMinimalXDimension)
            {
                // Enable minimal X-dimension mode to improve detection in noisy images
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Optionally set a minimal X-dimension value (e.g., 1 point)
                reader.QualitySettings.MinimalXDimension = 1f;
            }

            // Perform barcode reading
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results != null)
            {
                count = results.Length;
            }
        }

        return count;
    }
}