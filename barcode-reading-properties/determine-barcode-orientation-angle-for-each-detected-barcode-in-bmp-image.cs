// Title: Determine barcode orientation angle from a BMP image
// Description: Generates a rotated Code128 barcode, saves it as a BMP file, then reads the image to detect barcodes and prints each barcode's orientation angle.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition features, focusing on rotation handling. It uses BarcodeGenerator to create a barcode with a specific RotationAngle, saves it in BMP format, and employs BarCodeReader to decode all supported types, exposing the Region.Angle property for orientation. Developers working with image preprocessing, barcode scanning in varied orientations, or quality assurance of barcode placement will find this pattern useful.
// Prompt: Determine barcode orientation angle for each detected barcode in a BMP image.
// Tags: barcode orientation, barcode recognition, bmp, code128, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a rotated barcode, save it as BMP, and read its orientation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a rotated barcode, reads it back, and outputs orientation data.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeOrientationDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated BMP image
        string imagePath = Path.Combine(tempFolder, "rotated_barcode.bmp");

        // Generate a Code128 barcode rotated by 45 degrees and save it as BMP
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            generator.Parameters.RotationAngle = 45;
            generator.Save(imagePath, BarCodeImageFormat.Bmp);
        }

        // Ensure the image file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read all barcodes from the BMP image and output their orientation angles
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Angle: {result.Region.Angle}");
                Console.WriteLine();
            }
        }

        // Attempt to clean up temporary files; ignore any errors during cleanup
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for this demo
        }
    }
}