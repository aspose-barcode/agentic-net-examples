// Title: Detect and Verify Barcode Orientation in Rotated Images
// Description: This example generates a QR code, rotates it by a specified angle, and then uses Aspose.BarCode to detect the barcode and confirm that the detected orientation matches the expected rotation.
// Category-Description: Demonstrates barcode recognition with orientation handling using Aspose.BarCode. The example covers generating a barcode (BarcodeGenerator), applying rotation (Parameters.RotationAngle), reading the image (BarCodeReader), and accessing the Region.Angle property to verify orientation. Useful for developers needing to process scanned or photographed barcodes that may be rotated, such as in document imaging or inventory systems.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: barcode, orientation, rotation, qrcode, recognition, aspose.barcode, barcodegenerator, barcodereader, region.angle

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a rotated QR barcode, reads it back,
/// and verifies that the detected orientation matches the expected rotation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the expected rotation angle (in degrees) applied to the generated barcode.
        const float expectedAngle = 90f;

        // Create a QR barcode generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Apply the expected rotation to the barcode image.
            generator.Parameters.RotationAngle = expectedAngle;

            // Store the generated barcode image in a memory stream.
            using (var barcodeStream = new MemoryStream())
            {
                // Save the rotated barcode as a PNG image into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                barcodeStream.Position = 0;

                // Initialize a barcode reader to detect barcodes in the image.
                using (var reader = new BarCodeReader())
                {
                    // Load the image from the memory stream.
                    reader.SetBarCodeImage(barcodeStream);
                    // Perform barcode detection.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Check if any barcodes were found.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                    else
                    {
                        // Iterate through all detected barcodes.
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Code Text: {result.CodeText}");
                            Console.WriteLine($"Code Type: {result.CodeTypeName}");

                            // Retrieve the orientation angle reported by the recognition engine.
                            double detectedAngle = result.Region.Angle;
                            Console.WriteLine($"Detected Orientation Angle: {detectedAngle} degrees");

                            // Verify that the detected angle matches the expected rotation within a tolerance.
                            if (Math.Abs(detectedAngle - expectedAngle) < 0.1)
                            {
                                Console.WriteLine("Orientation verification passed.");
                            }
                            else
                            {
                                Console.WriteLine("Orientation verification failed.");
                            }
                        }
                    }
                }
            }
        }
    }
}