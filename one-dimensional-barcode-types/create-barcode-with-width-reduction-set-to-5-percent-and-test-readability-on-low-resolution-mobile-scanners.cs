// Title: Barcode generation with width reduction and low‑resolution readability test
// Description: Demonstrates creating a Code128 barcode with a 5 percent bar‑width reduction, then simulates a low‑resolution mobile scanner by downscaling the image and verifies that the barcode can still be read.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to adjust barcode appearance using the BarcodeGenerator.Parameters.Barcode.BarWidthReduction property and how to validate readability with BarCodeReader. Typical use cases include optimizing barcodes for small screens or low‑resolution capture devices. Developers often need to balance visual size reduction with scan reliability, making this pattern useful for mobile and IoT applications.
// Prompt: Create a barcode with width reduction set to 5 percent and test readability on low‑resolution mobile scanners.
// Tags: code128, barwidthreduction, lowresolution, barcodegeneration, barcoderecognition, png, aspnet

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a 5% bar‑width reduction,
/// downscaling it to simulate a low‑resolution mobile scanner, and reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates a low‑resolution version,
    /// and attempts to decode it using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        const string originalPath = "barcode.png";
        const string lowResPath = "barcode_lowres.png";

        // ------------------------------------------------------------
        // 1. Generate a high‑resolution Code128 barcode with 5% width reduction
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a 5 percent bar‑width reduction
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Save the generated barcode as a PNG image
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 2. Simulate a low‑resolution mobile scanner by downscaling the image
        // ------------------------------------------------------------
        using (var originalBitmap = new Bitmap(originalPath))
        {
            // Target width for the low‑resolution image (maintain aspect ratio)
            int targetWidth = 100;
            int targetHeight = (int)Math.Round((double)originalBitmap.Height * targetWidth / originalBitmap.Width);

            using (var lowResBitmap = new Bitmap(targetWidth, targetHeight))
            {
                using (var graphics = Graphics.FromImage(lowResBitmap))
                {
                    // Draw the original image onto the smaller bitmap (no high‑quality scaling needed)
                    graphics.DrawImage(originalBitmap, 0, 0, targetWidth, targetHeight);
                }

                // Save the low‑resolution image for recognition testing
                lowResBitmap.Save(lowResPath, ImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // 3. Attempt to read the barcode from the low‑resolution image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(lowResPath, DecodeType.AllSupportedTypes))
        {
            bool found = false;

            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the low‑resolution image.");
            }
        }
    }
}