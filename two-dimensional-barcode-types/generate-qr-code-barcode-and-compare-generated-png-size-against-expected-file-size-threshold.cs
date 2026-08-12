// Title: Generate QR Code and Validate PNG File Size
// Description: Creates a QR Code barcode, saves it as a PNG, and checks that the generated file size does not exceed a defined threshold.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with QR symbology, configure error correction level, and export the barcode to a PNG image. Typical use cases include creating QR codes for URLs, contact information, or product data and performing post‑generation validation such as file size checks. Developers often need to ensure generated images meet size constraints for web or mobile delivery.
// Prompt: Generate QR Code barcode and compare generated PNG size against expected file size threshold.
// Tags: qr code, barcode generation, png, file size, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it as PNG, and verifying the file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code, saves it, and compares its size to a threshold.
    /// </summary>
    static void Main()
    {
        // Define the temporary output path for the generated QR code PNG
        string outputPath = Path.Combine(Path.GetTempPath(), "qr.png");

        // Initialize the QR code generator with the QR symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "Hello World";

            // Configure QR error correction level (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the PNG file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate QR code image.");
            return;
        }

        // Retrieve the file size in bytes
        long fileSize = new FileInfo(outputPath).Length;
        const long sizeThreshold = 5000; // Expected maximum size in bytes

        Console.WriteLine($"Generated QR code image size: {fileSize} bytes.");

        // Compare the actual file size against the defined threshold
        if (fileSize <= sizeThreshold)
        {
            Console.WriteLine("Size is within the expected threshold.");
        }
        else
        {
            Console.WriteLine("Size exceeds the expected threshold.");
        }
    }
}