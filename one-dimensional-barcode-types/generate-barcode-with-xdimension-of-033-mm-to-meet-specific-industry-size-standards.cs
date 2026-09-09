// Title: Generate Code128 barcode with specific XDimension (0.33 mm)
// Description: Demonstrates how to set the XDimension of a Code128 barcode to 0.33 mm using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to create barcodes with precise module dimensions to comply with industry size standards, such as retail or logistics labeling. The snippet shows typical steps: initializing the generator, configuring barcode parameters, and exporting the image.
// Prompt: Generate a barcode with XDimension of 0.33 mm to meet specific industry size standards.
// Tags: code128, xdimension, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a custom XDimension and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXDimExample");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the XDimension (module width) to 0.33 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Render and save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}