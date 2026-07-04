// Title: Detect rotated barcode and verify orientation
// Description: Generates a Code128 barcode, rotates the image, then reads the barcode and checks that the detected orientation matches the known rotation.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: code128, barcode detection, rotation, orientation, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, image rotation, and orientation verification using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, rotates it, reads it back, and validates the detected angle.
    /// </summary>
    static void Main()
    {
        // Expected rotation angle (in degrees) applied to the image
        const double expectedAngle = 90.0;

        // File paths for the original and rotated barcode images
        string originalPath = "original.png";
        string rotatedPath = "rotated.png";

        // ------------------------------------------------------------
        // 1. Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode image to disk
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Verify that the original barcode image was successfully created
        if (!File.Exists(originalPath))
        {
            Console.WriteLine($"Failed to create barcode image: {originalPath}");
            return;
        }

        // ------------------------------------------------------------
        // 2. Load the original image, rotate it 90° clockwise, and save
        // ------------------------------------------------------------
        using (var originalBitmap = new Bitmap(originalPath))
        {
            // Clone the bitmap to avoid altering the original file
            using (var rotatedBitmap = (Bitmap)originalBitmap.Clone())
            {
                // Rotate the image 90 degrees clockwise (no flip)
                rotatedBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                // Save the rotated image to disk
                rotatedBitmap.Save(rotatedPath, ImageFormat.Png);
            }
        }

        // Verify that the rotated image was successfully created
        if (!File.Exists(rotatedPath))
        {
            Console.WriteLine($"Failed to create rotated image: {rotatedPath}");
            return;
        }

        // ------------------------------------------------------------
        // 3. Read the rotated barcode image and evaluate orientation
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(rotatedPath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded barcode text
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // Retrieve the angle of the barcode region (in degrees)
                double detectedAngle = result.Region.Angle;
                Console.WriteLine($"Detected Angle: {detectedAngle} degrees");

                // Allow a small tolerance when comparing angles
                double tolerance = 0.5;
                bool matchesExpected = Math.Abs(detectedAngle - expectedAngle) <= tolerance ||
                                       Math.Abs(detectedAngle - (360 - expectedAngle)) <= tolerance;

                // Report whether the detected orientation matches the expected rotation
                if (matchesExpected)
                {
                    Console.WriteLine("Orientation matches expected rotation.");
                }
                else
                {
                    Console.WriteLine("Orientation does NOT match expected rotation.");
                }
            }
        }

        // ------------------------------------------------------------
        // 4. Clean up temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(originalPath);
            File.Delete(rotatedPath);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}