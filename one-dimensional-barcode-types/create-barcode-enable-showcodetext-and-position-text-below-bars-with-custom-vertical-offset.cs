// Title: Generate Code128 Barcode with Text Below and Custom Offset
// Description: Demonstrates creating a Code128 barcode, displaying the human‑readable code text below the bars, and applying a custom vertical spacing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It covers setting CodeTextParameters such as location and spacing, which are common tasks when developers need readable text positioned relative to the barcode for labeling, packaging, or inventory systems.
// Prompt: Create a barcode, enable ShowCodeText, and position text below bars with custom vertical offset.
// Tags: code128, barcode generation, showcodetext, text location, vertical offset, aspnet, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, shows the code text below the bars,
/// and applies a custom vertical offset to the text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Show the human‑readable text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set a custom vertical offset (spacing) of 12 points between bars and text
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 12f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}