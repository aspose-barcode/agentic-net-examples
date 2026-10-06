// Title: Generate GS1 DataMatrix barcode without quiet zone and save as PNG
// Description: Demonstrates how to configure Aspose.BarCode to generate a GS1 DataMatrix barcode, minimize the quiet zone, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.GS1DataMatrix. It shows how to adjust barcode parameters such as padding (quiet zone) and export the result to common image formats. Developers working with GS1 symbologies, needing custom margins or image output, can refer to this pattern.
// Prompt: Configure the barcode generator to disable the quiet zone, generate a GS1 DataMatrix, and save as PNG.
// Tags: gs1datamatrix, barcode, generation, quiet zone, png, aspose.barcode, padding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with minimized quiet zone and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures padding, and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1DataMatrix.png");

        // GS1 DataMatrix code text (example with GTIN)
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix using the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Reduce the quiet zone by setting all padding sides to zero pixels
            // (Note: GS1 DataMatrix enforces a minimal quiet zone, so it cannot be completely removed)
            generator.Parameters.Barcode.Padding.Left.Pixels = 0f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 0f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 0f;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the PNG file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}