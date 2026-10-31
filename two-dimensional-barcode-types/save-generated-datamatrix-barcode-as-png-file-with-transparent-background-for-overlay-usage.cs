// Title: Save DataMatrix barcode as PNG with transparent background
// Description: Demonstrates generating a DataMatrix barcode and saving it as a PNG image with a transparent background, suitable for overlaying on other graphics.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting background and bar colors, selecting the DataMatrix symbology, and exporting to PNG format. Developers working with barcode creation for UI overlays, document stamping, or custom graphics will find this pattern useful.
// Prompt: Save generated DataMatrix barcode as PNG file with transparent background for overlay usage.
// Tags: datamatrix, barcode, generation, png, transparent background, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode and saves it as a PNG image with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "DataMatrixTransparent.png");

        // Initialize the barcode generator for DataMatrix with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
        {
            // Set the background to transparent and the bars to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}