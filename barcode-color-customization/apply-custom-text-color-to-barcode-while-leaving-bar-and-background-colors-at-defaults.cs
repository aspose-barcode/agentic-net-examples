// Title: Apply custom text color to a Code128 barcode
// Description: Demonstrates how to set a custom color for the barcode's human‑readable text while keeping the bar and background colors at their default values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Developers often need to adjust text styling (color, font) without affecting the barcode itself, especially for branding or visual integration in reports and UI.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: code128, custom text color, png, barcodegenerator, codetextparameters, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom text color and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies a green text color, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        string outputPath = "customTextColorBarcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply a custom text color (green). Bar and background colors remain at their defaults.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}