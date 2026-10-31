// Title: Apply Custom Text Color to a Barcode Using Aspose.BarCode
// Description: Demonstrates how to set a custom color for the barcode's human‑readable text while keeping the bar and background colors at their default values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance via the BarcodeGenerator and its Parameters. It covers setting text color, a common requirement when integrating barcodes into branded documents or UI designs. Developers often need to adjust text styling without affecting the barcode symbology itself, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Apply a custom text color to a barcode while leaving bar and background colors at defaults.
// Tags: barcode, text color, customization, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom text color while leaving bar and background colors at their defaults.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, generates the barcode, applies the custom text color, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode_custom_text_color.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a custom color to the human‑readable text; bar and background colors remain default
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}