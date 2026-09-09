// Title: Generate a Code128 barcode with lime foreground and save as TIFF
// Description: This example creates a Code128 barcode, applies a custom lime foreground color, and saves the result as a high‑quality TIFF image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It covers setting barcode symbology (EncodeTypes), customizing visual appearance (BarColor), and exporting to a specific image format (BarCodeImageFormat). Typical use cases include creating barcodes for product labeling, inventory tracking, and packaging where color branding and high‑resolution output are required. Developers often need to adjust colors and output formats to match branding guidelines and printing standards.
// Prompt: Generate a barcode with custom foreground color #00FF00 (lime) and save as a high‑quality TIFF file.
// Tags: code128, barcode generation, tiff, color, aspose.barcode, barcode symbology, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a lime foreground color
/// and saves it as a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output TIFF file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_lime.tiff");

        // Create a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode's foreground color to lime (#00FF00).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 0, 255, 0);

            // Save the generated barcode as a high‑quality TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}