// Title: Generate QR Code and stream as PNG image
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and writing the PNG image to a stream, which can be sent directly in an ASP.NET Core response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce QR Code images. Typical scenarios include generating barcodes on-the-fly for web APIs, embedding them in HTTP responses, or saving them to files. Developers working with dynamic barcode creation in ASP.NET Core often need to render images directly to response streams without intermediate files.
// Prompt: Generate QR Code barcode and write image directly to HTTP response stream in ASP.NET Core.
// Tags: qr code, barcode generation, aspnet core, image output, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and writes it to a stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, saves it to a memory stream,
    /// and writes the image size to the console (simulating an HTTP response).
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "https://example.com";

        // Initialize the QR code generator with the desired symbology and text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set the error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // In an ASP.NET Core controller you would write the stream to HttpResponse.Body.
                // For this console demonstration, output the size of the generated image.
                Console.WriteLine($"Generated QR code image size: {ms.Length} bytes");
            }
        }
    }
}