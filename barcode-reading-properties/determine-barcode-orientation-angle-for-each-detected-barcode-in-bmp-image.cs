// Title: Determine Barcode Orientation Angle in BMP Image
// Description: The program loads a BMP image, detects all barcodes, and outputs each barcode's type, text, and orientation angle.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: barcode symbology, orientation, bmp, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read barcodes from a BMP image and retrieve their orientation angles.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Detects barcodes in the specified BMP file and prints their type, text, and angle.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image containing barcodes.
        string imagePath = "sample.bmp";

        // Verify that the image file exists before processing.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file '{imagePath}' not found.");
            return;
        }

        // Initialize the barcode reader for all supported symbologies.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            int count = 0;

            // Iterate through each detected barcode.
            foreach (var result in reader.ReadBarCodes())
            {
                // The angle (in degrees) of the detected barcode.
                double angle = result.Region.Angle;

                // Output barcode details including its orientation angle.
                Console.WriteLine($"Barcode {count + 1}: Type={result.CodeTypeName}, Text={result.CodeText}, Angle={angle}");
                count++;
            }

            // If no barcodes were found, inform the user.
            if (count == 0)
            {
                Console.WriteLine("No barcodes detected in the image.");
            }
        }
    }
}