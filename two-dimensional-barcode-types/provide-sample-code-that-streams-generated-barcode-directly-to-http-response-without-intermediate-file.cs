// Title: Stream Barcode Directly to HTTP Response
// Description: Demonstrates how to generate a Code128 barcode and write it straight to an HTTP response stream without creating a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating direct streaming of barcode images using BarcodeGenerator and BarCodeImageFormat. Typical use cases include web applications that need to return barcode images on-the-fly, reducing I/O overhead. Developers often need to write barcode data to HttpResponse, MemoryStream, or other output streams efficiently.
// Prompt: Provide sample code that streams generated barcode directly to HTTP response without intermediate file.
// Tags: code128, barcode generation, png, stream, aspose.barcode, aspnet, http response

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
    /// Entry point that simulates an HTTP response using a MemoryStream and writes a barcode to it.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response stream using a MemoryStream.
        using (var responseStream = new MemoryStream())
        {
            // Generate the barcode and write it directly to the simulated response stream.
            WriteBarcodeToResponse(responseStream, "12345678");

            // For demonstration purposes, output the size of the generated barcode data.
            Console.WriteLine($"Generated barcode size: {responseStream.Length} bytes");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode and saves it to the provided stream (e.g., an HTTP response).
    /// </summary>
    /// <param name="responseStream">The output stream where the barcode image will be written.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    static void WriteBarcodeToResponse(Stream responseStream, string codeText)
    {
        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Directly write the barcode image to the provided stream in PNG format.
            generator.Save(responseStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position to the beginning if further reading is required.
        if (responseStream.CanSeek)
        {
            responseStream.Position = 0;
        }
    }
}