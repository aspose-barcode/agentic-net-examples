// Title: Generate QR Code and Return as Base64 Image via MemoryStream
// Description: This example creates a QR Code barcode using Aspose.BarCode, saves it to a MemoryStream in PNG format, and converts it to a Base64 string suitable for web responses.
// Category-Description: Demonstrates the Aspose.BarCode barcode generation workflow, focusing on QR Code creation. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to produce barcode images on‑the‑fly for web APIs, mobile apps, or dynamic documents. Typical use cases include generating QR codes for URLs, product information, or authentication tokens and delivering them as image streams or Base64 strings.
// Prompt: Generate a QR Code barcode and write image to memory stream for web response.
// Tags: qr code, barcode generation, memory stream, base64, aspose.barcode, png, web response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode and converting it to a Base64 string for web output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR Code, saves to MemoryStream, outputs Base64 string.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Text to encode in the QR Code
        string codeText = "Hello, World!";

        // Initialize the QR Code generator with the desired symbology and data
        var generator = new BarcodeGenerator(EncodeTypes.QR, codeText);

        // Save the generated barcode image to a memory stream in PNG format
        using (var memoryStream = new MemoryStream())
        {
            generator.Save(memoryStream, BarCodeImageFormat.Png);
            memoryStream.Position = 0; // Reset stream position for reading

            // Convert the image bytes to a Base64 string (simulating a web response payload)
            byte[] imageBytes = memoryStream.ToArray();
            string base64Image = Convert.ToBase64String(imageBytes);

            // Output the Base64 string to the console (replace with actual web response handling as needed)
            Console.WriteLine("QR Code image (Base64):");
            Console.WriteLine(base64Image);
        }
    }
}