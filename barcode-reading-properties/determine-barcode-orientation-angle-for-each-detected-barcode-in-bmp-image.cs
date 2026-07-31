// Title: Determine Barcode Orientation Angle in BMP Image
// Description: Loads a BMP image, detects all barcodes within it, and outputs each barcode's orientation angle in degrees.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, demonstrating how to use BarCodeReader to locate and analyze barcodes in raster images. It showcases key API classes such as BarCodeReader, DecodeType, and BarcodeResult, which are commonly used for barcode detection, extraction of metadata, and handling of various symbologies in real‑world applications like inventory management and document processing. Developers often need to determine barcode orientation for downstream image processing or alignment tasks.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: barcode orientation, detection, bmp, aspose.barcode, csharp, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to detect barcodes in a BMP image and retrieve their orientation angles.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample rotated barcode if needed,
    /// then reads the image, detects all barcodes, and prints their type, text, and orientation.
    /// </summary>
    static void Main()
    {
        // Path for the sample BMP image
        string imagePath = "rotated_barcode.bmp";

        // If the image does not exist, generate a sample barcode and rotate it
        if (!File.Exists(imagePath))
        {
            // Create a Code128 barcode with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Rotate the barcode by 90 degrees
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode as BMP
                generator.Save(imagePath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Generated sample barcode image: {imagePath}");
            }
        }

        // Verify the file exists before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File '{imagePath}' not found.");
            return;
        }

        // Read the BMP image and detect all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode
            foreach (var result in reader.ReadBarCodes())
            {
                // Orientation angle of the detected barcode (in degrees)
                double angle = result.Region.Angle;

                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Orientation Angle: {angle} degrees");
                Console.WriteLine();
            }
        }
    }
}