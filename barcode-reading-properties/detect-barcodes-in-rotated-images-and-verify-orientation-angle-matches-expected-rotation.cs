// Title: Detect and Verify Barcode Orientation in Rotated Images
// Description: This example generates a Code128 barcode, rotates it, saves it as a PNG file, then reads the image to detect the barcode and confirms that the detected orientation angle matches the expected rotation.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create a barcode with a specific rotation using BarcodeGenerator, save it, and then use BarCodeReader to extract the barcode and its region angle. Typical for scenarios where images may be captured at arbitrary angles and developers need to programmatically correct or validate orientation. Key classes: BarcodeGenerator, BarCodeReader, BarCodeResult.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: barcode symbology, generation, recognition, rotation, orientation, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a rotated barcode, reading it back, and verifying the detected orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a rotated barcode image, reads it, and validates the orientation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRotatedDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths and parameters
        string imagePath = Path.Combine(tempFolder, "rotated.png");
        string codeText = "123456";
        float expectedAngle = 90f;

        // Generate a rotated Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = expectedAngle;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            Cleanup(tempFolder);
            return;
        }

        // Set up the reader to detect all supported barcode types
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            bool found = false;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double detectedAngle = result.Region.Angle;
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {detectedAngle}");

                // Compare the detected angle with the expected rotation
                if (Math.Abs(detectedAngle - expectedAngle) < 0.1)
                {
                    Console.WriteLine("Orientation angle matches expected rotation.");
                }
                else
                {
                    Console.WriteLine($"Orientation angle does NOT match. Expected {expectedAngle}, got {detectedAngle}");
                }

                found = true;
            }

            // Inform the user if no barcode was found
            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }

        // Clean up temporary files and folder
        Cleanup(tempFolder);
    }

    /// <summary>
    /// Deletes the specified folder and its contents, handling any exceptions.
    /// </summary>
    /// <param name="folderPath">The path of the folder to delete.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}