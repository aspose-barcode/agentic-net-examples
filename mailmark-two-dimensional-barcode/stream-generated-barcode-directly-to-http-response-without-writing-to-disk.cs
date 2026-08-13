// Title: Stream Barcode Directly to HTTP Response
// Description: Demonstrates generating a Code128 barcode and streaming it as a PNG image directly to an HTTP response without creating a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It shows how to use the BarcodeGenerator class together with BarCodeImageFormat to produce barcode images on‑the‑fly. Typical use cases include web applications that need to return barcode images to browsers or APIs, where writing to disk is undesirable. Developers often need to write the image to a response stream, set colors, and choose output formats.
// Prompt: Stream the generated barcode directly to an HTTP response without writing to disk.
// Tags: barcode, code128, generation, stream, png, aspnet, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates streaming a generated barcode image directly to an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates an HTTP response stream and outputs diagnostic information.
    /// </summary>
    static void Main()
    {
        // Simulate an HTTP response stream.
        // In a real web application, replace this MemoryStream with HttpResponse.OutputStream.
        using (var responseStream = new MemoryStream())
        {
            // Generate the barcode and write it directly to the response stream.
            GenerateBarcodeToStream(responseStream);

            // Show the size of the generated image for verification.
            Console.WriteLine($"Generated barcode image size: {responseStream.Length} bytes");

            // Reset the stream position to the beginning to read its contents.
            responseStream.Position = 0;

            // Optional: display the image as a Base64 string (useful for testing or embedding in HTML).
            string base64 = Convert.ToBase64String(responseStream.ToArray());
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }
    }

    /// <summary>
    /// Generates a Code128 barcode and saves it as a PNG image to the specified output stream.
    /// </summary>
    /// <param name="outputStream">The stream to which the barcode image will be written.</param>
    static void GenerateBarcodeToStream(Stream outputStream)
    {
        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure visual appearance (optional).
            generator.Parameters.Barcode.BarColor = Color.Black;   // Foreground color.
            generator.Parameters.BackColor = Color.White;         // Background color.

            // Save the generated barcode directly to the provided stream in PNG format.
            generator.Save(outputStream, BarCodeImageFormat.Png);
        }
    }
}