// Title: Generate Code128 barcode with manual sizing for screen display
// Description: Demonstrates disabling auto‑size, setting XDimension, and saving a low‑resolution PNG suitable for screen rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions and resolution using the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to produce barcodes that render clearly on low‑resolution displays or when precise sizing is required, such as in web or mobile applications. The snippet shows typical usage of AutoSizeMode, XDimension, and resolution settings.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, autosizemode, xdimension, lowresolution, png, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with manual sizing,
/// optimized for low‑resolution screen display.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures sizing and resolution, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing to allow manual dimension control.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the module (X) dimension to a larger value (2 points) for better visibility on low‑resolution screens.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optionally lower the image resolution to 72 DPI, matching typical screen resolution.
            generator.Parameters.Resolution = 72f;

            // Save the configured barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}