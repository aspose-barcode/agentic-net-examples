// Title: Apply custom LightCoral background to QR barcode
// Description: Demonstrates generating a QR barcode with a LightCoral background color and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows setting visual parameters such as background color, a common requirement when integrating barcodes into themed UI designs. Developers often need to adjust colors, sizes, and formats to match application branding.
// Prompt: Apply a custom background color named “LightCoral” to match UI theme requirements.
// Tags: qr, barcode, background color, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode with a LightCoral background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies the background color, saves the image, and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for a QR code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the background color to LightCoral to match UI theme requirements
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightCoral;

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated successfully
        Console.WriteLine("Barcode generated with LightCoral background: barcode.png");
    }
}