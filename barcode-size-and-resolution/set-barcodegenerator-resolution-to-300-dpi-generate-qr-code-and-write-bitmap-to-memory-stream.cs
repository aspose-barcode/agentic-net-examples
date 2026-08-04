// Title: Generate QR Code with 300 DPI Resolution and Save to Memory Stream
// Description: Demonstrates setting the BarcodeGenerator resolution to 300 dpi, creating a QR code, and writing the resulting bitmap to a memory stream in PNG format.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution, encode data using QR symbology, and output the barcode as a bitmap via the BarcodeGenerator and Bitmap classes. Developers commonly use these APIs to produce high‑resolution barcodes for printing, digital display, or further image processing in .NET applications.
// Prompt: Set BarcodeGenerator resolution to 300 dpi, generate QR code, and write bitmap to memory stream.
// Tags: qr code, resolution, bitmap, memory stream, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR code at 300 dpi and writes the image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures the barcode generator, creates a QR code, and saves it as PNG in a memory stream.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the output image resolution to 300 dots per inch
            generator.Parameters.Resolution = 300f;

            // Define the data to encode in the QR code
            generator.CodeText = "Hello World";

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG-encoded image
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap into the stream using PNG format
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Output the size of the generated image (for demonstration purposes)
                    Console.WriteLine($"QR code image generated. Stream length: {memoryStream.Length} bytes");
                }
            }
        }
    }
}