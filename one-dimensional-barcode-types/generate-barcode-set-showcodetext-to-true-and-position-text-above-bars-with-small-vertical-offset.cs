// Title: Generate Code128 barcode with visible text positioned above the bars
// Description: Demonstrates creating a Code128 barcode, enabling the human‑readable text, and placing that text above the barcode with a small vertical offset.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using BarcodeGenerator, CodeTextParameters, and related classes. Typical use cases include adding readable text to barcodes for packaging, inventory, or retail applications where the text must be displayed separately from the bars. Developers often need to adjust text location, spacing, and output format when integrating barcodes into documents or UI screens.
// Prompt: Generate a barcode, set ShowCodeText to true, and position text above bars with a small vertical offset.
// Tags: code128, barcode, showcodetext, textposition, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode image with the human‑readable text displayed above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, configures text display, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Position the human‑readable text above the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Apply a small vertical offset (5 points) between the text and the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode as a PNG image to the specified file path.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}