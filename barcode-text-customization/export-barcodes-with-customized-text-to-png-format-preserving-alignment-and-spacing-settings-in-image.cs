// Title: Export PDF417 barcode with custom text to PNG
// Description: Generates a PDF417 barcode, customizes the displayed text alignment, spacing, and font, and saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑Dimension, code‑text location, alignment, spacing, and manual font settings. It demonstrates using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce customized barcode images, a common requirement for developers creating printable or digital barcode assets.
// Prompt: Export barcodes with customized text to PNG format, preserving alignment and spacing settings in the image.
// Tags: pdf417, barcode, export, png, custom-text, alignment, spacing, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a PDF417 barcode with customized text settings and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates output directory, configures barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the temporary output directory for the demo
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExportDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Set the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "custom_text.png");

        // Initialize the barcode generator with PDF417 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text"))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the code text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Align the code text to the right side
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            // Define spacing between the barcode and the text in pixels
            generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 40f;

            // Use manual font settings for the code text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
            // Choose Helvetica as the font family
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            // Set the font size in pixels
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 12f;

            // Save the configured barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}