using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 DataMatrix barcode with a sample GTIN and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the barcode content: AI (01) followed by a 14‑digit GTIN.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Note: Aspose.BarCode currently lacks a direct property to disable the quiet zone for DataMatrix.
            // If a quiet zone property becomes available in the future, it can be configured here.
            // No quiet zone modification is performed at this time.

            // Determine the full path for the output PNG file in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1datamatrix.png");

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
        }
    }
}