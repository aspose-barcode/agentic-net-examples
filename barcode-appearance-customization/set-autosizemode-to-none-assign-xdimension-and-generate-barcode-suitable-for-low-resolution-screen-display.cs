// Title: Generate Low‑Resolution Code128 Barcode with Fixed X‑Dimension
// Description: Demonstrates how to create a Code128 barcode optimized for low‑resolution screen display by disabling auto‑sizing and setting a specific X‑dimension.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rendering parameters such as AutoSizeMode, XDimension, and Resolution. Developers often need to produce barcodes that render clearly on low‑dpi displays or screens, requiring explicit control over module size and image resolution. The snippet shows typical steps for configuring these settings and saving the barcode as a PNG image.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, barcode generation, low resolution, autosizemode, xdimension, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a low‑resolution Code128 barcode with a fixed X‑dimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the output folder, configures the barcode generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "LowResBarcode.png");

        // Initialize the generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LOWRES"))
        {
            // Disable automatic sizing to allow manual control of dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the X‑dimension (module width) to 2 pixels for better visibility on low‑dpi screens.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Define the image resolution (96 DPI) suitable for screen display.
            generator.Parameters.Resolution = 96f;

            // Save the barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}