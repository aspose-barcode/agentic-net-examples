// Title: Save DataMatrix barcode as PNG with transparent background
// Description: Demonstrates generating a DataMatrix barcode and saving it as a PNG image with a transparent background, suitable for overlay scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class, set image format, and apply background transparency. Developers often need to create barcodes for UI overlays, reports, or composite images, and this snippet shows the typical steps for generating a PNG with transparency.
// Prompt: Save generated DataMatrix barcode as PNG file with transparent background for overlay usage.
// Tags: datamatrix, barcode, generation, png, transparent background, aspose.barcode, asposedrawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DataMatrix barcode and saves it as a PNG file with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures visual settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Create the directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "DataMatrixTransparent.png");

        // Initialize the barcode generator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Aspose"))
        {
            // Set the module size (pixel dimension) of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Make the background transparent so the barcode can be overlaid on other graphics.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Save the barcode as a PNG file, preserving the transparent background.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}