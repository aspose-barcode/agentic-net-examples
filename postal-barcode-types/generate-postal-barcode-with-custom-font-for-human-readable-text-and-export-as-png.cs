// Title: Generate Postal Barcode with Custom Font and PNG Output
// Description: Demonstrates creating a Postnet postal barcode, applying a custom Helvetica font to the human‑readable text, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as code text location, font, size, and alignment using the BarcodeGenerator and its Parameters properties. Typical use cases include generating postal barcodes for mailing applications where custom styling of the human‑readable text is required. Developers often need to customize font attributes and export the barcode to common image formats like PNG.
// Prompt: Generate a postal barcode with a custom font for the human‑readable text and export as PNG.
// Tags: postnet, custom-font, png, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Postnet postal barcode with a custom font for the human‑readable text
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures text appearance, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define output file name and the ZIP code to encode.
        const string outputPath = "postal.png";
        const string codeText = "12345"; // ZIP code for Postnet

        // Initialize the barcode generator with Postnet symbology and the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Position the human‑readable text below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set a custom, cross‑platform font (Helvetica) and size for the text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center‑align the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}