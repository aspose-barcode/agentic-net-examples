// Title: Generate Aztec barcode with Times New Roman caption font
// Description: Demonstrates creating an Aztec barcode and setting caption fonts to Times New Roman, size 9, for branding consistency.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure caption appearance for Aztec symbology using BarcodeGenerator and its Parameters API. Typical use cases include adding branded text above or below barcodes in reports, packaging, or inventory labels. Developers often need to customize font family, size, and visibility to match corporate style guidelines.
// Prompt: Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match branding.
// Tags: aztec, barcode, caption, font, times new roman, size 9, generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an Aztec barcode with customized caption fonts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates an Aztec barcode with captions,
    /// saves it as PNG, and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Build a unique temporary directory for the output file.
        string outputDir = Path.Combine(Path.GetTempPath(), "AztecDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "AztecBarcode.png");

        // Initialize the barcode generator for Aztec symbology with sample text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Aztec, "Sample Text"))
        {
            // Configure the caption displayed above the barcode.
            gen.Parameters.CaptionAbove.Visible = true;
            gen.Parameters.CaptionAbove.Text = "Above Caption";
            gen.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            gen.Parameters.CaptionAbove.Font.Size.Point = 9f;

            // Configure the caption displayed below the barcode.
            gen.Parameters.CaptionBelow.Visible = true;
            gen.Parameters.CaptionBelow.Text = "Below Caption";
            gen.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
            gen.Parameters.CaptionBelow.Font.Size.Point = 9f;

            // Optional: adjust the barcode's module size (pixel dimension).
            gen.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG image.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Aztec barcode saved to: {outputPath}");
    }
}