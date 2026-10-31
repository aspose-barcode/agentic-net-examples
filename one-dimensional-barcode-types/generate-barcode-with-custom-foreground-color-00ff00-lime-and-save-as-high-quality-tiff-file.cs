// Title: Generate Lime-Colored Code128 Barcode and Save as High‑Quality TIFF
// Description: This example creates a Code128 barcode with a custom lime foreground color (#00FF00) and saves it as a high‑resolution TIFF image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with custom visual styling. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure bar color and output format. Developers often need to customize barcode appearance and export to lossless image formats for printing or archival purposes.
// Prompt: Generate a barcode with custom foreground color #00FF00 (lime) and save as a high‑quality TIFF file.
// Tags: code128, barcode, color, tiff, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a lime foreground color
/// and saves it as a high‑quality TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output TIFF file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "LimeBarcode.tiff");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the foreground (bar) color to lime (#00FF00).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 255, 0);

            // Save the generated barcode as a high‑quality TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}