// Title: Adjust XDimension for Code39 barcode to reduce visual density
// Description: Demonstrates how to increase the XDimension (module width) of a Code39 barcode using Aspose.BarCode, resulting in wider bars and lower density—useful for print media where readability is critical.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance parameters such as XDimension. It uses the BarcodeGenerator class with EncodeTypes.Code39 and shows saving the result as a PNG image. Developers working with barcode generation often need to tweak visual properties to meet printing or scanning requirements, and this snippet provides a concise reference.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, xdimension, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code39 barcode with an increased XDimension to produce wider bars,
/// then saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, configures the barcode,
    /// saves it, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary directory for the output file.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "Code39_XDimension.png");

        // Initialize the barcode generator for Code39 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "HELLO123"))
        {
            // Increase the XDimension (module width) to 4 pixels to reduce visual density.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}