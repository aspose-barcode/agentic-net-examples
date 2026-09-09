// Title: Adjust DPI Settings for Accurate Barcode Detection
// Description: Demonstrates generating a QR barcode at 300 DPI, loading the image with matching DPI, and recognizing the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to control image resolution with BarcodeGenerator, adjust bitmap DPI with Aspose.Drawing.Bitmap, and read barcodes using BarCodeReader. Developers often need these steps when working with high‑resolution scans or when precise barcode region detection is required.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: qr, barcode, dpi, generation, recognition, aspose.barcode, bitmap, decode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR barcode image at a specific DPI,
/// adjusts the bitmap resolution to match, and reads the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, sets DPI, and performs recognition.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the example files
        string tempDir = Path.Combine(Path.GetTempPath(), "DPIExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode_300dpi.png");

        // Generate a QR barcode image with a high DPI (300)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Resolution = 300f; // Set image resolution to 300 DPI
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f; // Set module size
            generator.Save(barcodePath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the image and adjust its DPI before recognition
        using (var bitmap = new Bitmap(barcodePath))
        {
            // Ensure the bitmap DPI matches the generation DPI to improve detection accuracy
            bitmap.SetResolution(300f, 300f);

            // Initialize the barcode reader to detect all supported barcode types
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Perform barcode detection
                var results = reader.ReadBarCodes();

                // Output detection results
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                    }
                }
            }
        }

        // Cleanup temporary files (optional)
        // Directory.Delete(tempDir, true);
    }
}