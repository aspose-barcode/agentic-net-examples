// Title: Generate Code128 barcode with MistyRose background color
// Description: Demonstrates how to create a Code128 barcode and apply a custom MistyRose background for a soft pastel look.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and color customization. Developers often need to generate barcodes with specific visual styles for branding or UI integration, and this snippet illustrates setting background colors and saving the image in PNG format.
// Prompt: Apply a custom background color named “MistyRose” to create a soft pastel appearance for the barcode.
// Tags: code128, background-color, png, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a MistyRose background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it as PNG, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Combine directory and file name to get the full output path
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply the custom MistyRose background color for a soft pastel appearance
            generator.Parameters.BackColor = Color.MistyRose;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}