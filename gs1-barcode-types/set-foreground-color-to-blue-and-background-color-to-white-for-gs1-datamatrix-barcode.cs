// Title: Set colors for a GS1 DataMatrix barcode
// Description: Demonstrates how to generate a GS1 DataMatrix barcode image with a blue foreground and white background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Parameters to customize barcode appearance. Developers often need to adjust colors for branding or readability when creating DataMatrix barcodes for product identification, packaging, or inventory systems.
// Prompt: Set foreground color to blue and background color to white for a GS1 DataMatrix barcode.
// Tags: gs1 datamatrix, color, png, barcodegenerator, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 DataMatrix barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates and saves a colored GS1 DataMatrix barcode image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 DataMatrix code text with AI (01) and a 14‑digit GTIN
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix using the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the barcode's foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image file
            generator.Save("gs1datamatrix.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("GS1 DataMatrix barcode generated: gs1datamatrix.png");
    }
}