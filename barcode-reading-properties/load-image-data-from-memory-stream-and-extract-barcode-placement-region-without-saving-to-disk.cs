// Title: Load barcode from memory stream and retrieve placement region
// Description: Demonstrates generating a barcode in memory, reading it directly from a stream, and extracting the barcode's location without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the Region property to obtain placement coordinates. Typical scenarios include on‑the‑fly barcode generation and detection in web services or automated workflows where file I/O is avoided. Developers often need to generate, stream, and analyze barcodes without persisting intermediate images.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: barcode, code128, memory stream, region, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading barcode image data from a memory stream and extracting its placement region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it from memory, and prints detection details.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "Sample123" and save it as PNG into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Initialize a barcode reader that works directly on the memory stream and supports all barcode types.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the rectangle that defines the barcode's placement region.
                    var rect = result.Region.Rectangle;

                    // Output detection details to the console.
                    Console.WriteLine($"Detected barcode type: {result.CodeType}");
                    Console.WriteLine($"Code text: {result.CodeText}");
                    Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                }
            }
        }
    }
}