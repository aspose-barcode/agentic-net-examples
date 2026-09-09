// Title: Apply custom text color to a barcode using Aspose.BarCode
// Description: Demonstrates how to set a custom color for the barcode's human‑readable text while keeping the bar and background colors at their default values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize visual aspects of generated barcodes. It highlights the use of BarcodeGenerator, EncodeTypes, and Color settings to meet common requirements such as branding or visual emphasis. Developers often need to adjust text, bar, or background colors for integration into UI designs or printed materials.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: barcode symbology, color customization, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom text color to a barcode while leaving bar and background colors at defaults.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with green text and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeColorDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated PNG file.
        string outputPath = Path.Combine(tempDir, "customTextColor.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a custom color (green) for the human‑readable text; bar and background colors stay default.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Save the barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}