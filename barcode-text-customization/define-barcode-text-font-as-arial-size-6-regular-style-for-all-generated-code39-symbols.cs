// Title: Set custom font for Code39 barcode text
// Description: Demonstrates how to define the human‑readable text font (Arial, 6 pt, regular) for a Code39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class. Typical use cases include setting font properties, text location, and saving the barcode as an image. Developers often need to adjust font settings to match branding or printing requirements.
// Prompt: Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols.
// Tags: code39, font, barcode generation, png, aspose.barcode, barcode text

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code39 barcode with custom font settings for the human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures font properties, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a Code39 barcode generator with the sample value "CODE39".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Configure the font for the human‑readable text: Arial, 6 points, regular style.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

            // Position the human‑readable text below the barcode graphic.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG image file.
            generator.Save("code39.png");
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("Barcode image saved as code39.png");
    }
}