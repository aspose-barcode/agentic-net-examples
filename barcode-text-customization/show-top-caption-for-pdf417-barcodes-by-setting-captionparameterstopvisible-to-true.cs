// Title: PDF417 Barcode with Top Caption
// Description: Generates a PDF417 barcode image with a visible caption placed above the barcode.
// Category-Description: This example demonstrates Aspose.BarCode generation features, focusing on caption configuration for 2D barcodes. It uses the BarcodeGenerator class to set barcode parameters, including rows, X-dimension, and caption properties. Developers often need to add readable text above or below barcodes for labeling or instructional purposes, and this snippet shows the typical API usage for such scenarios.
// Prompt: Show top caption for PDF417 barcodes by setting CaptionParameters.Top.Visible to true.
// Tags: pdf417, barcode, caption, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a PDF417 barcode with a top caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, configures the barcode generator,
    /// adds a visible top caption, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "Pdf417_TopCaption.png");

        // Initialize the barcode generator for PDF417 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample CodeText"))
        {
            // Set the number of rows for the PDF417 barcode.
            generator.Parameters.Barcode.Pdf417.Rows = 12;

            // Define the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable and configure the caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}