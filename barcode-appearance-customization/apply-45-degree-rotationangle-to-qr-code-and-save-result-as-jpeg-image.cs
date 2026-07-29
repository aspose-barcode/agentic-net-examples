// Title: QR Code Generation with 45‑Degree Rotation
// Description: Demonstrates applying a 45‑degree rotation to a QR code and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as rotation using the BarcodeGenerator class. Typical use cases include customizing barcode appearance for branding or layout requirements. Developers often need to adjust rotation, size, and format when integrating barcodes into graphics or documents.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr code, rotation, jpeg, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR code, applies a 45‑degree rotation, and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, rotates it, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text that the QR code will encode
            generator.CodeText = "Hello World";

            // Apply a 45‑degree rotation to the generated barcode image
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated QR code as a JPEG file
            generator.Save("qr45.jpg");
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine("QR code generated with 45° rotation and saved as qr45.jpg");
    }
}