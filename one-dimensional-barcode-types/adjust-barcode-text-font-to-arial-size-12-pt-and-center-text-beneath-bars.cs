// Title: Adjust barcode text font and alignment
// Description: Demonstrates how to set the barcode's human‑readable text to Arial 12 pt and center it beneath the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize CodeTextParameters such as font, size, alignment, and location. It uses the BarcodeGenerator class together with EncodeTypes and CodeTextParameters to produce barcodes with tailored human‑readable text. Developers often need to modify these settings to match branding guidelines or improve readability in printed materials.
// Prompt: Adjust barcode text font to Arial, size 12 pt, and center the text beneath the bars.
// Tags: barcode, code128, font, alignment, textlocation, generation, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates adjusting barcode text font to Arial 12 pt and centering it below the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with customized text appearance and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample value "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the human‑readable text font: Arial, 12 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Align the text to the center and position it below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated: barcode.png");
    }
}