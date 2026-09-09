// Title: Generate a dense MaxiCode barcode with custom module size
// Description: Demonstrates how to create a MaxiCode barcode and set its module size to produce a denser image suitable for compact labels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension. Developers often need to generate high‑density MaxiCode symbols for shipping, inventory, or compact label applications, adjusting module size to fit space constraints while maintaining scan reliability.
// Prompt: Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compact labels.
// Tags: maxicode, barcode, generation, xdimension, module size, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode barcode with a custom module size
/// to generate a denser image suitable for compact labeling scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode,
    /// configures its module size, saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "maxicode.png");

        // Create a BarcodeGenerator for MaxiCode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set module size to 2 (denser image) via XDimension
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optional: set a specific MaxiCode mode if required (default is Auto)
            // generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}