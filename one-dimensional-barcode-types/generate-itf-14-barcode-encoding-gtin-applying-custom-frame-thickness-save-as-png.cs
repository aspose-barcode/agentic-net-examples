// Title: Generate ITF‑14 barcode with custom frame thickness and save as PNG
// Description: This example creates an ITF‑14 barcode encoding a GTIN, applies a custom frame border thickness, and saves the image as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the ITF‑14 symbology, focusing on border customization. The example uses BarcodeGenerator, EncodeTypes, and ITF14BorderType classes to illustrate typical tasks such as setting border type, thickness, colors, and exporting to PNG. Ideal for developers needing to produce packaging barcodes with specific visual requirements.
// Prompt: Generate ITF‑14 barcode encoding GTIN, applying custom frame thickness, save as PNG.
// Tags: itf-14, barcode-generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an ITF‑14 barcode with a custom frame border
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its appearance,
    /// and writes the result to a file.
    /// </summary>
    static void Main()
    {
        // GTIN for ITF‑14 (14 numeric characters)
        const string gtin = "01234567890123";

        // Desired frame thickness in points
        const float frameThickness = 5f;

        // Initialize the barcode generator for ITF‑14 with the GTIN value
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, gtin))
        {
            // Set the border type to a frame around the barcode
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Apply the custom frame thickness
            generator.Parameters.Barcode.ITF.BorderThickness.Point = frameThickness;

            // Optional: define bar and background colors (black on white)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file
            generator.Save("itf14.png");
        }
    }
}