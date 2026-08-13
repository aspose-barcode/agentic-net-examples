// Title: Generate QR Code and write PNG to HTTP response stream (ASP.NET Core)
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and writing the PNG image directly to a stream that can be used as an ASP.NET Core HttpResponse.Body.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes.QR and BarCodeImageFormat to produce QR Code images. Typical scenarios include generating barcodes on‑the‑fly for web APIs, embedding them in HTTP responses, or saving them to files. Developers working with ASP.NET Core often need to stream barcode images directly to the client without intermediate files.
// Prompt: Generate QR Code barcode and write image directly to HTTP response stream in ASP.NET Core.
// Tags: qr code, barcode generation, aspnet core, http response, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console entry point that demonstrates how to generate a QR Code barcode
/// and write the resulting PNG image directly to a stream suitable for an ASP.NET Core
/// HttpResponse.Body.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Simulates an HTTP response by using a <see cref="MemoryStream"/>
    /// and writes a QR Code image to it. The generated image is also saved to a file for
    /// demonstration purposes.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response body using an in‑memory stream.
        using (var responseStream = new MemoryStream())
        {
            // Generate a QR Code barcode and write the PNG image to the simulated response stream.
            WriteQrCodeToStream("Hello Aspose QR!", responseStream);

            // For demonstration, persist the generated image to a file.
            // In a real ASP.NET Core application, the responseStream would be the HttpResponse.Body.
            File.WriteAllBytes("qr.png", responseStream.ToArray());

            Console.WriteLine($"QR code image written to response stream. Bytes: {responseStream.Length}");
        }
    }

    /// <summary>
    /// Generates a QR Code barcode with the specified text and writes the PNG image
    /// directly to the provided stream (e.g., HttpResponse.Body in ASP.NET Core).
    /// </summary>
    /// <param name="codeText">The text to encode in the QR code.</param>
    /// <param name="outputStream">The stream to which the PNG image will be written.</param>
    static void WriteQrCodeToStream(string codeText, Stream outputStream)
    {
        // Ensure the output stream is positioned at the beginning before writing.
        if (outputStream.CanSeek)
        {
            outputStream.Seek(0, SeekOrigin.Begin);
        }

        // Create and configure the QR code generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set error correction level to high for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode image directly to the provided stream in PNG format.
            generator.Save(outputStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so that a consumer can read from the beginning.
        if (outputStream.CanSeek)
        {
            outputStream.Seek(0, SeekOrigin.Begin);
        }
    }
}