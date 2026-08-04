// Title: Generate EAN13 barcode with point-sized human‑readable text
// Description: Demonstrates creating an EAN13 barcode, setting the human‑readable font size using Unit.Point, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as CodeText and FontUnit. It uses the BarcodeGenerator and related parameter classes to produce barcodes for retail and inventory applications. Developers often need to customize human‑readable text appearance and export barcodes in common image formats.
// Prompt: Use Unit.Point for FontUnit of human‑readable text, then generate EAN13 barcode saved as PNG.
// Tags: ean13, barcode, generation, png, fontunit, point, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an EAN13 barcode,
/// sets the human‑readable text font size using Point units,
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the EAN13 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13))
        {
            // Assign a 12‑digit code; the checksum digit is calculated automatically.
            generator.CodeText = "123456789012";

            // Set the human‑readable text font size to 12 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image as a PNG file.
            generator.Save("ean13.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("EAN13 barcode generated and saved as ean13.png");
    }
}