// Title: Generate Code128 barcode with specific XDimension (0.33 mm)
// Description: This example creates a Code128 barcode image with an XDimension of 0.33 mm, demonstrating how to control module size for industry standards.
// Category-Description: Aspose.BarCode generation examples showing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes for packaging, labeling, and inventory. Developers often need to adjust dimensions, symbology, and output formats to meet regulatory or branding requirements. This snippet illustrates setting the XDimension property and saving the result as a PNG, a common workflow in automated barcode creation pipelines.
// Prompt: Generate a barcode with XDimension of 0.33 mm to meet specific industry size standards.
// Tags: barcode, code128, xdimension, millimeters, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom XDimension of 0.33 mm.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "Barcode_XDim_0_33mm.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the XDimension (module width) to 0.33 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }
}