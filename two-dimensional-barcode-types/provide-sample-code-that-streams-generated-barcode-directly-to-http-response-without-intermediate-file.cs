// Title: Stream generated barcode directly to HTTP response
// Description: Demonstrates how to generate a barcode with Aspose.BarCode and write it straight to an HTTP response stream without creating a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images on the fly. Typical scenarios include web applications that need to return barcode images as HTTP responses or API endpoints. Developers often need to stream images directly to the response to improve performance and reduce I/O overhead.
// Prompt: Provide sample code that streams generated barcode directly to HTTP response without intermediate file.
// Tags: barcode, generation, streaming, http response, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates streaming a generated barcode image directly to an HTTP response stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, writes it to a memory stream simulating an HTTP response,
    /// and outputs the image size and Base64 string for demonstration purposes.
    /// </summary>
    static void Main()
    {
        // Define the data to encode and the barcode symbology
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Simulate an HTTP response using a memory stream
        using (MemoryStream responseStream = new MemoryStream())
        {
            // Generate the barcode and save it directly into the response stream as PNG
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(responseStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so that it can be read from the beginning
            responseStream.Position = 0;

            // Example output: display the size of the generated image and its Base64 representation
            Console.WriteLine($"Generated barcode size: {responseStream.Length} bytes");
            string base64 = Convert.ToBase64String(responseStream.ToArray());
            Console.WriteLine(base64);
        }
    }
}