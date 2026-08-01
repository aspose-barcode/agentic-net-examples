// Title: Rotated barcode generation and recognition example
// Description: Demonstrates creating Code128 barcodes, rotating them at various angles, and verifying that Aspose.BarCode can correctly decode each orientation.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category, showcasing the use of BarcodeGenerator for barcode creation, Bitmap manipulation for rotation, and BarCodeReader for decoding. Typical use cases include handling scanned barcodes that may be rotated, ensuring robust recognition in real‑world applications. Developers often need to rotate images, adjust quality settings, and validate decoded values.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: barcode, rotation, code128, generation, recognition, aspose.barcode, bitmap, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 barcodes, rotating them, and recognizing the rotated images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates rotated barcode images, saves them, and validates recognition.
    /// </summary>
    static void Main()
    {
        // Define folder for generated barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "RotatedBarcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Text to encode in the barcode
        const string barcodeText = "Test123";

        // Rotation angles to apply (in degrees)
        int[] angles = new int[] { 0, 90, 180, 270 };

        // -----------------------------------------------------------------
        // Generate barcode images and rotate them according to the angles
        // -----------------------------------------------------------------
        foreach (int angle in angles)
        {
            string filePath = Path.Combine(folder, $"barcode_{angle}.png");

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                // Set module size (optional, improves readability)
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode to a memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Load the image from the stream for rotation
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Apply rotation if required
                        if (angle != 0)
                        {
                            bitmap.RotateFlip(GetRotateFlipType(angle));
                        }

                        // Persist the (rotated) image to disk
                        bitmap.Save(filePath, ImageFormat.Png);
                    }
                }
            }

            Console.WriteLine($"Generated barcode image at angle {angle} degrees: {filePath}");
        }

        Console.WriteLine();
        Console.WriteLine("=== Barcode Recognition of Rotated Images ===");

        // -----------------------------------------------------------------
        // Recognize each rotated image and verify the decoded text matches
        // -----------------------------------------------------------------
        foreach (int angle in angles)
        {
            string filePath = Path.Combine(folder, $"barcode_{angle}.png");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                continue;
            }

            // Initialize the barcode reader for Code128
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Use normal quality preset for balanced performance
                reader.QualitySettings = QualitySettings.NormalQuality;

                bool decoded = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    decoded = true;
                    Console.WriteLine($"Angle {angle}° - Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    if (result.CodeText != barcodeText)
                    {
                        Console.WriteLine($"  Mismatch! Expected '{barcodeText}'");
                    }
                }

                if (!decoded)
                {
                    Console.WriteLine($"Angle {angle}° - No barcode detected.");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("Processing completed.");
    }

    /// <summary>
    /// Maps a rotation angle (0, 90, 180, 270) to the corresponding RotateFlipType.
    /// </summary>
    /// <param name="angle">Rotation angle in degrees.</param>
    /// <returns>Corresponding RotateFlipType value.</returns>
    private static RotateFlipType GetRotateFlipType(int angle)
    {
        switch (angle)
        {
            case 90:
                return RotateFlipType.Rotate90FlipNone;
            case 180:
                return RotateFlipType.Rotate180FlipNone;
            case 270:
                return RotateFlipType.Rotate270FlipNone;
            case 0:
                return RotateFlipType.RotateNoneFlipNone;
            default:
                throw new ArgumentException("Unsupported rotation angle. Use 0, 90, 180, or 270.");
        }
    }
}