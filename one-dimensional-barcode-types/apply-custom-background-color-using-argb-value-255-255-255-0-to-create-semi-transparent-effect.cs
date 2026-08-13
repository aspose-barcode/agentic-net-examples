// Title: Apply semi‑transparent background color to QR code barcode
// Description: Demonstrates how to set a custom ARGB background color on a QR code using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and drawing parameters to customize barcode appearance. Typical use cases include branding, UI overlays, and visual emphasis where developers need to modify background colors, transparency, or other visual properties before exporting the barcode.
// Prompt: Apply a custom background color using ARGB value (255,255,255,0) to create a semi‑transparent effect.
// Tags: qr code, background color, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a semi‑transparent background color to a QR code and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code with a custom ARGB background and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the background color to semi‑transparent white (ARGB 255,255,255,0).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Export the barcode as a PNG image file.
            generator.Save("barcode.png");
        }

        // Inform the user that the file has been created.
        Console.WriteLine("Barcode image saved to barcode.png");
    }
}