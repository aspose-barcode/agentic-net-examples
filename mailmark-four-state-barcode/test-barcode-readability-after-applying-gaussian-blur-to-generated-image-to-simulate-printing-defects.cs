// Title: Test barcode readability after Gaussian blur
// Description: Demonstrates generating a QR barcode, applying a Gaussian blur to simulate printing defects, and reading the blurred barcode.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It shows how to generate a barcode with BarcodeGenerator, manipulate the image using Aspose.Drawing (applying a blur), and then decode it with BarCodeReader. Developers often need to test barcode robustness against image degradation such as blur, noise, or low resolution, and this snippet illustrates the typical workflow and key API classes (BarcodeGenerator, BarCodeReader, QualitySettings, DeconvolutionMode).
// Prompt: Test barcode readability after applying Gaussian blur to the generated image to simulate printing defects.
// Tags: qr, barcode, blur, image-processing, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, applying Gaussian blur, and reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, blurs it, attempts to read it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBlurTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and blurred barcode images
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string blurredPath = Path.Combine(tempFolder, "barcode_blur.png");

        // Generate a QR barcode and save the original image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set module size (X dimension) for better visibility
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);

            // Generate bitmap for further processing (blur)
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Apply Gaussian blur approximation
                using (Bitmap blurred = ApplyGaussianBlur(original))
                {
                    // Save the blurred image to disk
                    blurred.Save(blurredPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        // Read the blurred barcode using the reader
        using (var reader = new BarCodeReader(blurredPath, DecodeType.AllSupportedTypes))
        {
            // Enable fast deconvolution to improve detection on blurred images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, ReadingQuality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            File.Delete(blurredPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    // Simple 3x3 average blur (approximation of Gaussian blur)
    static Bitmap ApplyGaussianBlur(Bitmap source)
    {
        int width = source.Width;
        int height = source.Height;
        Bitmap dest = new Bitmap(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int sumR = 0, sumG = 0, sumB = 0, count = 0;
                for (int ky = -1; ky <= 1; ky++)
                {
                    int ny = y + ky;
                    if (ny < 0 || ny >= height) continue;
                    for (int kx = -1; kx <= 1; kx++)
                    {
                        int nx = x + kx;
                        if (nx < 0 || nx >= width) continue;
                        Color c = source.GetPixel(nx, ny);
                        sumR += c.R;
                        sumG += c.G;
                        sumB += c.B;
                        count++;
                    }
                }
                int avgR = sumR / count;
                int avgG = sumG / count;
                int avgB = sumB / count;
                dest.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
            }
        }

        return dest;
    }
}