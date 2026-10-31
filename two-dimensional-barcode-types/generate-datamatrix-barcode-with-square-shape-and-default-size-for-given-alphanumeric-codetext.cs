// Title: Generate a square DataMatrix barcode with default size
// Description: Creates a DataMatrix barcode using Aspose.BarCode with a square shape and default dimensions, saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to produce barcodes. Typical use cases include encoding alphanumeric data for inventory, tracking, or authentication purposes. Developers often need to quickly generate barcodes in common image formats without custom sizing or shape adjustments.
// Prompt: Generate a DataMatrix barcode with square shape and default size for given alphanumeric CodeText.
// Tags: datamatrix, barcode, generation, png, aspose.barcode, encode, defaultsize, square

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a square DataMatrix barcode with default size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode from the provided command‑line argument or a default string and saves it as PNG.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is used as the barcode text.</param>
    static void Main(string[] args)
    {
        // Determine the barcode text: use the first argument if supplied, otherwise fall back to a default alphanumeric value.
        string codeText = args.Length > 0 ? args[0] : "ABC123";

        // Build the output directory path (relative to the current working directory) and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "DataMatrix.png");

        // Create a BarcodeGenerator for DataMatrix with default (square) shape and size, then save the image.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}