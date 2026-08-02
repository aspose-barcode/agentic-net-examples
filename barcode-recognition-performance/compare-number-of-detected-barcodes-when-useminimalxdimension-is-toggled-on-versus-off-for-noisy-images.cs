// Title: Compare barcode detection with UseMinimalXDimension on noisy images
// Description: Demonstrates generating a Code128 barcode, adding noise, and comparing detection counts with and without the UseMinimalXDimension setting.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to configure QualitySettings for barcode detection in noisy images. It uses BarcodeGenerator, BarCodeReader, and XDimensionMode to show typical use cases where developers need to improve detection reliability under poor image conditions. The snippet serves as a reference for adjusting X‑dimension parameters to optimize recognition performance.
// Prompt: Compare the number of detected barcodes when UseMinimalXDimension is toggled on versus off for noisy images.
// Tags: barcode symbology, detection, noise, useminimalxdimension, qualitysettings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, adds random noise, and compares
/// the number of detected barcodes with and without the <c>UseMinimalXDimension</c> setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, noise addition,
    /// and detection comparison logic.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original and noisy barcode images.
        string barcodePath = "barcode.png";
        string noisyPath = "barcode_noisy.png";

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Load the generated barcode, add random noise, and save the noisy image.
        // --------------------------------------------------------------------
        using (var original = new Bitmap(barcodePath))
        using (var noisy = new Bitmap(original.Width, original.Height, original.PixelFormat))
        {
            // Copy the original barcode onto the new bitmap.
            using (var g = Graphics.FromImage(noisy))
            {
                g.DrawImage(original, 0, 0, original.Width, original.Height);
            }

            // Add simple random noise: draw colored dots on ~1% of the pixels.
            var rand = new Random();
            int noiseCount = (original.Width * original.Height) / 100;
            for (int i = 0; i < noiseCount; i++)
            {
                int x = rand.Next(original.Width);
                int y = rand.Next(original.Height);
                noisy.SetPixel(
                    x,
                    y,
                    Aspose.Drawing.Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256))
                );
            }

            // Save the noisy image to disk.
            noisy.Save(noisyPath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Verify that the noisy image was created successfully.
        if (!File.Exists(noisyPath))
        {
            Console.WriteLine("Failed to create noisy image.");
            return;
        }

        // --------------------------------------------------------------------
        // Helper function to count detected barcodes using a configurable reader.
        // --------------------------------------------------------------------
        int CountBarcodes(Action<BarCodeReader> configureReader)
        {
            using (var reader = new BarCodeReader(noisyPath, DecodeType.AllSupportedTypes))
            {
                // Apply any custom configuration to the reader (e.g., XDimension settings).
                configureReader?.Invoke(reader);

                // Perform barcode detection and return the count.
                var results = reader.ReadBarCodes();
                return results?.Length ?? 0;
            }
        }

        // Count barcodes with default XDimension settings (no UseMinimalXDimension).
        int countDefault = CountBarcodes(reader => { /* No custom configuration */ });

        // Count barcodes with UseMinimalXDimension enabled and a minimal size of 2 pixels.
        int countMinimal = CountBarcodes(reader =>
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f; // Example minimal size in pixels.
        });

        // Output the comparison results to the console.
        Console.WriteLine($"Detected barcodes (default XDimension): {countDefault}");
        Console.WriteLine($"Detected barcodes (UseMinimalXDimension): {countMinimal}");
    }
}