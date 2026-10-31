// Title: Generate Code128 barcode with 20% width reduction
// Description: Demonstrates how to reduce the bar width of a Code128 barcode by 20 percent using Aspose.BarCode, useful for fitting barcodes onto narrow label spaces.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as XDimension and BarWidthReduction. Developers working with label printing, inventory tracking, or any scenario requiring compact barcodes can use these APIs (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) to customize size and appearance before saving to an image file. Typical use cases include narrow product labels, RFID tag printing, and space‑constrained packaging.
// Prompt: Set barcode width reduction to 20 percent to fit narrow label spaces.
// Tags: code128, width reduction, barcode generation, png, aspose.barcode, barcodereduction, label printing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode, applies a 20 percent bar width reduction,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "BarWidthReduction20.png");

        // Initialize the barcode generator with Code128 symbology and the data "NARROW".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "NARROW"))
        {
            // Set the module (X) dimension to 10 pixels. This defines the base bar width.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Reduce the bar width by 20 percent (2 pixels of the 10‑pixel XDimension).
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 2f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}