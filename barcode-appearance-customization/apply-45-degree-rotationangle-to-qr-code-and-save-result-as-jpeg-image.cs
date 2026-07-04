// Title: QR Code Rotation Example
// Description: Demonstrates applying a 45-degree rotation to a QR code and saving it as a JPEG image.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr, rotation, jpeg, aspose.barcode, barcodegeneration

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the QR code rotation demonstration.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code, rotates it 45 degrees, and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Initialize the QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the rotation angle to 45 degrees.
            generator.Parameters.RotationAngle = 45f;

            // Persist the rotated QR code image as a JPEG file.
            generator.Save("qr_rotated.jpg");
        }
    }
}