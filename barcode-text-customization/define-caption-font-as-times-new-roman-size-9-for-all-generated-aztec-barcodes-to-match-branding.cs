// Title: Set caption font for Aztec barcode generation
// Description: Demonstrates how to configure the caption font to Times New Roman, size 9, for an Aztec barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers often need to customize barcode appearance, such as fonts and visibility of captions, for branding or documentation purposes. The snippet shows typical steps: creating a generator, setting caption fonts, and saving the image.
// Prompt: Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match branding.
// Tags: aztec, caption, font, png, barcodegenerator, encode-types, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting caption fonts for an Aztec barcode and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an Aztec barcode with custom caption fonts and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "AztecDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the text to encode and the full path for the resulting image
        string codeText = "Sample Aztec";
        string outputPath = Path.Combine(outputDir, "AztecBarcode.png");

        // Initialize the barcode generator for Aztec symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, codeText))
        {
            // Configure caption fonts: Times New Roman, 9‑point size
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 9f;
            generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionBelow.Font.Size.Point = 9f;

            // Make captions visible and assign sample text to demonstrate the font settings
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Above Caption";
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Below Caption";

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Aztec barcode generated at: " + outputPath);
    }
}