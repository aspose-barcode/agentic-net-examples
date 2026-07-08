// Title: Generate Code128 barcode with custom text above the bars
// Description: Demonstrates how to create a Code128 barcode and position the human‑readable text on top of the bars, useful for labeling where the text must appear above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Developers often need to adjust text location, font, and color for branding or compliance, and this snippet shows the typical API calls for such customizations.
// Prompt: Create a barcode with custom text positioned above the bars (top placement).
// Tags: code128, barcode, text placement, png, aspose.barcode, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom text placed above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, customizes the text appearance, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "Custom Text";

            // Position the human‑readable text above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Optional: customize the font family, size, and color of the above text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Blue;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }

        // Output a simple confirmation message.
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}