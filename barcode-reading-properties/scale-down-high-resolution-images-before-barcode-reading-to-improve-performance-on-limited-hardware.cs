// Title: Scale Down High‑Resolution Barcode Image for Faster Reading
// Description: Demonstrates generating a high‑resolution barcode, scaling it down, and reading it to improve performance on limited hardware.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Image manipulation via Aspose.Drawing to reduce image size, and BarCodeReader for decoding. Developers often need to handle high‑resolution scans on constrained devices, making image scaling a common performance‑optimizing technique.
// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
// Tags: barcode, scaling, performance, code128, generation, recognition, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a high‑resolution barcode, scales the image down, and reads the barcode from the scaled image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, scaling, and recognition workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for intermediate files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeScaleDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the high‑resolution and scaled images
        string highResPath = Path.Combine(tempDir, "highres.png");
        string scaledPath = Path.Combine(tempDir, "scaled.png");

        // ------------------------------------------------------------
        // Generate a high‑resolution barcode image (600 DPI)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = 600f; // Set high DPI for the barcode
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Verify that the high‑resolution image was created successfully
        if (!File.Exists(highResPath))
        {
            Console.WriteLine("Failed to create high‑resolution barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Scale down the image to half its original dimensions
        // ------------------------------------------------------------
        using (var original = Image.FromFile(highResPath))
        {
            int newWidth = original.Width / 2;
            int newHeight = original.Height / 2;

            using (var scaled = new Bitmap(newWidth, newHeight))
            {
                using (var graphics = Graphics.FromImage(scaled))
                {
                    // Draw the original image onto the scaled bitmap
                    graphics.DrawImage(original, 0, 0, newWidth, newHeight);
                }

                // Save the scaled image as PNG
                scaled.Save(scaledPath, ImageFormat.Png);
            }
        }

        // Verify that the scaled image was created successfully
        if (!File.Exists(scaledPath))
        {
            Console.WriteLine("Failed to create scaled barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read barcodes from the scaled image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(scaledPath, DecodeType.AllSupportedTypes))
        {
            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Detected Type: {result.CodeType}, Text: {result.CodeText}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcodes detected in the scaled image.");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directory (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(highResPath);
            File.Delete(scaledPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}