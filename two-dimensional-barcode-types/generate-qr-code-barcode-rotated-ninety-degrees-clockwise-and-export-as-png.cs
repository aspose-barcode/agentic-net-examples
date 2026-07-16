// Title: Generate and Rotate QR Code, Export as PNG
// Description: Demonstrates creating a QR Code barcode, rotating it 90 degrees clockwise, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to encode data into QR Code symbology, apply rotation via the Parameters.RotationAngle property, and export the result in PNG format. Typical use cases include creating rotated barcodes for label designs, packaging, or UI elements where orientation matters. Developers often need to adjust barcode orientation and output format using the generation API.
// Prompt: Generate a QR Code barcode rotated ninety degrees clockwise and export as PNG.
// Tags: qr code, rotation, png, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code, rotates it, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Set rotation angle to 90 degrees clockwise.
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated QR Code image as a PNG file.
            generator.Save("qr_rotated.png");
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine("QR Code generated and saved as 'qr_rotated.png'.");
    }
}