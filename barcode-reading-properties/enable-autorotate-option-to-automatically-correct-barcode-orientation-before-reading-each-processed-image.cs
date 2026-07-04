// Title: Auto-rotate barcode detection example
// Description: Demonstrates generating a barcode, rotating the image, and using Aspose.BarCode's auto-rotate feature to correctly read the barcode regardless of orientation.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: barcode, autorotate, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, rotates the image, and reads it using auto-rotate.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates, rotates, and reads a barcode while demonstrating auto-rotate handling.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original and rotated barcode images
        string originalPath = "barcode.png";
        string rotatedPath = "barcode_rotated.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to disk
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode image to the original path
            generator.Save(originalPath);
        }

        // Verify that the original barcode image was created successfully
        if (!File.Exists(originalPath))
        {
            Console.WriteLine($"Error: Failed to create {originalPath}");
            return;
        }

        // ------------------------------------------------------------
        // Load the original image, rotate it 90 degrees clockwise, and save the rotated version
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(originalPath))
        {
            // Rotate the image 90 degrees clockwise (no flip)
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // Save the rotated image as PNG
            bitmap.Save(rotatedPath, ImageFormat.Png);
        }

        // Verify that the rotated barcode image was created successfully
        if (!File.Exists(rotatedPath))
        {
            Console.WriteLine($"Error: Failed to create {rotatedPath}");
            return;
        }

        // ------------------------------------------------------------
        // Read the rotated barcode image using Aspose.BarCode's auto-rotate capability
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(rotatedPath, DecodeType.AllSupportedTypes))
        {
            // The reader automatically corrects orientation; no explicit AutoRotate setting is needed
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detected barcode type and decoded text
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                // Region.Angle indicates the orientation correction applied (in degrees)
                Console.WriteLine($"Detected Orientation Angle: {result.Region.Angle}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(originalPath);
            File.Delete(rotatedPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}