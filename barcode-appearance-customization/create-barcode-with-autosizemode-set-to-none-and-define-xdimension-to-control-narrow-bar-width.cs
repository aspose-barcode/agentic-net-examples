// Title: Generate Code128 barcode with custom XDimension and AutoSizeMode disabled
// Description: Demonstrates how to create a Code128 barcode, explicitly set AutoSizeMode to None, and define XDimension to control the narrow bar width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to customize barcode dimensions, such as disabling auto-sizing and setting XDimension, to meet specific layout or printing requirements. The snippet shows typical steps for configuring these properties and saving the result as a PNG image.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: barcode, code128, autosizemode, xdimension, png, barcodgenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures dimensions, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output folder and ensure it exists
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputFolder);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Explicitly set AutoSizeMode to None (default is None, but set for clarity)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set XDimension to define the narrow bar width (2 points in this case)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}