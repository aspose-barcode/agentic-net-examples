// Title: Generate QR Code with custom colors
// Description: Demonstrates creating a QR Code barcode with a blue foreground and white background, saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows setting bar and background colors for QR Code symbology, a common requirement when integrating barcodes into branding or UI designs. Developers often need to adjust colors, size, and format before saving the image.
// Prompt: Generate a QR Code barcode with foreground color blue and background color white, saved as PNG.
// Tags: qr code, barcode generation, color customization, png output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with custom colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code with blue bars on a white background and writes it to "qr.png".
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the color of the barcode bars (foreground) to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG file in the current directory.
            generator.Save("qr.png");
        }
    }
}