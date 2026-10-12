// Title: Generate Code128 barcode with visible code text positioned below and custom offset
// Description: Demonstrates how to create a Code128 barcode, enable the human‑readable code text, place it beneath the bars, and adjust the vertical spacing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include labeling products, assets, or documents where readable text is required alongside the barcode. Developers often need to control text location and spacing for better visual integration.
// Prompt: Create a barcode, enable ShowCodeText, and position text below bars with custom vertical offset.
// Tags: code128, barcode, showcodetext, textposition, verticaloffset, generation, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with the code text displayed
/// below the bars and a custom vertical offset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, generates the barcode,
    /// saves it as a PNG file, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define and ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Show the human‑readable code text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom vertical offset (spacing) between the barcode and the text
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 10f;

            // Save the generated barcode image as PNG
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outPath}");
    }
}