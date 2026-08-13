// Title: Detect rotated barcode and verify orientation
// Description: Demonstrates generating a Code128 barcode, rotating the image, and using Aspose.BarCode to detect the barcode and confirm its orientation matches the expected rotation.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It showcases the use of BarcodeGenerator for creating barcodes, setting rotation via Parameters.RotationAngle, and BarCodeReader for detecting barcodes in images. Developers often need to handle rotated barcodes in real‑world scenarios such as scanned documents or camera captures, requiring reliable orientation detection and verification.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: barcode symbology, detection, rotation, orientation, aspose.barcode, code128, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, rotation, and orientation verification using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a rotated Code128 barcode, saves it, reads it back, and checks the detected orientation.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "rotated_barcode.png";

        // Expected rotation angle in degrees (must be 0, 90, 180, or 270 for reliable detection)
        float expectedAngle = 90f;

        // Generate a Code128 barcode and rotate it by the expected angle
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply rotation to the barcode image
            generator.Parameters.RotationAngle = expectedAngle;

            // Save the rotated barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' was not found.");
            return;
        }

        // Read the barcode from the rotated image
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Iterate over detected barcodes (there should be only one in this example)
            foreach (var result in reader.ReadBarCodes())
            {
                // The detection engine automatically determines the orientation.
                // The detected angle is available via result.Region.Angle.
                double detectedAngle = result.Region.Angle;

                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Detected code text: {result.CodeText}");
                Console.WriteLine($"Detected orientation angle: {detectedAngle} degrees");

                // Compare the detected angle with the expected rotation
                if (Math.Abs(detectedAngle - expectedAngle) < 0.1)
                {
                    Console.WriteLine("Orientation matches the expected rotation.");
                }
                else
                {
                    Console.WriteLine($"Orientation mismatch: expected {expectedAngle}°, but detected {detectedAngle}°.");
                }
            }
        }
    }
}