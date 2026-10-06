// Title: Right-Aligned Human-Readable Text in a Code128 Barcode
// Description: Demonstrates how to generate a Code128 barcode with the human‑readable text aligned to the right edge of the image and saved as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and related settings to control the appearance of human‑readable text. Typical scenarios include creating barcodes for packaging, shipping labels, or inventory systems where precise text placement is required. Developers often need to adjust alignment, location, and image format to meet branding or regulatory guidelines.
// Prompt: Align barcode text to the right, positioning human‑readable characters at the far right of the image.
// Tags: code128, text-alignment, png, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode with right‑aligned human‑readable text and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures text alignment, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "RightAlignedBarcode.png");
        // The data to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Align the human‑readable text to the right edge of the image.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            // Position the text below the barcode (explicitly set for clarity).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}