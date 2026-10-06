// Title: Auto-rotate barcode detection example
// Description: Generates a rotated Code128 barcode image, saves it to a temporary folder, and reads it using Aspose.BarCode's automatic orientation correction.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a barcode, apply a rotation, and then employ BarCodeReader to automatically detect and correct the barcode's orientation during decoding. Developers working with scanned documents, image processing pipelines, or mobile capture scenarios often need to handle barcodes that are not perfectly aligned; this pattern shows the typical API classes (BarcodeGenerator, BarCodeReader, BarCodeResult) and common use cases for orientation‑agnostic barcode reading.
// Prompt: Enable autoRotate option to automatically correct barcode orientation before reading each processed image.
// Tags: code128, barcode generation, barcode recognition, autorotate, orientation correction, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a rotated barcode and reading it with automatic orientation correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a rotated barcode,
    /// reads it using auto‑rotate, and cleans up all temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AutoRotateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output path and the barcode text
        string barcodePath = Path.Combine(tempFolder, "rotated_barcode.png");
        string codeText = "ASPOSE123";

        // Generate a Code128 barcode and rotate it 90 degrees
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.RotationAngle = 90; // rotate 90 degrees
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with automatic orientation correction (autoRotate is enabled by default)
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Angle: {result.Region.Angle} degrees");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}