// Title: Generate EAN13 barcode with Point font size and save as PNG
// Description: Demonstrates setting human‑readable text font size using Unit.Point and creating an EAN13 barcode saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure text appearance with FontUnit, set colors, and output common image formats. It uses BarcodeGenerator, EncodeTypes, and related parameter classes, which developers frequently employ to embed barcodes in documents, labels, or web pages.
// Prompt: Use Unit.Point for FontUnit of human‑readable text, then generate EAN13 barcode saved as PNG.
// Tags: ean13, barcode generation, png, aspose.barcode, aspose.drawing, fontunit, point

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an EAN13 barcode with human‑readable text sized in points and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures the barcode generator, sets font size using Point units, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the EAN13 symbology with a 12‑digit value.
        // The checksum digit is calculated automatically.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
        {
            // ----- Configure human‑readable text -----
            // Set the font family for the code text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";

            // Use Point units to define the font size (12 points).
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center the text horizontally relative to the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Position the text below the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // ----- Optional visual styling -----
            // Set the barcode (bars) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // ----- Save the barcode image -----
            // The image is saved in PNG format with the specified file name.
            generator.Save("ean13.png");
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine("EAN13 barcode generated and saved as ean13.png");
    }
}