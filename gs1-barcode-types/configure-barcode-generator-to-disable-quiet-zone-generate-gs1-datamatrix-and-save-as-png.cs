// Title: Generate a GS1 DataMatrix barcode without quiet zone and save as PNG
// Description: Demonstrates how to configure Aspose.BarCode to disable the quiet zone, generate a GS1 DataMatrix barcode, and export it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode parameters such as module size and padding. Developers often need to produce compact barcodes for packaging or labeling where quiet zones are undesirable, and this snippet provides a concise reference.
// Prompt: Configure the barcode generator to disable the quiet zone, generate a GS1 DataMatrix, and save as PNG.
// Tags: datamatrix, gs1, quietzone, png, aspose.barcode, generation, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode without a quiet zone and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode generator, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload (GTIN with Application Identifier 01)
        string codeText = "(01)12345678901231";

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.png");

        // Initialize the barcode generator for GS1 DataMatrix symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Optional: set the module (X) dimension to control barcode size
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Disable the quiet zone by setting all padding values to zero
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}