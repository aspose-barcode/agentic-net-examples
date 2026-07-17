// Title: Generate and rotate QR Code barcode 180° for upside-down display
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and rotating it 180 degrees so it appears upside down.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image manipulation and orientation. It showcases the use of BarcodeGenerator, EncodeTypes, and rotation parameters to produce rotated barcode images, a common requirement for custom label designs, packaging, or display scenarios where barcode orientation must be altered.
// Prompt: Generate QR Code barcode and rotate barcode 180 degrees for upside‑down display.
// Tags: qr code, rotation, image generation, aspose.barcode, barcodegenerator, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and rotating it 180 degrees for upside‑down display using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, applies a 180° rotation, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with QR code symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Apply a 180-degree rotation to make the barcode appear upside down.
            generator.Parameters.RotationAngle = 180f;

            // Save the rotated barcode image to a PNG file.
            generator.Save("qr_upside_down.png");
        }
    }
}