// Title: Generate QR Code Bitmap at 300 DPI and Write to MemoryStream
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to produce a QR code image at 300 dpi resolution and store the resulting PNG bitmap in a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters, and the Aspose.Drawing bitmap handling to create high‑resolution barcode images. Typical scenarios include generating QR codes for web links, embedding them in documents, or transmitting them over APIs where a memory stream is required. Developers often need to adjust resolution, select symbology, and serialize the image without writing to disk.
// Prompt: Set BarcodeGenerator resolution to 300 dpi, generate QR code, and write bitmap to memory stream.
// Tags: qr code, barcode generation, resolution, memory stream, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code bitmap at 300 dpi and writing it to a memory stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, sets resolution, generates a bitmap, and saves it to a memory stream.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR code with the target text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a memory stream to hold the PNG data
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap to the stream in PNG format
                    bitmap.Save(memoryStream, ImageFormat.Png);

                    // Output the size of the generated image (optional diagnostic)
                    Console.WriteLine($"Generated QR code image size: {memoryStream.Length} bytes");
                }
            }
        }
    }
}