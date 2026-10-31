// Title: Export DataMatrix barcode to JPEG memory stream
// Description: Demonstrates generating a DataMatrix barcode and saving it as a JPEG image into a MemoryStream, suitable for inclusion in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to create barcodes. Typical use cases include creating barcode images on-the-fly for web APIs, embedding them in documents, or returning them directly to clients. Developers often need to render barcodes to streams in various image formats (PNG, JPEG, etc.) for HTTP responses or further processing.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, barcode, export, jpeg, memorystream, aspnet, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode and writes it to a JPEG MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode, saves it as JPEG into a MemoryStream,
    /// and writes diagnostic information to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Text to encode in the DataMatrix barcode.
        string codeText = "SampleDataMatrix";

        // Initialize the barcode generator with DataMatrix symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: adjust the size of each module (pixel dimension).
            // generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Create a memory stream to hold the generated JPEG image.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in JPEG format.
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Output the size of the generated image for verification.
                Console.WriteLine($"DataMatrix barcode saved to memory stream. Size: {memoryStream.Length} bytes");

                // In a real HTTP response you would write memoryStream.ToArray() to the response body.
            }
        }
    }
}