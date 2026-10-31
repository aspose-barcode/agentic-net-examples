// Title: Generate Code128 barcode with custom text position and offset
// Description: Demonstrates creating a Code128 barcode, displaying the human‑readable code text below the bars, and applying a custom vertical spacing between the bars and the text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with EncodeTypes, CodeLocation, and CodeTextParameters to customize barcode appearance. Typical use cases include producing printable barcodes with readable text positioned according to layout requirements, a common need for inventory, shipping, and retail applications. Developers often need to control text visibility, location, and spacing to meet branding or regulatory standards.
// Prompt: Create a barcode, enable ShowCodeText, and position text below bars with custom vertical offset.
// Tags: code128, barcode, showcodetext, textposition, verticaloffset, png, aspose.barcode, generation

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
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the human‑readable text to appear below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Apply a custom vertical spacing (10 points) between the bars and the text.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 10f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}