// Title: Demonstrate checksum validation on mixed-symbology barcode image
// Description: Generates Code128 and EAN13 barcodes, combines them, and reads them using default checksum validation.
// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
// Tags: barcode symbology, checksum validation, mixed symbology, aspnet barcoderecognition, aspnet barcodelibrary

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcodes, combines them, and reads them with default checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, merges them, and reads the combined image while applying default checksum validation.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Paths for individual and combined barcode images
        string code128Path = Path.Combine(outputDir, "code128.png");
        string ean13Path = Path.Combine(outputDir, "ean13.png");
        string mixedPath = Path.Combine(outputDir, "mixed.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate an EAN13 barcode with a valid checksum and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(ean13Path, BarCodeImageFormat.Png);
        }

        // Combine the two barcode images into a single image with spacing
        using (var img1 = (Bitmap)Image.FromFile(code128Path))
        using (var img2 = (Bitmap)Image.FromFile(ean13Path))
        {
            int width = Math.Max(img1.Width, img2.Width);
            int height = img1.Height + img2.Height + 20; // extra spacing between images

            using (var combined = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(combined))
                {
                    // Fill background with white
                    graphics.Clear(Aspose.Drawing.Color.White);
                    // Draw the first barcode at the top
                    graphics.DrawImage(img1, 0, 0);
                    // Draw the second barcode below the first, with spacing
                    graphics.DrawImage(img2, 0, img1.Height + 20);
                }

                // Save the combined image
                combined.Save(mixedPath, ImageFormat.Png);
            }
        }

        // Verify that the combined image was created successfully
        if (!File.Exists(mixedPath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // Read all supported barcodes from the combined image using default checksum validation
        using (var reader = new BarCodeReader(mixedPath, DecodeType.AllSupportedTypes))
        {
            // Apply default checksum handling
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            // Iterate through detected barcodes and output details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // For 1D barcodes, display the checksum if it is available
                if (result.Extended?.OneD?.CheckSum != null)
                {
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }

                Console.WriteLine();
            }
        }
    }
}