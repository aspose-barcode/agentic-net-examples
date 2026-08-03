// Title: Generate a Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a Code128 barcode, setting a transparent background, and exporting it to a PNG file while preserving the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include creating barcodes for web or UI overlays where background transparency is required. Developers often need to adjust colors, backgrounds, and export formats using the Parameters property and Save method.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: code128, barcode generation, transparent background, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode, applies a transparent background, and saves it.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background color to transparent so the PNG retains the alpha channel.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Save the generated barcode as a PNG file; transparency is preserved.
            generator.Save("transparent_barcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated with transparent background.");
    }
}