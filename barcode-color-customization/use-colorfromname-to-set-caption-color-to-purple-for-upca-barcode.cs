// Title: Set Caption Color to Purple for UPC-A Barcode
// Description: Demonstrates generating a UPC-A barcode with Aspose.BarCode and setting the caption color using Color.FromName.
// Prompt: Use Color.FromName to set caption color to "Purple" for a UPC-A barcode.
// Tags: barcode, upc-a, caption, color, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a UPC-A barcode image and sets the caption color to purple.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures caption color, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a UPC-A barcode generator with a valid 12‑digit code
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Set the caption text (optional) and apply purple color using Color.FromName
            generator.Parameters.CaptionAbove.Text = "UPC‑A Sample";
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Save the generated barcode as a PNG file
            generator.Save("upc_a.png");
        }
    }
}