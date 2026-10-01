// Title: Read barcodes from a webcam frame and log orientation angles
// Description: Demonstrates how to use Aspose.BarCode to detect any supported barcode type in an image captured from a webcam and output each barcode's decoded text, type, and orientation angle.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the BarCodeReader class with DecodeType.AllSupportedTypes to automatically recognize multiple symbologies in a single image. Typical use cases include processing video frames from cameras, scanning documents, or analyzing images where barcode orientation varies. Developers often need to retrieve orientation data to correct image alignment or to support downstream processing pipelines.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: barcode, reading, orientation, webcam, aspose.barcode, decode, image, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that reads barcodes from an image (simulating a webcam frame) and prints
/// each barcode's text, type, and orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // NOTE: Direct webcam capture requires additional libraries not available in this runner.
        // For demonstration, we use a sample image file that represents a captured video frame.
        // Replace "frame.jpg" with the path to an actual webcam snapshot when running in a real environment.
        string imagePath = "frame.jpg";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a barcode reader that attempts to detect any supported barcode type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected in the image.");
            }
            else
            {
                // Iterate through each detected barcode and log its details.
                foreach (BarCodeResult result in results)
                {
                    // Retrieve the orientation angle of the barcode region.
                    double angle = result.Region.Angle;

                    // Output decoded text, barcode type, and orientation angle.
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"Orientation Angle: {angle} degrees");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}