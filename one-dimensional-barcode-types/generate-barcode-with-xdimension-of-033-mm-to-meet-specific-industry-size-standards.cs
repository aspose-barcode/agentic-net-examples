// Title: Generate Code128 barcode with specific XDimension
// Description: Demonstrates creating a Code128 barcode, setting its XDimension to 0.33 mm, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class. Developers often need to adjust XDimension to meet industry size standards for scanning reliability. Typical use cases include customizing barcode size for packaging, labeling, and compliance with printing specifications.
// Prompt: Generate a barcode with XDimension of 0.33 mm to meet specific industry size standards.
// Tags: code128, barcode generation, png output, xdimension, aspnet.barcode, barcodegenerator

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with a custom XDimension and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures its XDimension, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the XDimension (module width) to 0.33 millimeters as required by industry standards.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the generated barcode image in PNG format.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been successfully generated.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}