// Title: Generate GS1 DataMatrix barcode with transparent background and PNG output
// Description: Demonstrates creating a GS1 DataMatrix barcode, applying a transparent background, and saving it as a PNG file that includes an alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 DataMatrix symbology. It shows how to configure barcode appearance using the BarcodeGenerator class, set background colors with Aspose.Drawing, and export images in formats that support transparency such as PNG. Developers working with product identification, inventory, or logistics often need to generate GS1 DataMatrix codes and embed them in graphics with transparent backgrounds for seamless UI integration.
// Prompt: Create a GS1 DataMatrix barcode, set background transparency, and export as PNG with an alpha channel.
// Tags: gs1datamatrix, barcode generation, transparent background, png, alpha channel, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies transparency, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (AI 01 with a 14‑digit GTIN)
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix using the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the background to be transparent so the saved PNG contains an alpha channel
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Save the generated barcode as a PNG file (PNG supports alpha transparency)
            generator.Save("gs1datamatrix.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("GS1 DataMatrix barcode generated: gs1datamatrix.png");
    }
}