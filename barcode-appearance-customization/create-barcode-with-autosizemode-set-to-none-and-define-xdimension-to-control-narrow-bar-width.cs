// Title: Generate Code128 Barcode with Fixed XDimension and No AutoSize
// Description: Demonstrates creating a Code128 barcode, disabling automatic sizing, and setting the narrow bar width via XDimension.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce custom barcodes. Typical use cases include precise control over barcode dimensions for printing or UI display, where developers need to set properties like AutoSizeMode and XDimension to meet layout requirements.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: code128, barcode, autosizemode, xdimension, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with AutoSizeMode set to None
/// and a custom XDimension to control the narrow bar width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, configures the barcode generator,
    /// saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "Barcode_None_XDimension.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
        {
            // Explicitly set AutoSizeMode to None (prevents automatic size adjustments)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Define the narrow bar width via XDimension (2 pixels in this example)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}