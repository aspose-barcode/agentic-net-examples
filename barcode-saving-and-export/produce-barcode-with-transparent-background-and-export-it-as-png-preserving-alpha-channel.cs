// Title: Generate a Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a barcode with a transparent background and exporting it as a PNG while preserving the alpha channel.
// Prompt: Produce a barcode with transparent background and export it as PNG preserving the alpha channel.
// Tags: code128, barcode, transparent background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a transparent background
/// and saves it as a PNG image preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures colors, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "transparent_barcode.png";

        // Initialize the barcode generator with the desired symbology (Code128) and data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the background to be fully transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the foreground (bars) color; black is used here
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file; transparency is retained in the output
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the absolute path of the saved image for user reference
        Console.WriteLine("Barcode saved to: " + Path.GetFullPath(outputPath));
    }
}