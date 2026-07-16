// Title: Save Han Xin barcode as GIF with limited palette
// Description: Demonstrates generating a Han Xin barcode and saving it as a GIF image using a minimal two‑color palette, ideal for web deployment.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode parameters such as symbology, error correction, and colors, then export to a GIF format. It showcases key classes like BarcodeGenerator, EncodeTypes, and HanXinErrorLevel, which developers use when creating barcodes for web pages, emails, or other bandwidth‑sensitive environments. The snippet serves as a reference for quickly producing compact, web‑friendly barcode images.
// Prompt: Save Han Xin barcode as GIF image with limited color palette for web use.
// Tags: hanxin, barcode, gif, color-palette, generation, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Han Xin barcode and saving it as a GIF image with a minimal color palette.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Text to encode in the Han Xin barcode.
        const string codeText = "Han Xin Sample 123";

        // Initialize a BarcodeGenerator for Han Xin symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Optional: set the error correction level for Han Xin (L2 provides higher reliability).
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Configure barcode colors: black bars on a white background.
            // Using only two colors keeps the GIF palette minimal (max 256 colors).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a GIF image.
            // GIF format inherently uses a limited color palette, making it suitable for web use.
            generator.Save("HanXinBarcode.gif");
        }

        // Output a simple confirmation message.
        Console.WriteLine("Han Xin barcode saved as GIF with a limited color palette.");
    }
}