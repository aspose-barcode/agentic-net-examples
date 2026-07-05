// Title: Rotated barcode recognition test
// Description: Demonstrates generating a Code128 barcode, rotating it at various angles, and verifying that the Aspose.BarCode recognizer correctly decodes each orientation.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: barcode, rotation, recognition, code128, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates a barcode, creates rotated versions, and validates recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode, rotates images, and reads them back to verify decoding.
    /// </summary>
    static void Main()
    {
        // Define barcode text and output directory
        string codeText = "Test123";
        string outputDir = Directory.GetCurrentDirectory();

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Console.WriteLine("Output directory does not exist.");
            return;
        }

        // Generate original barcode image
        string originalPath = Path.Combine(outputDir, "barcode_original.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(originalPath);
        }

        // Verify that the original image was created successfully
        if (!File.Exists(originalPath))
        {
            Console.WriteLine("Failed to create original barcode image.");
            return;
        }

        // Angles to test (in degrees)
        int[] angles = new int[] { 0, 90, 180, 270 };

        // Create rotated versions of the original barcode image
        foreach (int angle in angles)
        {
            string rotatedPath = Path.Combine(outputDir, $"barcode_{angle}.png");
            using (var originalBitmap = new Bitmap(originalPath))
            {
                // Clone original for rotation to avoid modifying the source file
                using (var bitmap = (Bitmap)originalBitmap.Clone())
                {
                    if (angle != 0)
                    {
                        // Apply rotation based on the current angle
                        switch (angle)
                        {
                            case 90:
                                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 180:
                                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 270:
                                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                    }

                    // Save the rotated image as PNG
                    bitmap.Save(rotatedPath, ImageFormat.Png);
                }
            }
        }

        // Recognize each rotated image and verify decoded text
        foreach (int angle in angles)
        {
            string path = Path.Combine(outputDir, $"barcode_{angle}.png");
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Use normal quality preset for recognition
                reader.QualitySettings = QualitySettings.NormalQuality;

                bool found = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    found = true;
                    Console.WriteLine($"Image: barcode_{angle}.png");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                    if (result.CodeText != codeText)
                    {
                        Console.WriteLine("Warning: Decoded text does not match expected value.");
                    }
                    Console.WriteLine();
                }

                if (!found)
                {
                    Console.WriteLine($"No barcode detected in image: barcode_{angle}.png");
                }
            }
        }
    }
}