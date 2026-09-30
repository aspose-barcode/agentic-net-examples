// Title: Set PDF417 barcode background and bar colors
// Description: Demonstrates how to set a light gray background and dark gray bar color for a PDF417 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a PDF417 barcode with specific colors, a common requirement for branding, printing, and improving scan reliability. Developers often need to adjust foreground and background colors to match design guidelines or enhance contrast for various output media.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, background, barcolor, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with a light gray background and dark gray bars,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies color settings,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "pdf417.png");

        // Initialize the barcode generator for PDF417 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text"))
        {
            // Apply a light gray background to the entire image.
            generator.Parameters.BackColor = Color.LightGray;

            // Set the barcode (foreground) color to dark gray.
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Render and save the barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}