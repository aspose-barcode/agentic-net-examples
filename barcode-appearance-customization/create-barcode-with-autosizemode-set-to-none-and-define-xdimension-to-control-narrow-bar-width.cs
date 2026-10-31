// Title: Generate Code128 Barcode with Fixed XDimension and No AutoSize
// Description: Demonstrates creating a Code128 barcode, disabling auto‑size, and setting the narrow bar width via XDimension.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce custom-sized barcodes. Developers often need to control barcode dimensions for precise layout requirements, such as setting a fixed narrow bar width and turning off automatic sizing. The snippet shows typical steps for configuring generation parameters and saving the result as an image.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: code128, barcode, autosizemode, xdimension, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with a fixed narrow bar width
/// and disables automatic sizing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates the output folder, configures the barcode generator,
    /// saves the barcode image, and writes the result path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic size calculation to allow manual dimension control
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the narrow bar width (XDimension) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}