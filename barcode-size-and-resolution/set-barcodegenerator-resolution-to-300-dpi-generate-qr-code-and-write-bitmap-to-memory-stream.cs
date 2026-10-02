// Title: Generate QR Code Bitmap at 300 DPI and Save to Memory Stream
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to produce a QR code image at 300 dpi, render it as a bitmap, and write the PNG data to a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and drawing classes to create high‑resolution barcodes. Typical scenarios include generating QR codes for printing, embedding in PDFs, or transmitting over networks. Developers often need to control resolution, output format, and stream handling when integrating barcode images into applications.
// Prompt: Set BarcodeGenerator resolution to 300 dpi, generate QR code, and write bitmap to memory stream.
// Tags: qr code, barcode generation, resolution, bitmap, memory stream, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR code bitmap at 300 dpi and writes it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, sets high resolution, and saves the image as PNG into a MemoryStream.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure the output resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Generate the barcode image as a bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG data
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap into the stream using PNG format
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Output the size of the generated image for verification
                    Console.WriteLine($"Generated QR code bitmap size: {memoryStream.Length} bytes");
                }
            }
        }
    }
}