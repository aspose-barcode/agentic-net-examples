// Title: Generate GS1 Code 128 barcode with custom human‑readable font and save as PNG
// Description: Demonstrates creating a GS1 Code 128 barcode, customizing the font of the human‑readable text, and exporting the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure CodeTextParameters (location, font family, size), and save the barcode in a raster format. Developers working with product labeling, inventory tracking, or any GS1‑compliant applications often need to customize the appearance of the human‑readable text while generating barcodes programmatically.
// Prompt: Create a GS1 Code 128 barcode, apply a custom font for human‑readable text, and save as PNG.
// Tags: gs1code128, barcode generation, custom font, png output, aspose.barcode, code text parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with custom human‑readable text font and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures visual parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "GS1Code128.png");
        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with GS1 Code 128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1"))
        {
            // Set the X-dimension (module width) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Place human‑readable text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Apply custom font settings for the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}