// Title: Generate DataMatrix Barcode PNG via REST-like Endpoint
// Description: Demonstrates generating a DataMatrix barcode image in PNG format from input text, simulating a REST endpoint.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to create barcode images. Typical use cases include creating printable labels, embedding barcodes in documents, or serving barcode images through web APIs. Developers often need to configure symbol properties, select output formats, and integrate the generation logic into REST services.
// Prompt: Expose a REST endpoint that accepts text and returns a DataMatrix barcode image in PNG format.
// Tags: datamatrix, barcode, generation, png, aspnet, rest, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simulates a REST endpoint that generates a DataMatrix barcode image in PNG format from supplied text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads input text from command‑line arguments,
    /// creates a DataMatrix barcode, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments where the first element is the text to encode.</param>
    static void Main(string[] args)
    {
        // In a real application this would be a REST endpoint.
        // Here we simulate the endpoint by reading the text from command‑line arguments
        // and generating a DataMatrix PNG image.

        // Use the first argument as the barcode text; fall back to a default value if none provided.
        string codeText = args.Length > 0 ? args[0] : "Sample Text";

        // Create a DataMatrix barcode generator with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure a square DataMatrix symbol (aspect ratio = 1) and a specific size.
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Define the output file path.
            string outputPath = "datamatrix.png";

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved.
            Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
        }
    }
}