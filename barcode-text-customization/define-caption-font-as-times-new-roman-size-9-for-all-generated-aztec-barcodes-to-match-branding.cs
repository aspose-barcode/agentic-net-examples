// Title: Set caption font for Aztec barcode using Aspose.BarCode
// Description: Demonstrates how to define a Times New Roman, size 9 caption font for an Aztec barcode and save it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode captions using the BarcodeGenerator class. It shows setting caption text, font properties, and alignment—common tasks when branding barcodes for print or digital media. Developers often need to adjust caption appearance to match corporate style guidelines.
// Prompt: Define caption font as Times New Roman, size 9, for all generated Aztec barcodes to match branding.
// Tags: aztec, barcode, caption, font, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates an Aztec barcode with a caption styled in Times New Roman, size 9,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures the caption,
    /// and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "aztec_caption.png";

        // Initialize the barcode generator for Aztec symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "Sample123"))
        {
            // Set optional caption text that will appear above the barcode.
            generator.Parameters.CaptionAbove.Text = "Aztec Barcode";

            // Apply the required font settings: Times New Roman, 9‑point size.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 9f;

            // Align the caption to the center of the barcode area.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Render and save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Aztec barcode saved to {outputPath}");
    }
}