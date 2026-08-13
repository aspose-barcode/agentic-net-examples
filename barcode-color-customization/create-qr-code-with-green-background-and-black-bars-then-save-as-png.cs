// Title: Generate QR Code with Green Background and Black Bars
// Description: Demonstrates creating a QR code with a green background and black foreground bars, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize QR code appearance using BarcodeGenerator, EncodeTypes, and drawing parameters. Developers often need to adjust colors, sizes, and output formats for branding or UI integration, and this snippet shows the essential API calls for such tasks.
// Prompt: Create a QR code with a green background and black bars, then save as PNG.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with custom colors and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code with a green background and black bars, then writes it to "qr_green.png".
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator using the QR symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR code
            generator.CodeText = "Hello, Aspose!";

            // Apply a green background color to the image
            generator.Parameters.BackColor = Color.Green;

            // Set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated QR code as a PNG file in the current directory
            generator.Save("qr_green.png");
        }
    }
}