// Title: Generate MaxiCode barcode with custom aspect ratio
// Description: Demonstrates creating a MaxiCode barcode and setting its aspect ratio to 1.5 to control width‑to‑height proportion.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure MaxiCode symbology using the BarcodeGenerator class. Typical use cases include customizing barcode dimensions for packaging and shipping labels where specific aspect ratios are required. Developers often need to adjust XDimension and aspect ratio properties to meet printing specifications.
// Prompt: Create a MaxiCode barcode with aspect ratio 1.5 to adjust width‑to‑height proportion.
// Tags: maxicode, aspect ratio, barcode generation, png output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a MaxiCode barcode with a custom aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it as PNG, and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "MaxiCodeAspectRatio1.5.png");

        // Initialize barcode generator for MaxiCode symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 15;

            // Adjust the aspect ratio to 1.5 (width to height)
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}