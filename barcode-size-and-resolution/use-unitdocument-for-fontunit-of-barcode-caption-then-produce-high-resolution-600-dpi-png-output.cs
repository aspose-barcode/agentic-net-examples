// Title: Generate 600 dpi PNG barcode with caption using Document unit for font size
// Description: Demonstrates creating a Code128 barcode, setting a 600 dpi resolution, and applying a caption whose font size is specified in Document units, then saving as a high‑resolution PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode resolution, caption visibility, and font sizing using the FontUnit.Document property. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to produce printable, high‑quality barcodes for packaging, inventory, and labeling solutions.
// Prompt: Use Unit.Document for FontUnit of barcode caption, then produce high‑resolution 600 dpi PNG output.
// Tags: barcode, code128, generation, high-resolution, png, caption, fontunit, document, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a caption,
/// sets the resolution to 600 dpi, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_600dpi.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the image resolution to 600 dots per inch for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Enable and configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Caption Above";

            // Set the caption font family.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";

            // Specify the font size using Document units (1/300 inch per unit).
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f; // 12 document units

            // Save the generated barcode as a PNG file with the specified resolution.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}