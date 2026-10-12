// Title: PDF417 barcode with top caption
// Description: Demonstrates how to generate a PDF417 barcode image with a visible caption placed above the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters to customize barcode appearance. Typical use cases include adding descriptive text above barcodes for printing labels or documents. Developers often need to control caption visibility, text, and styling when creating barcodes programmatically.
// Prompt: Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true.
// Tags: pdf417, barcode, caption, top caption, image generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a PDF417 barcode with a top caption and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, configures the barcode generator,
    /// sets caption properties, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "Pdf417_TopCaption.png");

        // Initialize the barcode generator with PDF417 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCodeText"))
        {
            // Set PDF417-specific parameters: number of rows and X-dimension in pixels
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable the caption above the barcode and configure its text and font size
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}