// Title: Generate a dense MaxiCode barcode with custom module size
// Description: Demonstrates how to set the module size of a MaxiCode barcode to produce a denser image suitable for compact labels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension to customize barcode appearance. Developers often need to adjust module size for different printing requirements, especially for dense symbologies like MaxiCode. The snippet shows creating a generator, configuring the module size, and saving the result as PNG.
// Prompt: Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compact labels.
// Tags: maxicode, barcode, module size, densify, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a denser module size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a MaxiCode barcode, sets a custom module size, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting image file
        string outputPath = Path.Combine(outputDir, "MaxiCodeDense.png");

        // Initialize a MaxiCode generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Configure the module size to 2 points for a denser barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}