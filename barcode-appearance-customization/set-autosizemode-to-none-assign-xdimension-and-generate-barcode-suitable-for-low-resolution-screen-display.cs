// Title: Generate low‑resolution Code128 barcode with custom XDimension
// Description: This example creates a Code128 barcode, disables automatic sizing, sets a specific XDimension, and saves the image as PNG for display on low‑resolution screens.
// Category-Description: Demonstrates Aspose.BarCode generation techniques, focusing on sizing and resolution control. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes suitable for web or UI scenarios where screen DPI is limited. Developers often need to fine‑tune XDimension and AutoSizeMode to ensure readability on low‑resolution displays.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, barcode, autosizemode, xdimension, lowresolution, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a low‑resolution Code128 barcode with custom sizing settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures sizing, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "LowResBarcode.png");
        // Text to encode in the barcode
        string codeText = "LOWRES";

        // Initialize generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable automatic sizing; we will define dimensions manually
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set narrow bar width (XDimension) in pixels for low‑resolution rendering
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Define image resolution (DPI) typical for screen display
            generator.Parameters.Resolution = 96f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}