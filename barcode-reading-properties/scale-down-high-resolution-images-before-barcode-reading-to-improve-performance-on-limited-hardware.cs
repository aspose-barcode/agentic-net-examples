// Title: Scale down high‑resolution barcode image before reading
// Description: Demonstrates generating a high‑resolution Code128 barcode, scaling it down to reduce size, and then reading the barcode from the scaled image.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to use BarcodeGenerator for barcode creation, System.Drawing for image scaling, and BarCodeReader for barcode recognition. Typical use cases include optimizing performance on devices with limited resources by reducing image resolution before decoding. Developers often need to balance image quality with processing speed, and this snippet provides a clear pattern for that workflow.
/// Prompt: Scale down high‑resolution images before barcode reading to improve performance on limited hardware.
/// Tags: code128, scaling, png, barcode generation, barcode recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a high‑resolution barcode, scaling the image down,
/// and reading the barcode from the scaled image to improve performance on limited hardware.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary folder, generates a barcode,
    /// scales the image, reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeScaleDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original and scaled images
        string originalPath = Path.Combine(workFolder, "original.png");
        string scaledPath = Path.Combine(workFolder, "scaled.png");

        // Generate a high‑resolution Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set larger dimensions (points) to increase image resolution
            generator.Parameters.ImageWidth.Point = 800f;
            generator.Parameters.ImageHeight.Point = 300f;
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Verify that the original image was created successfully
        if (!File.Exists(originalPath))
        {
            Console.WriteLine("Failed to create the original barcode image.");
            return;
        }

        // Load the original image for scaling
        using (Bitmap original = (Bitmap)Image.FromFile(originalPath))
        {
            // Calculate scaled dimensions (e.g., 25% of the original size)
            int newWidth = original.Width / 4;
            int newHeight = original.Height / 4;
            if (newWidth == 0) newWidth = 1;
            if (newHeight == 0) newHeight = 1;

            // Create a new bitmap with the scaled dimensions
            using (Bitmap scaled = new Bitmap(newWidth, newHeight))
            {
                // Draw the original image onto the scaled bitmap
                using (Graphics g = Graphics.FromImage(scaled))
                {
                    g.DrawImage(original, 0, 0, newWidth, newHeight);
                }

                // Save the scaled image to disk
                scaled.Save(scaledPath, ImageFormat.Png);
            }
        }

        // Verify that the scaled image was saved successfully
        if (!File.Exists(scaledPath))
        {
            Console.WriteLine("Failed to create the scaled barcode image.");
            return;
        }

        // Read and decode the barcode from the scaled image
        using (var reader = new BarCodeReader(scaledPath, DecodeType.AllSupportedTypes))
        {
            var barcodes = reader.ReadBarCodes();
            if (barcodes.Length == 0)
            {
                Console.WriteLine("No barcode detected in the scaled image.");
            }
            else
            {
                foreach (var result in barcodes)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(originalPath);
            File.Delete(scaledPath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demo
        }
    }
}