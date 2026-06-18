using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a rotated barcode, saving it to a file,
/// and then reading the barcode to verify its detected orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a rotated barcode image, saves it, and validates the orientation
    /// using Aspose.BarCode's recognition capabilities.
    /// </summary>
    static void Main()
    {
        // Define the output image file path.
        string imagePath = "rotated_barcode.png";

        // Expected rotation angle in degrees for the generated barcode.
        float expectedAngle = 45f;

        // ------------------------------------------------------------
        // Generate a barcode image with the specified rotation angle.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply the desired rotation to the barcode.
            generator.Parameters.RotationAngle = expectedAngle;

            // Save the rotated barcode image to the file system.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the image file was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the saved image and obtain its orientation.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform barcode recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check if any barcodes were detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
                return;
            }

            // Iterate through each detected barcode result.
            foreach (var result in results)
            {
                // Detected orientation angle (in degrees) of the barcode region.
                double detectedAngle = result.Region.Angle;

                // Output the decoded text and detected orientation.
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Orientation Angle: {detectedAngle} degrees");

                // Verify that the detected orientation matches the expected rotation.
                double tolerance = 0.5; // allowable deviation in degrees
                if (Math.Abs(detectedAngle - expectedAngle) <= tolerance)
                {
                    Console.WriteLine("Orientation verification: SUCCESS (angle matches expected rotation).");
                }
                else
                {
                    Console.WriteLine("Orientation verification: FAILURE (angle does not match expected rotation).");
                }
            }
        }
    }
}