// Title: Generate a square DataMatrix barcode with default size
// Description: Creates a DataMatrix barcode in a square shape using default dimensions and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to produce barcodes. Typical use cases include creating barcodes for inventory tracking, shipping labels, and product identification. Developers often need to configure symbology-specific parameters such as version or shape while relying on default sizing for quick generation.
// Prompt: Generate a DataMatrix barcode with square shape and default size for given alphanumeric CodeText.
// Tags: datamatrix, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a square DataMatrix barcode with default size and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts an optional command‑line argument for the barcode text,
    /// generates a square DataMatrix barcode, and writes the image to the current directory.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the barcode text if provided.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode; use the first argument if supplied, otherwise default to "ABC123".
        string codeText = args.Length > 0 ? args[0] : "ABC123";

        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrix.png");

        // Create a BarcodeGenerator for the DataMatrix symbology with the specified code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Ensure a square shape by selecting a square version (32x32 modules).
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            // No explicit size settings are applied; the generator uses its default size.

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}