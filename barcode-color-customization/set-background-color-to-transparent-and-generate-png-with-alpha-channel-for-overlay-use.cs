// Title: Generate Transparent PNG Barcode with Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode with a transparent background and saving it as a PNG that retains the alpha channel for overlay use.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class, set background colors, and export images in formats that support transparency such as PNG. Developers often need to produce barcodes that can be layered on top of other graphics without obscuring underlying content, making transparent PNG output a common requirement.
// Prompt: Set the background color to transparent and generate a PNG with alpha channel for overlay use.
// Tags: barcode, code128, transparent background, png, alpha channel, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the transparent PNG barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a transparent background and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Choose the barcode symbology (Code128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample";

        // Initialize the barcode generator with the selected type and text.
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Configure the generator to use a transparent background.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode as a PNG image, which preserves the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}