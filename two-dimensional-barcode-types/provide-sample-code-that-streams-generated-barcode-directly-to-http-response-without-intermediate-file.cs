// Title: Stream barcode image directly to HTTP response without intermediate file
// Description: Demonstrates generating a barcode in memory and converting it to a Base64 string that can be sent as an HTTP response body.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator, set barcode parameters, and save the image to a stream. Developers often need to embed barcodes in web pages or APIs without writing temporary files, and this pattern shows the typical workflow for such scenarios.
// Prompt: Provide sample code that streams generated barcode directly to HTTP response without intermediate file.
// Tags: barcode, code128, streaming, http response, memory stream, base64, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode in memory and outputting it as a Base64 string suitable for HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes it to a memory stream, converts to Base64, and writes to console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Optional: customize barcode appearance.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Use a MemoryStream to hold the generated image in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image directly to the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading.
                memoryStream.Position = 0;

                // Convert the image bytes to a Base64 string.
                string base64Image = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string (simulating an HTTP response body).
                Console.WriteLine(base64Image);
            }
        }
    }
}