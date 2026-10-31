// Title: Generate GS1 DataMatrix barcode with transparent background and save as PNG
// Description: Demonstrates creating a GS1 DataMatrix barcode, applying a transparent background, and exporting it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix. It shows typical tasks such as configuring visual parameters (background color, X‑dimension) and saving the result in a format that supports transparency. Developers working with product identification, inventory, or packaging often need to generate GS1 DataMatrix symbols and embed them in graphics with alpha channels.
// Prompt: Create a GS1 DataMatrix barcode, set background transparency, and export as PNG with an alpha channel.
// Tags: gs1datamatrix, barcode, generation, transparency, png, aspose.barcode, background, alpha channel

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1datamatrix.png");

        // Set the GS1 DataMatrix payload (including Application Identifier 01).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the payload.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Make the barcode background fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Define the module size (X dimension) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode as a PNG file that supports an alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}