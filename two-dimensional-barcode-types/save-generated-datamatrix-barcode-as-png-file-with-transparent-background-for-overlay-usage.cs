// Title: Save DataMatrix barcode as PNG with transparent background
// Description: Demonstrates generating a DataMatrix barcode and saving it as a PNG file with a transparent background, suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting background transparency and bar color, then exporting to PNG. Developers working with barcode creation for UI overlays, document stamping, or image compositing often need these settings.
// Prompt: Save generated DataMatrix barcode as PNG file with transparent background for overlay usage.
// Tags: datamatrix, png, transparent background, barcode generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a DataMatrix barcode and saving it as a PNG with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.png");

        // Initialize a DataMatrix barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Configure the barcode to have a transparent background (useful for overlaying on other images)
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the bar (foreground) color; default is black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file preserving the transparent background
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}