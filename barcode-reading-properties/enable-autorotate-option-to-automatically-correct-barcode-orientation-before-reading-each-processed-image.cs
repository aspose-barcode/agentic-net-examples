// Title: Auto‑rotate barcode reading with Aspose.BarCode
// Description: Demonstrates how Aspose.BarCode automatically corrects the orientation of a rotated barcode image using the auto‑rotate feature during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. It highlights the auto‑rotate capability that detects and corrects barcode orientation, a common requirement when processing scanned or photographed barcodes. Developers often need to generate barcodes, manipulate images, and reliably read them regardless of rotation, using classes like BarcodeGenerator, BarCodeReader, Image, and related enums.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: code128, auto-rotate, png, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates auto‑rotation of barcode images during recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, rotates it, and reads it back with auto‑rotate enabled.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeAutoRotate_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and rotated barcode images
        string originalPath = Path.Combine(tempFolder, "original.png");
        string rotatedPath = Path.Combine(tempFolder, "rotated.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Load the generated barcode image, rotate it by 90 degrees, and save the rotated version
        using (Image img = Image.FromFile(originalPath))
        {
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            img.Save(rotatedPath, ImageFormat.Png);
        }

        // Read the rotated barcode; Aspose.BarCode automatically corrects orientation (auto‑rotate)
        using (BarCodeReader reader = new BarCodeReader(rotatedPath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Detected Angle (auto‑rotated): {result.Region.Angle}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(originalPath)) File.Delete(originalPath);
            if (File.Exists(rotatedPath)) File.Delete(rotatedPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not crash the demo
        }
    }
}