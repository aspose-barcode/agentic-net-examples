// Title: Adjust barcode text font and alignment
// Description: Demonstrates how to set the barcode text font to Arial 12 pt, position it below the bars, and center it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include creating readable barcodes with styled human‑readable text for packaging, inventory, and point‑of‑sale applications. Developers often need to adjust font, size, location, and alignment of the code text to meet branding or layout requirements.
// Prompt: Adjust barcode text font to Arial, size 12 pt, and center the text beneath the bars.
// Tags: barcode, code128, font, alignment, textlocation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with customized text font and alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to the Output folder.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text font to Arial, 12 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Position the text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Center the text horizontally
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}