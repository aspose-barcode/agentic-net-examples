// Title: Generate a DataMatrix barcode with transparent background and save as PNG
// Description: Demonstrates creating a DataMatrix barcode, setting a transparent background, and saving it as a PNG file suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DataMatrix, configure visual parameters such as BackColor and BarColor, and export the image in PNG format. Developers often need to produce barcodes with transparent backgrounds for UI overlays, reports, or composite images, and this snippet shows the typical API usage for those cases.
// Prompt: Save generated DataMatrix barcode as PNG file with transparent background for overlay usage.
// Tags: datamatrix, barcode, generation, transparent background, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the DataMatrix barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with a transparent background and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrixTransparent.png");

        // Create a BarcodeGenerator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleDataMatrix"))
        {
            // Set the background to transparent so the barcode can be overlaid on other images.
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally ensure the barcode bars are black (default color).
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}