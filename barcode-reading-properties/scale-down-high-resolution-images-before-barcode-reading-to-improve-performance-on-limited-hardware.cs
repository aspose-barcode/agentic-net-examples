// Title: Scale Down High‑Resolution Barcode Image for Faster Reading
// Description: Demonstrates generating a high‑resolution QR code, scaling it down, and reading the barcode from the smaller image to improve performance on limited hardware.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to use BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. It illustrates common tasks such as high‑resolution image generation, image scaling with Aspose.Drawing, and barcode recognition on scaled images—operations frequently needed by developers optimizing barcode scanning on constrained devices.
// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
// Tags: barcode, qr, scaling, performance, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a high‑resolution QR code, scales it down,
/// and reads the barcode from the scaled image to demonstrate performance gains.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Paths for the generated high‑resolution barcode and the scaled image
        string highResPath = Path.Combine(Path.GetTempPath(), "highResBarcode.png");
        string scaledPath = Path.Combine(Path.GetTempPath(), "scaledBarcode.png");

        // -----------------------------------------------------------------
        // 1. Generate a high‑resolution QR code image (simulating a large input)
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a very high DPI and large canvas to produce a big image
            generator.Parameters.Resolution = 600f;
            generator.Parameters.ImageWidth.Pixels = 2000f;
            generator.Parameters.ImageHeight.Pixels = 2000f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the high‑resolution barcode as PNG
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Verify the image was created
        if (!File.Exists(highResPath))
        {
            Console.WriteLine("Failed to create the high‑resolution barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Load the high‑resolution image and scale it down
        // -----------------------------------------------------------------
        using (var originalImage = Image.FromFile(highResPath))
        {
            // Desired maximum dimension (pixels) for the scaled image
            const int maxDimension = 500;

            // Compute scaling factor while preserving aspect ratio
            float scale = Math.Min((float)maxDimension / originalImage.Width, (float)maxDimension / originalImage.Height);
            int newWidth = (int)(originalImage.Width * scale);
            int newHeight = (int)(originalImage.Height * scale);

            // Create a new bitmap with the target size
            using (var scaledBitmap = new Bitmap(newWidth, newHeight))
            {
                using (var graphics = Graphics.FromImage(scaledBitmap))
                {
                    // Draw the original image into the smaller bitmap
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }

                // Save the scaled image to a file (optional, for visual verification)
                using (var fileStream = new FileStream(scaledPath, FileMode.Create, FileAccess.Write))
                {
                    scaledBitmap.Save(fileStream, ImageFormat.Png);
                }

                // -----------------------------------------------------------------
                // 3. Read barcodes from the scaled image to demonstrate improved performance
                // -----------------------------------------------------------------
                using (var memoryStream = new MemoryStream())
                {
                    // Write the scaled bitmap into a memory stream
                    scaledBitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0;

                    // Use BarCodeReader on the scaled image stream
                    using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Decoded barcode text: {result.CodeText}");
                        }
                    }
                }
            }
        }

        // Cleanup: delete temporary files (optional)
        try { File.Delete(highResPath); } catch { }
        try { File.Delete(scaledPath); } catch { }
    }
}