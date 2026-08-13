// Title: Generate DataMatrix barcode with top caption, padding, and centered alignment
// Description: Demonstrates creating a DataMatrix barcode, adding a top caption with 10‑pixel padding, and centering the text. The barcode is saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and caption parameters to customize barcode appearance. Typical use cases include adding descriptive text above barcodes for labeling, packaging, or inventory systems. Developers often need to adjust caption styling, padding, and alignment to meet design requirements.
// Prompt: Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and centered alignment.
// Tags: datamatrix, caption, padding, alignment, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DataMatrix barcode with a top caption,
/// applies 10‑pixel top padding, centers the caption, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "datamatrix.png";

        // Initialize a BarcodeGenerator for the DataMatrix symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // ----- Configure the top caption (CaptionAbove) -----
            generator.Parameters.CaptionAbove.Text = "Top Caption";                     // Caption text
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";           // Font family
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;                    // Font size in points
            generator.Parameters.CaptionAbove.TextColor = Aspose.Drawing.Color.Black; // Text color
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;        // Center alignment

            // Apply a 10‑pixel top padding to the caption to create visual separation.
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10f;

            // Save the configured barcode as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}