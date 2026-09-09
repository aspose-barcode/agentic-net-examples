// Title: Generate MaxiCode barcode with custom quiet zone
// Description: Demonstrates creating a MaxiCode barcode and applying custom quiet zone padding to meet specific scanning requirements.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension, Padding, and MaxiCode mode. Developers often need to customize barcode size, quiet zones, and output format for integration into packaging, shipping labels, and inventory systems.
// Prompt: Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requirements.
// Tags: maxicode, barcode, quiet zone, padding, generation, aspnet, aspnetcore, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with custom quiet zone padding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "maxicode.png");

        // Initialize the barcode generator for MaxiCode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode Text"))
        {
            // Set the module (pixel) size of the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Apply custom quiet zone padding (20 points on each side)
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optionally set the MaxiCode mode (default is Mode4)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}