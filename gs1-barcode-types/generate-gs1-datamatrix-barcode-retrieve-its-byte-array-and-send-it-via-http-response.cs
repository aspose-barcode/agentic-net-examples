// Title: Generate GS1 DataMatrix barcode and output as Base64 string
// Description: Demonstrates creating a GS1 DataMatrix barcode with Aspose.BarCode, retrieving its PNG byte array, and showing how the data could be sent in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes.GS1DataMatrix to produce a GS1‑compliant DataMatrix symbol. Typical scenarios include encoding product identifiers for supply‑chain applications, embedding the image in web pages, or returning the image bytes from a web API. Developers often need to adjust visual parameters (e.g., X‑dimension) and obtain the raw image bytes for further processing or transmission.
/// Prompt: Generate a GS1 DataMatrix barcode, retrieve its byte array, and send it via HTTP response.
/// Tags: gs1, datamatrix, barcode, generation, png, bytearray, http response, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a GS1 DataMatrix barcode, extracts its PNG byte array,
/// and writes a Base64 representation to the console (simulating an HTTP response payload).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, captures the image bytes,
    /// and outputs the Base64 string.
    /// </summary>
    static void Main()
    {
        // Define the GS1 data to encode. The parentheses indicate Application Identifiers.
        string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Optional: set the module (pixel) size for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Retrieve the raw PNG bytes from the memory stream.
                byte[] barcodeBytes = ms.ToArray();

                // In a real HTTP server you would write 'barcodeBytes' directly to the response stream.
                // For demonstration, output the Base64-encoded image to the console.
                Console.WriteLine(Convert.ToBase64String(barcodeBytes));
            }
        }
    }
}