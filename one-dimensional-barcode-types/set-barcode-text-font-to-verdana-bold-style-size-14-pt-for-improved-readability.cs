// Title: Set barcode text font to Verdana bold 14pt using Aspose.BarCode
// Description: Demonstrates how to change the human‑readable text font of a barcode to Verdana, bold style, 14 pt, and save the image.
// Category-Description: This example belongs to the Aspose.BarCode appearance‑customization category, illustrating how to modify barcode text styling using the BarcodeGenerator class. It covers setting font family, style, and size via CodeTextParameters, a common requirement when generating readable barcodes for print or screen. Developers often need to adjust these properties to match branding guidelines or improve legibility.
// Prompt: Set barcode text font to Verdana, bold style, size 14 pt for improved readability.
// Tags: barcode symbology, text formatting, code128, image output, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with customized text font.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample value "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the human‑readable text font:
            //   - Font family: Verdana
            //   - Font style: Bold
            //   - Font size: 14 points
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG image
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("Barcode generated: barcode.png");
    }
}