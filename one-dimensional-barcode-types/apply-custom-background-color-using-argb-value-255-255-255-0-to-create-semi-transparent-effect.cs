// Title: Apply Semi‑Transparent Background Color to Barcode
// Description: Demonstrates setting a custom ARGB background color on a barcode image to achieve a semi‑transparent effect and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of generated barcodes using the BarcodeGenerator class. It shows configuring rendering parameters such as background color, a common requirement when integrating barcodes into UI designs or reports. Developers often need to adjust colors, sizes, and formats to match branding or layout constraints.
// Prompt: Apply a custom background color using ARGB value (255,255,255,0) to create a semi‑transparent effect.
// Tags: code128, background-color, png, barcodelibrary, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a semi‑transparent background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies a custom background color, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply a semi‑transparent background color using ARGB (255, 255, 255, 0)
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Define the output file path for the PNG image
            string outputPath = "barcode.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}