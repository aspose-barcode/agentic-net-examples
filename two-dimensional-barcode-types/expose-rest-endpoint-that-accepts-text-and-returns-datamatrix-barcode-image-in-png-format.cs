// Title: Generate DataMatrix barcode and output as Base64 PNG (demo)
// Description: Demonstrates creating a DataMatrix barcode from input text using Aspose.BarCode and saving it as a PNG image. The example shows how to retrieve the image bytes for use in a REST response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DataMatrix. Typical scenarios include generating machine‑readable DataMatrix images for inventory, shipping, or authentication purposes. Developers often need to configure parameters such as XDimension and export the result in common image formats like PNG for web APIs.
// Prompt: Expose a REST endpoint that accepts text and returns a DataMatrix barcode image in PNG format.
// Tags: datamatrix, barcode generation, png, aspose.barcode, rest endpoint

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application that generates a DataMatrix barcode image and outputs it as a Base64 string.
/// In a real-world scenario this logic would be part of a REST endpoint returning the PNG bytes directly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional command‑line text, generates a DataMatrix PNG, and writes the image as Base64 to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the barcode text.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode: use the first argument if provided, otherwise fall back to a default.
        string text = args.Length > 0 ? args[0] : "Sample DataMatrix";

        // Define a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Create a BarcodeGenerator for DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Optional: set the module (pixel) size of the DataMatrix.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the PNG file into a byte array.
        byte[] imageBytes = File.ReadAllBytes(outputPath);

        // Convert the image bytes to a Base64 string for easy transport (e.g., JSON response).
        string base64 = Convert.ToBase64String(imageBytes);

        // Output the Base64 string to the console (simulating a REST response payload).
        Console.WriteLine("DataMatrix PNG Base64:");
        Console.WriteLine(base64);
    }
}