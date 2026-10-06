// Title: Generate Code128 barcode with custom XDimension and handle missing YDimension
// Description: Demonstrates how to set the XDimension of a barcode to 0.5 mm using Aspose.BarCode and notes that YDimension is not supported in the current API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create and save barcode images. Typical scenarios include generating product labels, inventory tags, or QR codes where precise module sizing is required. Developers often need to adjust dimensions, symbology, and output formats, making this a reference for common barcode creation tasks.
// Prompt: Set XDimension to 0.5 mm and YDimension to 30 mm before generating the barcode image.
// Tags: code128, barcode generation, image output, xdimension, ydimension, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode image with a custom XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define and ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the XDimension (module width) to 0.5 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // YDimension is not exposed in the current Aspose.BarCode API; inform the user
            Console.WriteLine("YDimension property is not available in this version of Aspose.BarCode.");

            // Render and save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}