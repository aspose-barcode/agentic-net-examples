// Title: Create barcode image without human‑readable text
// Description: Demonstrates how to generate a Code128 barcode and hide the human‑readable text by setting the code text location to none, then saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure barcode appearance using BarcodeGenerator and its Parameters. Typical use cases include creating machine‑readable barcodes for packaging or inventory where the text label is not required. Developers often need to adjust CodeTextParameters such as location, font, and visibility to meet design specifications.
// Prompt: Create a barcode with ShowCodeText disabled to produce an image without human‑readable text.
// Tags: code128, hidecodetext, barcode generation, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image without displaying the human‑readable code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, disables code text display, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory with a unique name
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Disable the human‑readable text by setting its location to none
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}