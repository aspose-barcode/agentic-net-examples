// Title: Create Code128 barcode with right-aligned Calibri text
// Description: Demonstrates how to generate a Code128 barcode, set the human‑readable text font to Calibri 13 pt, and align the text to the right of the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize human‑readable text. Typical use cases include branding, product labeling, and custom UI where specific font styling and text placement are required. Developers often need to adjust font family, size, and alignment to match design guidelines.
// Prompt: Create a barcode with custom text font Calibri, size 13 pt, and align text right of the barcode.
// Tags: code128, barcode generation, custom font, text alignment, png, aspose.barcode, aspose.barcode.generation, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom font and right-aligned text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, applies font settings, aligns text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the data to encode in the barcode
            generator.CodeText = "Sample123";

            // Set the human‑readable text font to Calibri, 13 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Position the text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image file
            generator.Save("custom_barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as 'custom_barcode.png'.");
    }
}