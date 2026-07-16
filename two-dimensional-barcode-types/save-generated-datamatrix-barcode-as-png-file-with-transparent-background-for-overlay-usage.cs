// Title: Save DataMatrix barcode as PNG with transparent background
// Description: Generates a DataMatrix barcode and saves it as a PNG file with a transparent background, ideal for overlay usage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to create barcodes using the BarcodeGenerator class, configure visual properties such as background transparency, and export to common image formats. Developers often need to produce barcodes for UI overlays, reports, or print media where background blending is required.
// Prompt: Save generated DataMatrix barcode as PNG file with transparent background for overlay usage.
// Tags: datamatrix, barcode, generation, png, transparent, background, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as a PNG with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures colors, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "datamatrix.png";

        // Ensure the target directory exists; create it if necessary
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize a DataMatrix barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
        {
            // Set the background color to transparent so the barcode can be overlaid on other images
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the barcode (foreground) color; default is black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG file preserving the transparent background
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}