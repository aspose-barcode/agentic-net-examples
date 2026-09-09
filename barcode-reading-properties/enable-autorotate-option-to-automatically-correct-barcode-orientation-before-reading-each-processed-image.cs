// Title: Auto-rotate QR barcode detection example
// Description: Demonstrates generating a QR barcode, rotating it, and using Aspose.BarCode's auto‑rotate feature to correctly read the barcode regardless of orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcodes and the BarCodeReader class for decoding them. Typical use cases include reading barcodes that may be scanned at arbitrary angles, where developers rely on the autoRotate option to automatically correct orientation before decoding.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: qr, barcode, autorotate, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Sample program that creates a rotated QR barcode and reads it using Aspose.BarCode's auto‑rotate capability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AutoRotateSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output path for the rotated barcode image and the text to encode
        string barcodePath = Path.Combine(tempFolder, "rotated_qr.png");
        string codeText = "https://example.com";

        // Generate a QR barcode, rotate it 90 degrees, and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Parameters.RotationAngle = 90; // Apply rotation
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the rotated barcode; Aspose.BarCode automatically detects and corrects orientation
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Detected Orientation Angle: {result.Region.Angle} degrees");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}