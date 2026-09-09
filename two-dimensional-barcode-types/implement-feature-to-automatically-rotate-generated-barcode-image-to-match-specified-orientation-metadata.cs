// Title: Automatic Rotation of Generated Barcode Image Based on Detected Orientation
// Description: Demonstrates generating a barcode, detecting its rotation angle, and regenerating it with matching orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for extracting metadata such as orientation, and how developers can align barcode images to detected angles. Typical scenarios include preparing barcodes for printing, scanning, or image processing pipelines where consistent orientation is required.
// Prompt: Implement feature to automatically rotate generated barcode image to match specified orientation metadata.
// Tags: barcode symbology, generation, recognition, rotation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, detecting its rotation angle, and regenerating it with matching orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates a barcode, reads its orientation, and creates a rotated version.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for output files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define barcode content and output file paths
        string codeText = "ASPOSE123";
        string originalPath = Path.Combine(workFolder, "original.png");
        string rotatedPath = Path.Combine(workFolder, "rotated.png");

        // Step 1: Generate a barcode with a known rotation (e.g., 45 degrees)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = 45f;
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Verify the original barcode image was created successfully
        if (!File.Exists(originalPath))
        {
            Console.WriteLine("Failed to create the original barcode image.");
            return;
        }

        // Step 2: Read the barcode to obtain its orientation angle metadata
        float detectedAngle = 0f;
        using (BarCodeReader reader = new BarCodeReader(originalPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Region.Angle provides the orientation angle in degrees
                detectedAngle = (float)result.Region.Angle;
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {detectedAngle}");
                break; // Assuming a single barcode in the image
            }
        }

        // Step 3: Generate a new barcode rotated to match the detected angle
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = detectedAngle;
            generator.Save(rotatedPath, BarCodeImageFormat.Png);
        }

        // Output the file locations for verification
        Console.WriteLine($"Original barcode saved at: {originalPath}");
        Console.WriteLine($"Rotated barcode saved at: {rotatedPath}");
    }
}