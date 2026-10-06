// Title: Center Aligned Barcode with Automatic Scaling
// Description: Generates a Code128 barcode with the human‑readable text centered and automatically scales the barcode to fit a narrow image width.
// Category-Description: This example demonstrates Aspose.BarCode generation features, focusing on text alignment and auto‑size modes. It uses the BarcodeGenerator class with EncodeTypes, TextAlignment, AutoSizeMode, and image dimension settings. Developers creating barcodes for limited‑space layouts often need to center the caption and let the library adjust the barcode size to fit a specific width.
// Prompt: Align barcode text to center and enable automatic scaling to fit within narrow barcode width.
// Tags: barcode, code128, text-alignment, autosize, scaling, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with centered human‑readable text
/// and automatic scaling to fit a narrow image width using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode,
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the generated barcode image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "centered_scaled_barcode.png");

        // Ensure the target directory exists.
        string dir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Center align the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic scaling to fit the barcode within a narrow canvas.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 150f; // narrow canvas width
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // optional module size

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}