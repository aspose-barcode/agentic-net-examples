// Title: Set Bottom Caption Padding for DataMatrix Barcode
// Description: Demonstrates adding a bottom caption to a DataMatrix barcode and configuring its bottom padding to 8 pixels, providing visual separation between the caption and the symbol.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting caption visibility, text, and padding—common tasks when integrating barcodes into reports, labels, or UI elements where clear labeling is required. Developers often need to adjust caption layout to meet design guidelines, and this snippet illustrates the essential API calls for such customizations.
// Prompt: Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation from the symbol.
// Tags: datamatrix, caption, padding, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with a visible bottom caption and custom bottom padding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode,
    /// saves it as a PNG file, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory path.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "DataMatrixWithBottomCaption.png");

        // Initialize the barcode generator for a DataMatrix symbology with the given data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "1234567890"))
        {
            // Enable the caption that appears below the barcode.
            generator.Parameters.CaptionBelow.Visible = true;
            // Set the caption text.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            // Apply an 8‑pixel bottom padding to separate the caption from the symbol.
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8;
            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}