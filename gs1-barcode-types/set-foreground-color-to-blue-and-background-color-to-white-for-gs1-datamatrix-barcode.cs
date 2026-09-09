// Title: Set Colors for GS1 DataMatrix Barcode using Aspose.BarCode
// Description: Demonstrates how to generate a GS1 DataMatrix barcode with a blue foreground and white background using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance such as colors, dimensions, and image format. It uses the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and drawing Color settings. Developers often need to adjust visual properties of barcodes for branding or readability, and this snippet shows the typical steps.
// Prompt: Set foreground color to blue and background color to white for a GS1 DataMatrix barcode.
// Tags: gs1 datamatrix, barcode, color, foreground, background, aspose.barcodes, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 DataMatrix barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// saves the barcode image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "gs1_datamatrix.png");

        // GS1 DataMatrix payload (application identifiers 01 and 21).
        string codeText = "(01)12345678901231(21)ASPOSE";

        // Initialize the barcode generator with the desired symbology and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Define the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}