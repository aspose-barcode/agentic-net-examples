// Title: Adjust XDimension for Code39 Barcode to Increase Bar Width
// Description: Demonstrates how to increase the XDimension of a Code39 barcode using Aspose.BarCode, resulting in wider bars and lower visual density suitable for print media.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance by modifying size parameters. It uses the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create a PNG image. Developers often need to adjust XDimension to meet printing requirements, improve scan reliability, or match branding guidelines.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, xdimension, barwidth, png, aspose.barcode, generation, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code39 barcode with an increased XDimension to produce wider bars,
/// then saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// adjusts the XDimension, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "Code39_XDimension.png");

        // Initialize the barcode generator for Code39 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Increase XDimension to make bars wider (reduces visual density)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Ensure AutoSizeMode is set to None so the barcode size adapts to the new XDimension
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}