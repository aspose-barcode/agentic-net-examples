// Title: Set Barcode Text Font and Alignment for Decorative Labels
// Description: Demonstrates how to set the human‑readable text font to Comic Sans MS, size 11 pt, and center it beneath a Code128 barcode, then save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and CodeTextParameters to customize barcode appearance. Developers commonly need to adjust font, size, and alignment of human‑readable text for branding, labeling, and decorative purposes. Typical use cases include creating product labels, tickets, and promotional materials where visual style matters.
// Prompt: Set barcode text font to Comic Sans MS, size 11 pt, and align text centrally for decorative labels.
// Tags: code128, font, png, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with customized text font and alignment,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures text appearance,
    /// saves the image, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample value "DecorativeLabel"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DecorativeLabel"))
        {
            // Configure the human‑readable text font to Comic Sans MS, 11 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the text centrally beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG file
            generator.Save("decorative_label.png");
        }

        // Inform the user that the barcode has been generated and saved
        Console.WriteLine("Barcode generated and saved as decorative_label.png");
    }
}