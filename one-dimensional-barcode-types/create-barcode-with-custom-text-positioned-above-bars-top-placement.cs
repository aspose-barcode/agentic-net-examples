// Title: Create Code128 barcode with text positioned above the bars
// Description: Demonstrates generating a Code128 barcode and placing the human‑readable text above the barcode symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, CodeLocation, and BarCodeImageFormat to customize barcode appearance. Typical use cases include creating printable labels, inventory tags, or product packaging where text placement is required. Developers often need to adjust text location, spacing, and output format when integrating barcode creation into .NET applications.
// Prompt: Create a barcode with custom text positioned above the bars (top placement).
// Tags: code128, text placement, barcode generation, png, aspose.barcode, barcode symbology

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
    /// Entry point of the example. Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image.
        string outPath = Path.Combine(outputDir, "barcode_top.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Position the human‑readable text above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Set the spacing between the text and the barcode (5 points).
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the barcode image as PNG to the specified path.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}