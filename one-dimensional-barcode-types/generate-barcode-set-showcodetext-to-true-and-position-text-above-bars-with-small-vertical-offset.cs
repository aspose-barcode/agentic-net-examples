// Title: Generate Code128 barcode with text above bars
// Description: Demonstrates how to create a Code128 barcode, enable the human‑readable text, position it above the bars, and apply a small vertical offset before saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator and related parameter classes (CodeTextParameters, CodeLocation, TextAlignment). Typical use cases include creating printable barcodes with customized text placement for inventory, shipping, or retail applications. Developers often need to adjust text location, alignment, and spacing to meet branding or layout requirements.
// Prompt: Generate a barcode, set ShowCodeText to true, and position text above bars with a small vertical offset.
// Tags: code128, barcode generation, showcodetext, text above, vertical offset, png, aspose.barcode, barcodgenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with human‑readable text positioned above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures text display, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code128 with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Show the human‑readable text and place it above the bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Apply a small vertical offset (2 points) between the text and the bars.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2f;

            // Center the text horizontally.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image.
            generator.Save("barcode.png");
        }

        // Output the location of the generated file.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}