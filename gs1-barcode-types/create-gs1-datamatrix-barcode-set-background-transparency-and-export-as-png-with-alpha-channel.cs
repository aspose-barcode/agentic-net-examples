// Title: Create GS1 DataMatrix barcode with transparent background and save as PNG
// Description: Demonstrates generating a GS1 DataMatrix barcode, making its background fully transparent, and exporting the image as a PNG that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image rendering options such as background transparency and PNG output with alpha channel support. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create GS1 DataMatrix symbols, a common requirement for supply‑chain labeling where transparent backgrounds are needed for overlaying on various media.
// Prompt: Create a GS1 DataMatrix barcode, set background transparency, and export as PNG with an alpha channel.
// Tags: gs1datamatrix, barcode, background-transparency, png, alpha-channel, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 DataMatrix barcode with a fully transparent background
/// and saves it as a PNG image that preserves the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures visual settings,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path where the PNG image will be saved.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1datamatrix.png");

        // GS1 DataMatrix payload: Application Identifier (01) for GTIN and (21) for serial number.
        string codeText = "(01)12345678901231(21)ASPOSE";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the payload.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the background color to fully transparent (alpha = 0).
            generator.Parameters.BackColor = Color.FromArgb(0, 0, 0, 0);

            // Define the size of a single module (pixel) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode as a PNG file preserving the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}