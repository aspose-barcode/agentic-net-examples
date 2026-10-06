// Title: Export DataMatrix barcode to JPEG in memory stream
// Description: Demonstrates generating a DataMatrix barcode and saving it as a JPEG image into a MemoryStream, suitable for sending in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DataMatrix and BarCodeImageFormat.Jpeg. Typical use cases include creating barcode images on-the-fly for web APIs, embedding them in HTML or JSON responses, and converting them to Base64 for client-side rendering. Developers often need to generate barcodes in memory without writing to disk, then stream them directly to HTTP responses.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, barcode, generation, jpeg, memorystream, http, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a DataMatrix barcode to a JPEG MemoryStream for HTTP responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode from the provided text (or default) and writes the JPEG image to a MemoryStream.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is used as barcode text.</param>
    static void Main(string[] args)
    {
        // Determine barcode text: use first argument if supplied, otherwise default.
        string codeText = args.Length > 0 ? args[0] : "SampleDataMatrix";

        // Create a BarcodeGenerator for DataMatrix symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Prepare an in‑memory stream to hold the JPEG image.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the generated barcode as JPEG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Output the size of the generated image (useful for debugging).
                Console.WriteLine($"Generated DataMatrix JPEG size: {memoryStream.Length} bytes");

                // Convert the image to Base64 for easy embedding in HTTP responses or JSON payloads.
                string base64 = Convert.ToBase64String(memoryStream.ToArray());
                Console.WriteLine($"Base64: {base64}");
            }
        }
    }
}