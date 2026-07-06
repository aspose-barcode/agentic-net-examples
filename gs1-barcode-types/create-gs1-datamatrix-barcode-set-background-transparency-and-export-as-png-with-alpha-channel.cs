// Title: Create GS1 DataMatrix barcode with transparent background and PNG output
// Description: Demonstrates generating a GS1 DataMatrix barcode, applying a fully transparent background, and saving it as a PNG that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and drawing parameters to control visual appearance, a common requirement when integrating barcodes into UI designs that need transparent backgrounds.
// Prompt: Create a GS1 DataMatrix barcode, set background transparency, and export as PNG with an alpha channel.
// Tags: gs1 datamatrix, background transparency, png, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a GS1 DataMatrix barcode with a transparent background and exporting it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, sets transparency, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (Application Identifier (01) for GTIN)
        string codeText = "(01)01234567890123";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the background color to be fully transparent (alpha = 0)
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);

            // Save the barcode as a PNG file; the alpha channel preserves transparency
            generator.Save("gs1_datamatrix.png");
        }
    }
}