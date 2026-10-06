// Title: Generate QR Code and embed as Base64 in JSON
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG image, encoding it to Base64, and placing it inside a JSON payload.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include embedding barcodes in data interchange formats such as JSON or XML for web APIs. Developers often need to convert generated images to Base64 strings for transport or storage, and this snippet shows the standard workflow using MemoryStream and BarCodeImageFormat.
// Prompt: Generate QR Code barcode and embed it into a JSON payload as base64 string.
// Tags: qr code, barcode generation, base64, json, aspose.barcode, image encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation and embedding the image as a Base64 string within a JSON payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, converts it to PNG, encodes to Base64, and outputs JSON.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code.
        string codeText = "Hello, World!";

        // Initialize the barcode generator for QR Code with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Use a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the QR Code image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Embed the Base64 string into a simple JSON payload.
                string json = $"{{\"barcode\":\"{base64}\"}}";

                // Output the JSON to the console.
                Console.WriteLine(json);
            }
        }
    }
}