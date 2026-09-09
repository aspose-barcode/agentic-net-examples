// Title: Create barcode with custom background color and export as GIF
// Description: Demonstrates how to generate a Code128 barcode with a custom light‑blue background and dark‑blue bars, then save it as a GIF image suitable for web pages.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance (background and bar colors) and export to web‑friendly formats. Developers often need to adjust visual styling of barcodes for branding or UI integration and require GIF output for lightweight web delivery.
// Prompt: Create a barcode with custom background color and export it as a GIF image for web use.
// Tags: barcode, code128, background color, gif, aspose.barcode, generation, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom background color and saves it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies styling, and writes the file to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_bg_barcode.gif");
        string directory = Path.GetDirectoryName(outputPath);

        // Ensure the target directory exists.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a light blue background for the entire image.
            generator.Parameters.BackColor = Color.FromArgb(255, 173, 216, 230);
            // Set the barcode bars to dark blue.
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Save the generated barcode as a GIF file.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}