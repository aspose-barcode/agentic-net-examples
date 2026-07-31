// Title: Read QR barcode from image and log orientation angle
// Description: Generates a QR code, saves it to disk, reads it back, and logs the barcode type, text, and detected orientation angle.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create a barcode using BarcodeGenerator, save it as an image, and then use BarCodeReader to decode the barcode and retrieve its Region.Angle property. Developers working with barcode imaging, scanning, or orientation detection can use these patterns for QR, DataMatrix, and other symbologies in desktop or server applications.
// Prompt: Read barcodes from a video frame captured by a webcam and log orientation angles.
// Tags: qr, barcode, generation, recognition, orientation, console, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a QR barcode, saving it, and reading it back to log orientation information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, saves it, reads it, and outputs detection details.
    /// </summary>
    static void Main()
    {
        // Define the output directory and barcode image path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        string barcodePath = Path.Combine(outputDir, "sample_barcode.png");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a simple QR barcode and save it to a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode Sample"))
        {
            // Set QR error correction level (optional visual parameter)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // Read the barcode from the saved image and log its orientation angle
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // The Region.Angle property indicates the detected orientation of the barcode
                Console.WriteLine($"Detected Barcode Type : {result.CodeTypeName}");
                Console.WriteLine($"Detected Code Text    : {result.CodeText}");
                Console.WriteLine($"Detected Angle (deg)  : {result.Region.Angle}");
                Console.WriteLine();
            }
        }
    }
}