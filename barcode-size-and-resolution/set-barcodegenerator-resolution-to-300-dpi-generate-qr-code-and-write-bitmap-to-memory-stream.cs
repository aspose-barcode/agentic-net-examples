// Title: Generate QR code bitmap with 300 dpi resolution and write to memory stream
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to produce a QR code at 300 dpi, render it as a bitmap, and store the image in a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters, and the GenerateBarCodeImage method to create high‑resolution barcode images. Typical scenarios include generating QR codes for web links, product information, or authentication tokens, where developers need to control image quality and output to streams for further processing or transmission.
// Prompt: Set BarcodeGenerator resolution to 300 dpi, generate QR code, and write bitmap to memory stream.
// Tags: qr code, resolution, bitmap, memory stream, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code bitmap at 300 dpi and writes it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures the barcode generator, creates a QR code bitmap, and saves it to a memory stream.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text (a sample URL)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the image resolution to 300 dpi for higher quality output
            generator.Parameters.Resolution = 300f;

            // Generate the barcode as a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a memory stream to hold the PNG-encoded bitmap
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap into the stream using PNG format
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Output the size of the generated image stream for verification
                    Console.WriteLine($"QR code image generated, stream length: {memoryStream.Length} bytes");
                }
            }
        }
    }
}