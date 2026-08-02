// Title: Generate Code128 Barcode with Custom XDimension
// Description: Demonstrates how to generate a Code128 barcode image and set the XDimension to 3 pixels for proper 1D element width.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QualitySettings to control barcode dimensions. Developers often need to customize module width (XDimension) to meet printing standards or scanner requirements. The snippet shows typical steps: creating a generator, disabling auto‑size, setting XDimension, and saving the image.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: barcode, code128, generation, png, xdimension, aspose.barcode, qualitysettings

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode image with a custom XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a barcode PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing so the manually set XDimension is respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the XDimension (module width) to 3 pixels, matching typical 1D barcode element widths.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}