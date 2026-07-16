// Title: DataMatrix Barcode with Custom Margin
// Description: Demonstrates how to set a margin (padding) around a DataMatrix barcode to improve scanning reliability.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator and BarCodeReader classes. It shows how to configure barcode parameters such as padding, version selection, and image saving, which are common tasks for developers creating and validating barcodes in .NET applications.
// Prompt: Provide configuration option to set DataMatrix margin size around the symbol for better scanning.
// Tags: datamatrix, margin, padding, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode with a configurable margin and then reads it back to verify decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode, applies padding, saves it as PNG,
    /// and uses BarCodeReader to confirm the barcode can be decoded.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "datamatrix_margin.png";

        // Create a DataMatrix barcode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // Set a 10‑point margin (padding) on all sides of the symbol.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: specify a particular DataMatrix version (e.g., ECC200_20x20).
            // generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Read the generated barcode to demonstrate successful scanning.
        using (var reader = new BarCodeReader(outputPath, DecodeType.DataMatrix))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}