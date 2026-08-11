// Title: Create Code128 barcode with custom Verdana font and left-aligned text
// Description: Demonstrates how to generate a Code128 barcode using Aspose.BarCode, set the human‑readable text to Verdana 10 pt, and align the text to the left of the bars. The example saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include creating barcodes with specific font styles and text alignment for labeling and packaging applications. Developers often need to adjust font family, size, and alignment to meet branding or layout requirements.
// Prompt: Create a barcode with custom text font Verdana, size 10 pt, and align text left of the bars.
// Tags: code128, barcode generation, text formatting, png output, aspose.barcode, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom text font and alignment using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode
            generator.CodeText = "Sample123";

            // Configure the human‑readable text font: Verdana, 10 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Align the human‑readable text to the left side of the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated: barcode.png");
    }
}