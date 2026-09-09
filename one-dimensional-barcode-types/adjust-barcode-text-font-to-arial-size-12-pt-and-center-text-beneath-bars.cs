// Title: Set barcode text font and alignment for Code128 barcode
// Description: Demonstrates how to change the barcode text font to Arial 12 pt and center it below the bars using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize CodeTextParameters such as font, size, location, and alignment. It showcases the BarcodeGenerator and related parameter classes, which developers commonly use to produce barcodes with tailored human‑readable text for labeling, inventory, and packaging applications.
// Prompt: Adjust barcode text font to Arial, size 12 pt, and center the text beneath the bars.
// Tags: code128, set-font, png, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, customizes the human‑readable text font and alignment,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode,
    /// and writes the image file to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the text font to Arial, 12 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the text below the bars and center it horizontally
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}