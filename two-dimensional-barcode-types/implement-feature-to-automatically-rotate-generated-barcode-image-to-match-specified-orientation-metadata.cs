// Title: Barcode Generation with Automatic Rotation Based on Detected Orientation
// Description: Demonstrates generating a barcode, rotating it, detecting its orientation, and then creating a new barcode automatically rotated to match the detected angle.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for extracting orientation metadata, and the RotationAngle parameter to control image rotation. Developers working with barcode imaging often need to align barcodes to physical media or match existing orientation data, making this pattern a common requirement.
// Prompt: Implement feature to automatically rotate generated barcode image to match specified orientation metadata.
// Tags: barcode symbology, rotation, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, rotating it, detecting its orientation,
/// and automatically rotating a new barcode to match the detected angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder for the demo files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRotateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        string codeText = "1234567890";

        // ------------------------------------------------------------
        // 1. Generate the original (non‑rotated) barcode image.
        // ------------------------------------------------------------
        string originalPath = Path.Combine(outputDir, "original.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Original barcode saved to: {originalPath}");

        // ------------------------------------------------------------
        // 2. Generate a barcode rotated by 90 degrees.
        // ------------------------------------------------------------
        string rotatedPath = Path.Combine(outputDir, "rotated.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = 90f;
            generator.Save(rotatedPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Rotated barcode saved to: {rotatedPath}");

        // ------------------------------------------------------------
        // 3. Read the orientation metadata from the rotated image.
        // ------------------------------------------------------------
        float detectedAngle = 0f;
        using (var reader = new BarCodeReader(rotatedPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // The Region.Angle property provides the detected rotation angle.
                detectedAngle = (float)result.Region.Angle;
                Console.WriteLine($"Detected orientation angle: {detectedAngle} degrees");
                break; // Only need the first result.
            }
        }

        // ------------------------------------------------------------
        // 4. Generate a new barcode automatically rotated to match the detected angle.
        // ------------------------------------------------------------
        string autoRotatedPath = Path.Combine(outputDir, "auto_rotated.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = detectedAngle;
            generator.Save(autoRotatedPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Auto-rotated barcode saved to: {autoRotatedPath}");
    }
}