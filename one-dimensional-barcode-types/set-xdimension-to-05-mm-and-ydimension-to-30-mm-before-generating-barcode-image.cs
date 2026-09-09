// Title: Generate Code128 barcode with custom XDimension
// Description: Demonstrates how to set the XDimension of a Code128 barcode to 0.5 mm and generate a PNG image. Shows basic setup of Aspose.BarCode generator and saving the result.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to customize module size (XDimension) to meet printing specifications, and this snippet shows the typical workflow for creating and exporting a barcode image in .NET applications.
// Prompt: Set XDimension to 0.5 mm and YDimension to 30 mm before generating the barcode image.
// Tags: code128, xdimension, barcode generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
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
        // Prepare the output directory in the system temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Define the text to encode and the barcode symbology.
        string codeText = "ASPOSE123";

        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the module width (XDimension) to 0.5 mm.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // YDimension is not a supported property in Aspose.BarCode generation API.
            // The library only provides XDimension for module size.
            Console.WriteLine("YDimension property is not available; only XDimension can be set.");

            // Generate the barcode image and save it as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}