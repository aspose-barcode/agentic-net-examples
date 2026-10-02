// Title: Generate EAN13 barcode with point-sized human‑readable text and save as PNG
// Description: Demonstrates how to create an EAN‑13 barcode, set the human‑readable text font size using points, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to produce product barcodes. Typical scenarios include creating retail labels, inventory tags, and packaging graphics where precise font sizing and placement of human‑readable text are required. Developers often need to control font units, location, and output format when integrating barcode creation into .NET applications.
// Prompt: Use Unit.Point for FontUnit of human‑readable text, then generate EAN13 barcode saved as PNG.
// Tags: ean13, barcode, generation, png, fontunit, point, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an EAN‑13 barcode, configures the human‑readable text
/// using point units for the font size, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies font settings, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        string outputPath = "ean13.png";

        // Initialize the barcode generator with EAN‑13 symbology and a valid 13‑digit value.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the human‑readable text font size using points (Unit.Point).
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Optional: specify the font family for the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";

            // Position the human‑readable text below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}