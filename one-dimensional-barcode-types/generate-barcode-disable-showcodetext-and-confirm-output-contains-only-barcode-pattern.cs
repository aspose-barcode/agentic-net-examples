// Title: Generate Code128 barcode without human‑readable text
// Description: Demonstrates how to create a Code128 barcode image, disable the displayed code text, and save it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode graphics. Typical scenarios include creating barcodes for inventory, shipping labels, or product packaging where only the machine‑readable pattern is required. Developers often need to hide the human‑readable text to reduce visual clutter or meet design guidelines.
/// Prompt: Generate a barcode, disable ShowCodeText, and confirm output contains only the barcode pattern.
/// Tags: code128, barcode, generation, hidecodetext, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode image with the human‑readable text disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable the human‑readable code text (ShowCodeText equivalent)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created and inform the user
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode image generated at: {outputPath}");
            Console.WriteLine("ShowCodeText disabled – image contains only the barcode pattern.");
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}