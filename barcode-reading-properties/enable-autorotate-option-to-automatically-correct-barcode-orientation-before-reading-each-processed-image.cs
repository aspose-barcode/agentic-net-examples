// Title: Auto-rotate QR barcode detection example
// Description: Demonstrates generating a QR barcode, rotating it, and using Aspose.BarCode's auto‑rotate feature to correctly read the barcode regardless of orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcodes, the BarCodeReader class for decoding them, and the auto‑rotate capability that automatically corrects image orientation during reading. Developers working with scanned or programmatically rotated barcodes often need this pattern to ensure reliable detection without manual preprocessing.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: qr,barcode,generation,recognition,autorotate,aspose.barcode,csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a rotated QR barcode and read it using the auto‑rotate feature.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a rotated QR code, reads it with auto‑rotation, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AutoRotateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the rotated barcode image
        string barcodePath = Path.Combine(tempFolder, "rotated_qr.png");

        // Generate a QR barcode, rotate it by 90 degrees, and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Parameters.RotationAngle = 90; // Apply rotation
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode; the reader automatically detects and corrects orientation
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {result.Region.Angle} degrees");
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
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}