// Title: Create barcode with custom font and right-aligned text
// Description: Demonstrates how to generate a Code128 barcode with human‑readable text using Calibri 13 pt font and align the text to the right of the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include branding, product labeling, and custom UI where specific font styling and text positioning are required. Developers often need to adjust font family, size, and alignment to match design guidelines.
// Prompt: Create a barcode with custom text font Calibri, size 13 pt, and align text right of the barcode.
// Tags: code128, barcode generation, custom font, text alignment, png, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a Code128 barcode with custom text styling
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures font and alignment,
    /// then saves the result to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the human‑readable text font to Calibri, 13 pt.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode image as a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}