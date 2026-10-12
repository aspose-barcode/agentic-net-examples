// Title: Barcode Generation with Rotation Based on Metadata
// Description: Demonstrates generating a Code128 barcode image rotated to a specified orientation angle and reading back its detected orientation.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It shows how to use BarcodeGenerator to set RotationAngle, save the image, and then use BarCodeReader to decode and retrieve the Region.Angle. Developers working with barcode imaging often need to rotate barcodes to match physical orientation metadata, and this snippet illustrates the typical API workflow for such scenarios.
// Prompt: Implement feature to automatically rotate generated barcode image to match specified orientation metadata.
// Tags: barcode, rotation, code128, image generation, image recognition, aspose.barcode, png, metadata

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a rotated barcode image and reading its orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a rotated Code128 barcode, saves it, and reads back its orientation.
    /// </summary>
    static void Main()
    {
        // Define the orientation angle (in degrees) that should be applied to the barcode image.
        float metadataAngle = 90f;

        // Create a unique temporary folder to store the generated barcode image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeRotationDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string barcodePath = Path.Combine(outputFolder, "rotated_barcode.png");

        // Generate the barcode with the specified rotation angle and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            generator.Parameters.RotationAngle = metadataAngle;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the generated barcode and output the detected code text and orientation angle.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Orientation Angle: {result.Region.Angle}");
            }
        }

        // Optional clean‑up: delete the temporary folder and its contents.
        // Directory.Delete(outputFolder, true);
    }
}