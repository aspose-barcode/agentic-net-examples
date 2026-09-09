// Title: Set custom font for barcode text using Aspose.BarCode
// Description: Demonstrates how to configure the barcode text font to Verdana, bold style, 14 pt size, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize text appearance in barcodes. It uses BarcodeGenerator, EncodeTypes, and CodeTextParameters classes to modify font properties, a common requirement when creating readable barcodes for packaging, inventory, or retail labels. Developers often need to adjust font family, style, and size to match branding guidelines or improve scan reliability.
// Prompt: Set barcode text font to Verdana, bold style, size 14 pt for improved readability.
// Tags: barcode, code128, font, text formatting, png, aspose.barcode, generation, customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting a custom font for barcode text and saving the barcode as an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with Verdana bold 14 pt text and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable manual font mode to allow custom font settings.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the font family to Verdana, apply bold style, and specify a 14 pt size.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}