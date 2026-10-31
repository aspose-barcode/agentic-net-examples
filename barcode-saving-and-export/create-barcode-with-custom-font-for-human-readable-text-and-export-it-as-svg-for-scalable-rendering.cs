// Title: Generate Code39 Barcode with Custom Font and Export as SVG
// Description: Demonstrates creating a Code39 barcode, applying a custom Helvetica font to the human‑readable text, and saving the result as an SVG file for scalable rendering.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize code‑text appearance using FontMode.Manual and export the barcode to vector formats. It showcases key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use when they need high‑quality, scalable barcodes for web or print.
// Prompt: Create a barcode with custom font for human‑readable text and export it as SVG for scalable rendering.
// Tags: code39, custom font, human readable text, svg, barcode generation, aspose.barcode, vector export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode with a custom font for the human‑readable text
/// and saves it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_font_barcode.svg");

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a Code39 barcode generator with the sample text "12345"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Configure the code‑text to use a manual font
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the human‑readable text below the barcode (default behavior)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Attempt to save the barcode as an SVG file, handling potential evaluation‑license limitations
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Detect evaluation‑license related errors and provide a friendly message
                if (ex.Message != null && ex.Message.IndexOf("evaluation", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine("A valid license is required for SVG export of this symbology.");
                }
                else
                {
                    Console.WriteLine($"Error saving barcode: {ex.Message}");
                }
            }
        }
    }
}