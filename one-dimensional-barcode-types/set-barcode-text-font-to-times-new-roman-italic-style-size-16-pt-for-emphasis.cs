// Title: Set barcode text font to Times New Roman italic 16pt
// Description: Demonstrates how to customize the human‑readable text font of a Code128 barcode using Aspose.BarCode, saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode font customization category, illustrating the use of BarcodeGenerator and CodeTextParameters to modify text appearance. Developers often need to match branding guidelines or emphasize barcode data, and these APIs provide fine‑grained control over font family, style, and size.
// Prompt: Set barcode text font to Times New Roman, italic style, size 16 pt for emphasis.
// Tags: code128, set-font, png, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeFontExample
{
    /// <summary>
    /// Shows how to set the barcode's human‑readable text font to Times New Roman, italic, 16 pt, and save the image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates a Code128 barcode with custom font settings and writes the image to disk.
        /// </summary>
        static void Main()
        {
            // Initialize a BarcodeGenerator for Code128 with the sample value "Sample123"
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Configure the font used for the human‑readable (code text) part of the barcode
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = Aspose.Drawing.FontStyle.Italic;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;

                // Save the generated barcode as a PNG file
                generator.Save("barcode.png");
            }

            // Inform the user that the barcode has been created
            Console.WriteLine("Barcode generated with custom font.");
        }
    }
}