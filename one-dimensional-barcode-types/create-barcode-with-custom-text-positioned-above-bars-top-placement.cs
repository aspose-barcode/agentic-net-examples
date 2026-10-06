// Title: Generate Code128 barcode with custom text positioned above the bars
// Description: Demonstrates how to create a Code128 barcode and place the human‑readable text above the barcode symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text placement, alignment, and spacing. Developers often need to customize the appearance of barcodes for labeling, packaging, and inventory systems, and this snippet shows the typical API calls for positioning text relative to the bars.
// Prompt: Create a barcode with custom text positioned above the bars (top placement).
// Tags: code128, barcode generation, text placement, above, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with the code text displayed above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures the barcode generator,
    /// sets text placement parameters, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample");
        Directory.CreateDirectory(outputDir);

        // Full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "barcode_top.png");

        // Initialize the barcode generator with Code128 symbology and custom text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CustomText"))
        {
            // Position the code text above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Center the text horizontally relative to the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Set the spacing between the text and the barcode bars (5 points).
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}