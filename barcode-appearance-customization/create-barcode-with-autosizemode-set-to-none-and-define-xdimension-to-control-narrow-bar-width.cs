// Title: Create Code128 barcode with custom XDimension and no auto sizing
// Description: Demonstrates how to generate a Code128 barcode, disable automatic sizing, and set the narrow bar width using XDimension. The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and generation parameters such as AutoSizeMode and XDimension. Developers often need to control exact bar dimensions for printing or scanning requirements, and this snippet shows typical steps for creating and saving a barcode image with custom sizing.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: code128, autosizemode, xdimension, png, generation, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom dimensions and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full file path for the PNG image
        string filePath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing so we can set dimensions manually
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the narrow bar width (XDimension) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {filePath}");
    }
}