// Title: PDF417 Barcode with Top Caption
// Description: Demonstrates how to generate a PDF417 barcode and display a caption above it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters to customize barcode appearance. Developers often need to add human‑readable text (captions) above or below barcodes for labeling, documentation, or UI purposes. The snippet shows typical steps: creating a generator, configuring caption visibility, text, and font, then saving the image.
// Prompt: Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true.
// Tags: pdf417, barcode, caption, generation, image, aspose.barcode, csharp

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with a visible top caption and saves it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures the top caption, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a PDF417 barcode generator with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417"))
        {
            // Enable the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text and its font size.
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Define the output file path and save the barcode as a PNG image.
            string outputPath = "pdf417_with_caption.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}