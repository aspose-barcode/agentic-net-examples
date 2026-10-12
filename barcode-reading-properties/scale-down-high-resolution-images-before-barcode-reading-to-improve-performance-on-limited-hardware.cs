// Title: Scale down high‑resolution barcode image before reading
// Description: Demonstrates generating a high‑resolution barcode, scaling it down, and reading it to improve performance on limited hardware.
// Category-Description: This example belongs to the Aspose.BarCode image preprocessing category. It shows how to use BarcodeGenerator to create a barcode, Aspose.Drawing to resize images, and BarCodeReader to decode barcodes. Typical use cases include reducing image size to speed up recognition on devices with constrained resources, such as embedded systems or mobile devices. Developers often need to balance image quality with processing speed, and this snippet illustrates the common workflow.
// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
// Tags: barcode, scaling, image preprocessing, performance, code128, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a high‑resolution barcode, scaling the image down,
/// and reading the barcode from the scaled image to improve recognition performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, rescales it, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeScaleDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original high‑resolution and the scaled images
        string highResPath = Path.Combine(tempFolder, "high.png");
        string scaledPath = Path.Combine(tempFolder, "scaled.png");

        // Generate a high‑resolution barcode image (300 DPI) and save it to disk
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = 300f; // high DPI for better quality
            generator.Save(highResPath, BarCodeImageFormat.Png);
        }

        // Scale down the high‑resolution image to reduce processing load during recognition
        using (Image original = Image.FromFile(highResPath))
        {
            int newWidth = original.Width / 3;
            int newHeight = original.Height / 3;

            using (var bitmap = new Bitmap(newWidth, newHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Draw the original image onto the smaller bitmap
                    graphics.DrawImage(original, 0, 0, newWidth, newHeight);
                }

                // Save the scaled image for barcode reading
                bitmap.Save(scaledPath, ImageFormat.Png);
            }
        }

        // Read barcodes from the scaled image using the BarCodeReader
        using (var reader = new BarCodeReader(scaledPath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Attempt to clean up temporary files and folder; ignore any errors
        try
        {
            File.Delete(highResPath);
            File.Delete(scaledPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for this demo
        }
    }
}