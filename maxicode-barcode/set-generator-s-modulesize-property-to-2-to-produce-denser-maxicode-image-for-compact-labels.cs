// Title: Generate a dense MaxiCode barcode with custom module size
// Description: Demonstrates how to create a MaxiCode barcode image and set a smaller XDimension to increase density for compact labels.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension for MaxiCode symbology. Developers often need to adjust module size to fit barcodes on limited space, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. The snippet shows typical usage for creating PNG images of barcodes in .NET applications.
// Prompt: Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compact labels.
// Tags: maxicode, barcode, generation, xdimension, densify, png, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode with a denser module size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode barcode, adjusts its XDimension,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeDense.png");

        // Initialize the barcode generator for MaxiCode symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the XDimension (module size) to 2 points to increase barcode density.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}