// Title: Generate Code128 Barcode with Text Above Bars and Custom Offset
// Description: This example creates a Code128 barcode, displays the human‑readable text above the bars, and applies a small vertical offset before saving it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It covers setting barcode symbology (EncodeTypes), configuring CodeTextParameters for text visibility and positioning, and saving the result in a common image format. Ideal for developers implementing product labeling, inventory tracking, or any scenario requiring customizable barcode rendering.
// Prompt: Generate a barcode, set ShowCodeText to true, and position text above bars with a small vertical offset.
// Tags: code128, barcode, generation, showcodetext, textposition, offset, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, shows the code text above the bars,
/// applies a small vertical offset, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the human‑readable text to appear above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Apply a small vertical spacing (offset) of 5 points between the text and the bars.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Render and save the barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}