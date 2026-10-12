// Title: Generate Code128 barcode with Verdana bold font
// Description: Creates a Code128 barcode image, setting the human‑readable text to Verdana bold 14 pt and placing it below the bars.
// Category-Description: This example demonstrates Aspose.BarCode generation features, focusing on customizing the barcode's code‑text appearance. It uses BarcodeGenerator, EncodeTypes, and CodeTextParameters to control font family, style, size, and location. Typical use cases include producing printable barcodes with clear, readable text for inventory, shipping, or retail applications. Developers often need to adjust font settings to match branding or readability requirements.
// Prompt: Set barcode text font to Verdana, bold style, size 14 pt for improved readability.
// Tags: code128, barcode, font, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode image with customized
/// code‑text font settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, applies Verdana bold
    /// 14 pt font to the code text, saves the image as PNG, and writes the
    /// output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        try
        {
            // Initialize the barcode generator with Code128 symbology and sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure the code‑text font: manual mode, Verdana family, bold style, 14 pt size.
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

                // Position the human‑readable text below the barcode bars.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the generated barcode as a PNG image to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}