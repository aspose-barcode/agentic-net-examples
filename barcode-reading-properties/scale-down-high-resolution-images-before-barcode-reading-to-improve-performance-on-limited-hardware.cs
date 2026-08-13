// Title: Scale Down High‑Resolution Barcode Image for Faster Reading
// Description: Demonstrates generating a high‑resolution barcode, scaling it down, and reading the barcode to improve performance on limited hardware.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing category. It shows how to use BarcodeGenerator, Image manipulation classes from Aspose.Drawing, and BarCodeReader to downscale images before recognition. Developers often need to reduce image size to speed up barcode scanning on devices with constrained resources.
// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
// Tags: barcode, scaling, image preprocessing, code128, reading, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Example program that creates a high‑resolution barcode, scales the image down,
/// and reads the barcode from the scaled image to demonstrate performance‑optimizing preprocessing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, downscales it, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Paths for the generated high‑resolution and scaled images
        const string highResPath = "highres.png";
        const string scaledPath = "scaled.png";

        // -------------------------------------------------
        // 1. Generate a high‑resolution barcode image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Increase resolution to simulate a high‑resolution source (300 DPI)
            generator.Parameters.Resolution = 300f;
            // Save the barcode as a PNG file
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Verify the high‑resolution file was created successfully
        if (!File.Exists(highResPath))
        {
            Console.WriteLine($"Failed to create {highResPath}");
            return;
        }

        // -------------------------------------------------
        // 2. Downscale the image to improve recognition speed
        // -------------------------------------------------
        using (var originalImage = Image.FromFile(highResPath))
        {
            // Calculate target dimensions (50 % of original size)
            int targetWidth = originalImage.Width / 2;
            int targetHeight = originalImage.Height / 2;

            using (var scaledBitmap = new Bitmap(targetWidth, targetHeight))
            {
                using (var graphics = Graphics.FromImage(scaledBitmap))
                {
                    // Use high‑quality interpolation for better visual fidelity
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    // Draw the original image onto the scaled bitmap
                    graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
                }

                // Save the downscaled image as PNG
                scaledBitmap.Save(scaledPath, ImageFormat.Png);
            }
        }

        // Verify the scaled file was created successfully
        if (!File.Exists(scaledPath))
        {
            Console.WriteLine($"Failed to create {scaledPath}");
            return;
        }

        // -------------------------------------------------
        // 3. Read the barcode from the scaled image
        // -------------------------------------------------
        using (var reader = new BarCodeReader(scaledPath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeType}");
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }

        // Cleanup: optional removal of temporary files (best‑effort)
        try
        {
            File.Delete(highResPath);
            File.Delete(scaledPath);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical
        }
    }
}