// Title: PDF417 Barcode with Top Caption
// Description: Generates a PDF417 barcode image with a visible top caption using Aspose.BarCode.
// Category-Description: This example demonstrates how to create barcodes with Aspose.BarCode, focusing on PDF417 symbology and caption customization. It showcases the use of BarcodeGenerator, EncodeTypes, and the CaptionParameters classes to add textual captions above the barcode. Developers working with barcode generation for documents, labels, or packaging often need to add readable text annotations, and this snippet provides a clear pattern for doing so.
// Prompt: Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true.
// Tags: pdf417, barcode, caption, top-caption, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with a visible top caption and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// enables a top caption, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Define the temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "Pdf417_TopCaption.png");

        // Initialize the barcode generator for PDF417 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCodeText"))
        {
            // Configure PDF417-specific parameters
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable and configure the top caption (CaptionAbove)
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Style = FontStyle.Regular;
            generator.Parameters.CaptionAbove.TextColor = Color.Black;

            // Save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}