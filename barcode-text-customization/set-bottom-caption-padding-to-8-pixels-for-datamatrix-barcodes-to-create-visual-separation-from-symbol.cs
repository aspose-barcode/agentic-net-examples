// Title: Set bottom caption padding for DataMatrix barcode
// Description: Demonstrates how to add a bottom caption to a DataMatrix barcode and set its padding to 8 pixels, creating visual separation between the caption and the symbol.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters properties to customize caption visibility, text, and padding. Typical use cases include adding descriptive text below barcodes for printing labels or reports, where developers need to control spacing for readability. The example shows how to work with EncodeTypes, BarCodeImageFormat, and caption padding settings.
// Prompt: Set bottom caption padding to 8 pixels for DataMatrix barcodes to create visual separation from the symbol.
// Tags: datamatrix, caption, padding, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting bottom caption padding for a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a DataMatrix barcode with a bottom caption and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "DataMatrixWithBottomCaption.png");

        // Create a barcode generator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Enable the caption below the barcode and set its text
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";

            // Set bottom padding of the caption to 8 pixels for visual separation
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8f;

            // Save the barcode image as PNG
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}