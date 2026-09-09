// Title: Compare barcode detection with and without UseMinimalXDimension on noisy images
// Description: Demonstrates generating a Code128 barcode, adding random noise, and reading it twice—once with normal XDimension settings and once with UseMinimalXDimension—to show how the setting affects detection count.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure XDimension quality settings for noisy images. It uses BarCodeGenerator, BarCodeReader, and QualitySettings classes, common when developers need to improve detection reliability under poor image conditions. The snippet helps developers understand when to enable minimal XDimension mode to increase read success.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: code128, barcode detection, noise, xdimension, useminimalxdimension, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the effect of the UseMinimalXDimension setting on barcode detection in noisy images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, adds noise, and compares detection counts with different XDimension settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeNoiseDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the clean and noisy barcode images
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string noisyPath = Path.Combine(tempDir, "barcode_noisy.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Load the clean barcode image and add random noise pixels
        using (var bitmap = new Bitmap(barcodePath))
        {
            var rand = new Random();
            for (int i = 0; i < 5000; i++)
            {
                int x = rand.Next(bitmap.Width);
                int y = rand.Next(bitmap.Height);
                var color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                bitmap.SetPixel(x, y, color);
            }
            // Save the noisy image for later recognition
            bitmap.Save(noisyPath, ImageFormat.Png);
        }

        // Read barcodes using normal XDimension mode
        int countNormal = ReadBarcodes(noisyPath, useMinimal: false);
        // Read barcodes using UseMinimalXDimension mode
        int countMinimal = ReadBarcodes(noisyPath, useMinimal: true);

        // Output the detection results
        Console.WriteLine($"Barcodes detected (Normal XDimension): {countNormal}");
        Console.WriteLine($"Barcodes detected (UseMinimalXDimension): {countMinimal}");
    }

    /// <summary>
    /// Reads barcodes from the specified image using either normal or minimal XDimension settings.
    /// </summary>
    /// <param name="imagePath">Path to the image containing barcodes.</param>
    /// <param name="useMinimal">If true, enables UseMinimalXDimension mode; otherwise, uses normal mode.</param>
    /// <returns>The number of barcodes detected.</returns>
    static int ReadBarcodes(string imagePath, bool useMinimal)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return 0;
        }

        // Configure the reader to look for Code128 barcodes
        BaseDecodeType decode = DecodeType.Code128;
        using (var reader = new BarCodeReader(imagePath, decode))
        {
            // Apply the appropriate XDimension quality setting based on the flag
            if (useMinimal)
            {
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f;
            }
            else
            {
                reader.QualitySettings.XDimension = XDimensionMode.Normal;
            }

            // Perform barcode detection and return the count of results
            BarCodeResult[] results = reader.ReadBarCodes();
            return results?.Length ?? 0;
        }
    }
}