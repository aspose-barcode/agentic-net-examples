// Title: Export Code39 Barcode with Custom Text to SVG
// Description: Demonstrates generating a Code39 barcode, customizing its human‑readable text, and exporting it as an SVG file for scalable vector rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and rendering parameters to create barcodes. Typical use cases include producing printable or web‑ready barcodes with vector output, customizing appearance, and ensuring high‑quality scaling. Developers often need to adjust colors, fonts, and layout before saving to formats like SVG, PNG, or PDF.
// Prompt: Export barcodes with customized text to SVG format to retain vector‑based alignment for scalable rendering.
// Tags: code39, barcode generation, svg, aspose.barcode, barcodgenerator, custom text, vector rendering

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code39 barcode with customized human‑readable text
/// and saves it as an SVG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies visual customizations, and writes the SVG output.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code39 (evaluation version limitation)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "1234567890"))
        {
            // Set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Configure vector‑based rendering (SVG) with interpolation sizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300;   // Width in points
            generator.Parameters.ImageHeight.Point = 100;  // Height in points

            // Customize the human‑readable text appearance
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.DarkBlue;

            // Save the generated barcode as an SVG file (vector format)
            generator.Save("custom_barcode.svg");
        }

        // Inform the user that the file has been created
        Console.WriteLine("Barcode saved as custom_barcode.svg");
    }
}