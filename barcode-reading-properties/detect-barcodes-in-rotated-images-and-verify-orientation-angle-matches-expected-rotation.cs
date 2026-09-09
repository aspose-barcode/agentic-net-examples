// Title: Detect and Verify Barcode Orientation in Rotated Images
// Description: Demonstrates generating Code128 barcodes at various rotation angles, saving them as PNG, then reading the images to detect the barcode and verify that the detected orientation matches the expected rotation.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It shows how to use BarcodeGenerator to create rotated barcodes and BarCodeReader to decode them, retrieve the Region.Angle property, and compare it with an expected value. Developers working with scanned documents, label verification, or automated quality checks often need to confirm barcode orientation after image transformations.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: barcode, rotation, orientation, detection, verification, code128, aspnet, aspose.barcode, generation, recognition, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating rotated barcodes and verifying their orientation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at 0°, 90°, 180°, and 270°, then validates detected orientation.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeRotationDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define rotation angles to test
        string[] anglesText = { "0", "90", "180", "270" };
        foreach (string angleStr in anglesText)
        {
            // Parse angle string to float
            float angle = float.Parse(angleStr);
            // Build file path for the rotated barcode image
            string filePath = Path.Combine(tempDir, $"barcode_{angleStr}.png");

            // Generate and save the rotated barcode
            GenerateRotatedBarcode(filePath, angle);

            // Verify that the image was created successfully
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create image at {filePath}");
                continue;
            }

            // Read the barcode and verify its orientation
            VerifyBarcodeOrientation(filePath, angle);
        }

        // Cleanup temporary directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }

    /// <summary>
    /// Generates a Code128 barcode, rotates it by the specified angle, and saves it as a PNG file.
    /// </summary>
    /// <param name="path">Full file path where the image will be saved.</param>
    /// <param name="rotationAngle">Rotation angle in degrees.</param>
    static void GenerateRotatedBarcode(string path, float rotationAngle)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply rotation to the barcode image
            generator.Parameters.RotationAngle = rotationAngle;
            // Save the rotated barcode as PNG
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Reads a barcode image, extracts the detected orientation, and compares it with the expected angle.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="expectedAngle">The angle that was used when generating the barcode.</param>
    static void VerifyBarcodeOrientation(string imagePath, float expectedAngle)
    {
        // Use all supported barcode types for detection
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            bool found = false;
            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double detectedAngle = result.Region.Angle;

                // Normalize angles to the range [0,360)
                double normExpected = ((double)expectedAngle % 360 + 360) % 360;
                double normDetected = ((detectedAngle % 360) + 360) % 360;

                // Compute the smallest angular difference
                double diff = Math.Abs(normExpected - normDetected);
                if (diff > 180) diff = 360 - diff; // shortest distance

                // Output detection details
                Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                Console.WriteLine($"Detected Code: {result.CodeText}");
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Expected Angle: {normExpected}°, Detected Angle: {normDetected}°, Difference: {diff}°");

                // Verify orientation within a tolerance of 0.5°
                if (diff <= 0.5)
                {
                    Console.WriteLine("Orientation verification: SUCCESS");
                }
                else
                {
                    Console.WriteLine("Orientation verification: FAILURE");
                }
                found = true;
            }

            // No barcode detected case
            if (!found)
            {
                Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
            }
        }
    }
}