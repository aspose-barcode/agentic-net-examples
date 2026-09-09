// Title: Generate GS1 DataMatrix barcode and return as PNG byte array
// Description: Demonstrates creating a GS1 DataMatrix barcode using Aspose.BarCode, extracting the PNG image as a byte array, and preparing it for an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters, render the symbol to a memory stream, and embed the resulting image in an HttpResponseMessage. Developers working with barcode imaging often need to generate barcodes on the fly and send them over HTTP, using classes such as BarcodeGenerator, BarCodeImageFormat, and HttpResponseMessage.
// Prompt: Generate a GS1 DataMatrix barcode, retrieve its byte array, and send it via HTTP response.
// Tags: gs1datamatrix, barcode generation, png, byte array, http response, aspose.barcode

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a GS1 DataMatrix barcode, converts it to a PNG byte array, and wraps it in an HttpResponseMessage.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix content (including Application Identifier 01 for GTIN).
        string codeText = "(01)12345678901231";
        byte[] imageBytes;

        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Adjust the X-dimension (module size) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream contents to a byte array.
                imageBytes = ms.ToArray();
            }
        }

        // Create an HTTP response message containing the barcode image.
        using (var response = new HttpResponseMessage())
        {
            response.Content = new ByteArrayContent(imageBytes);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            Console.WriteLine($"Generated GS1 DataMatrix barcode, byte size: {imageBytes.Length}");
            // Simulated HTTP response containing the barcode image.
        }
    }
}