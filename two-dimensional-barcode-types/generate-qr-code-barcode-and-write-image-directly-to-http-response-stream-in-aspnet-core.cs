// Title: Generate QR Code barcode and output as Base64 (ASP.NET Core example)
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image in a memory stream, and converting the image to a Base64 string for display.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce QR Code barcodes. Typical use cases include generating QR codes for URLs, product information, or authentication tokens and delivering them as images in web applications. Developers often need to render barcodes directly to HTTP response streams or encode them for transport, using classes like BarcodeGenerator, BarCodeImageFormat, and memory streams.
// Prompt: Generate QR Code barcode and write image directly to HTTP response stream in ASP.NET Core.
// Tags: qr code, generation, png, aspnet core, http response, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves it to a memory stream as PNG,
/// and outputs the image as a Base64 string. In an ASP.NET Core environment, the memory
/// stream would be written directly to the HttpResponse.Body.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with QR symbology and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set the QR error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for reading.
                ms.Position = 0;

                // Convert the PNG image bytes to a Base64 string for console output.
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("QR Code image (Base64 PNG):");
                Console.WriteLine(base64);
            }
        }
    }
}