// Title: Stream Barcode Image Directly to HTTP Response
// Description: Demonstrates generating a Code128 barcode and streaming it as a PNG image directly to an HTTP response without creating a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode generation and output category, illustrating how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce barcode images on the fly. Typical use cases include web applications that need to return barcode images in HTTP responses without persisting them on disk. Developers often need to write the image bytes to the response stream, set appropriate headers, and avoid intermediate storage for performance and security.
// Prompt: Provide sample code that streams generated barcode directly to HTTP response without intermediate file.
// Tags: barcode, code128, generation, streaming, png, aspose.barcode, http response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates streaming a generated barcode image directly to an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, writes HTTP headers, and streams the PNG image to the output.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode
        string codeText = "12345678";

        // Initialize the barcode generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated image (no file is created)
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                barcodeStream.Position = 0;

                // Write HTTP response headers (simulated here by console output)
                Console.WriteLine("Content-Type: image/png");
                Console.WriteLine("Content-Length: " + barcodeStream.Length);
                Console.WriteLine();

                // Copy the image bytes to the standard output stream (represents HTTP response body)
                Stream stdout = Console.OpenStandardOutput();
                barcodeStream.CopyTo(stdout);
                stdout.Flush();
            }
        }
    }
}