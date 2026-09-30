// Title: Generate low‑resolution barcode with custom XDimension
// Description: Demonstrates creating a Code128 barcode, disabling auto‑sizing, setting a larger XDimension, and saving it as a PNG suitable for low‑resolution screen displays.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcodes for web or mobile applications where screen DPI is low. Developers often need to control sizing, module width, and output format to ensure readability on devices with limited resolution.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, barcode, autosizemode, xdimension, lowresolution, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom sizing options
/// for low‑resolution displays.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures sizing,
    /// and saves the image to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Disable automatic size adjustment (AutoSizeMode.None is the default).
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Increase the XDimension (module width) to improve visibility on low‑resolution screens.
            generator.Parameters.Barcode.XDimension.Point = 2f; // module width in points

            // Set the target screen resolution (e.g., 72 DPI) to match low‑resolution displays.
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}