// Title: Generate DataMatrix barcode and save as PNG (console demo)
// Description: Demonstrates creating a DataMatrix barcode from input text, saving it as a PNG file, and optionally returning the image as a Base64 string for HTTP responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix. Typical use cases include generating machine‑readable codes for inventory, tracking, or authentication. Developers often need to configure encoding, choose an image format, and embed the result in web APIs or UI components.
// Prompt: Expose a REST endpoint that accepts text and returns a DataMatrix barcode image in PNG format.
// Tags: datamatrix, barcode, generation, png, aspose.barcode, eciencoding, base64, console

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that generates a DataMatrix barcode image from supplied text.
/// The core logic can be reused inside a REST endpoint to return the PNG image directly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode, saves it as PNG, and writes the file path and Base64 representation to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the barcode text.</param>
    static void Main(string[] args)
    {
        // Input text: use the first command‑line argument if provided, otherwise a default value.
        string inputText = args.Length > 0 ? args[0] : "Sample123";

        // Determine a temporary file path for the PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Create and configure the DataMatrix barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, inputText))
        {
            // Use UTF‑8 ECI encoding to support Unicode characters.
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode directly as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");

        // Optionally, output the image as a Base64 string (useful for HTTP responses).
        byte[] imageBytes = File.ReadAllBytes(outputPath);
        string base64 = Convert.ToBase64String(imageBytes);
        Console.WriteLine("Base64 PNG:");
        Console.WriteLine(base64);
    }
}