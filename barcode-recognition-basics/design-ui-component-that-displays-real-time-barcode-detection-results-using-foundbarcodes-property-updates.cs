// Title: Real‑time barcode detection with rotation handling
// Description: Generates a QR code, rotates the image, and demonstrates barcode recognition on both the original and rotated images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap and Graphics for image manipulation, and BarCodeReader for detecting barcodes in images. Typical scenarios include validating barcode readability after image transformations and providing real‑time feedback in UI components.
// Prompt: Design a UI component that displays real‑time barcode detection results using FoundBarCodes property updates.
// Tags: qr, barcode, generation, recognition, rotation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, image rotation, and barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, rotates it, reads both images, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and rotated barcode images
        string originalPath = Path.Combine(tempFolder, "barcode.png");
        string rotatedPath = Path.Combine(tempFolder, "barcode_rotated.png");

        // Generate a QR barcode image and save it to the original path
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Demo123"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Rotate the generated image by 45 degrees and save the result
        if (File.Exists(originalPath))
        {
            using (var sourceBmp = new Bitmap(originalPath))
            {
                using (var rotatedBmp = new Bitmap(sourceBmp.Width, sourceBmp.Height))
                {
                    using (var graphics = Graphics.FromImage(rotatedBmp))
                    {
                        // Fill background with white to avoid transparent corners
                        graphics.Clear(Color.White);

                        // Move origin to center, rotate, then move back
                        graphics.TranslateTransform(sourceBmp.Width / 2f, sourceBmp.Height / 2f);
                        graphics.RotateTransform(45f);
                        graphics.TranslateTransform(-sourceBmp.Width / 2f, -sourceBmp.Height / 2f);

                        // Draw the original image onto the rotated canvas
                        graphics.DrawImage(sourceBmp, 0, 0, sourceBmp.Width, sourceBmp.Height);
                    }
                    rotatedBmp.Save(rotatedPath, ImageFormat.Png);
                }
            }
        }

        // Read and display detection results for the original image
        Console.WriteLine("Reading original barcode image:");
        ReadAndDisplay(originalPath);

        // Read and display detection results for the rotated image
        Console.WriteLine("\nReading rotated barcode image:");
        ReadAndDisplay(rotatedPath);

        // Cleanup temporary files and folder
        try
        {
            if (File.Exists(originalPath)) File.Delete(originalPath);
            if (File.Exists(rotatedPath)) File.Delete(rotatedPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not crash the program
        }
    }

    /// <summary>
    /// Reads a barcode image, performs recognition, and writes detection details to the console.
    /// </summary>
    /// <param name="imagePath">Path to the image file containing barcodes.</param>
    static void ReadAndDisplay(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform recognition; results are stored in FoundBarCodes collection
            reader.ReadBarCodes();

            // Output the total number of barcodes found
            Console.WriteLine($"FoundCount: {reader.FoundCount}");

            // Iterate through each detected barcode and display its details
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
                Console.WriteLine("---");
            }
        }
    }
}