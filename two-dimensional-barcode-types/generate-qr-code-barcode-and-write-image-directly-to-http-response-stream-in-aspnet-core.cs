// Title: Generate QR Code and stream as PNG in ASP.NET Core
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it to a memory stream that mimics an HTTP response, and outputting the image data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images, configure QR‑specific parameters such as error correction level, and write the resulting PNG image to a stream (e.g., an ASP.NET Core HTTP response). Developers working with web APIs, dynamic image generation, or mobile scanning scenarios frequently need to generate barcodes on‑the‑fly and return them directly to clients without intermediate files.
// Prompt: Generate QR Code barcode and write image directly to HTTP response stream in ASP.NET Core.
// Tags: qr code, barcode generation, aspnet core, png, memorystream, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode and writes the PNG image to a stream,
/// simulating an HTTP response in an ASP.NET Core application.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it to a memory stream,
    /// and writes diagnostic information to the console.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can override the QR text.</param>
    static void Main(string[] args)
    {
        // Default QR code text; can be overridden by a command‑line argument.
        string codeText = "Hello Aspose QR";
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            codeText = args[0];
        }

        // Initialize the QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR error correction level (optional, LevelM provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to act as the HTTP response body.
            using (var responseStream = new MemoryStream())
            {
                // Save the generated barcode as a PNG image into the stream.
                generator.Save(responseStream, BarCodeImageFormat.Png);
                // Reset the stream position so it can be read from the beginning.
                responseStream.Position = 0;

                // Write diagnostic information about the generated image.
                Console.WriteLine($"QR code image generated. Size: {responseStream.Length} bytes.");

                // For demonstration purposes, output the image as a Base64 string.
                // In a real ASP.NET Core controller you would write the raw bytes to the response.
                string base64 = Convert.ToBase64String(responseStream.ToArray());
                Console.WriteLine("Base64 image data:");
                Console.WriteLine(base64);
            }
        }
    }
}