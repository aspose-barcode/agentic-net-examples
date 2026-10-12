// Title: Generate Code128 barcode with lime foreground and save as high‑resolution TIFF
// Description: This example creates a Code128 barcode, applies a custom lime foreground color, and saves it as a 300 dpi TIFF image.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting barcode symbology, customizing visual appearance (color, resolution), and exporting to high‑quality image formats. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which are commonly employed by developers to produce printable barcodes for inventory, shipping, and labeling scenarios.
// Prompt: Generate a barcode with custom foreground color #00FF00 (lime) and save as a high‑quality TIFF file.
// Tags: code128, barcode generation, tiff, aspose.barcode, aspose.drawing, color customization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom lime foreground color
/// and saving it as a high‑resolution TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures appearance,
    /// and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output TIFF file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.tiff");

        // Create a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode's foreground color to lime (#00FF00).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 255, 0);

            // Define a high resolution (300 DPI) for the output image.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved file.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}