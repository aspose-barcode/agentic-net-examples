// Title: Set bottom caption padding for DataMatrix barcode
// Description: Demonstrates how to add a bottom caption to a DataMatrix barcode and set its padding to 8 pixels, creating visual separation from the symbol.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on customizing caption appearance. It showcases the use of BarcodeGenerator, EncodeTypes, and the Parameters.CaptionBelow API to control caption visibility, text, and padding. Developers often need to adjust caption layout for better readability in printed or displayed barcodes.
// Prompt: Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation from the symbol.
// Tags: datamatrix, caption, padding, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with a bottom caption and custom padding,
/// then saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode,
    /// applies caption settings, and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrix_WithBottomCaption.png");

        // Initialize the barcode generator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "123456"))
        {
            // Enable the bottom caption and assign its display text
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";

            // Apply an 8‑pixel bottom padding to separate the caption from the symbol
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8;

            // Render and save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}