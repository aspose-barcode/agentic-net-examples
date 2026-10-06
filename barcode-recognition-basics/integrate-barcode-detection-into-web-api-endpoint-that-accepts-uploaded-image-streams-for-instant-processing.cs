// Title: Barcode generation and detection using Aspose.BarCode in a simulated web API
// Description: This example generates a Code128 barcode, stores it in a memory stream, and then reads the same stream to detect and decode the barcode.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on BarcodeGenerator, BarCodeReader, and related image handling. Typical use cases include processing uploaded images in web APIs to instantly read barcodes. Developers often need to convert streams to images, detect multiple symbologies, and extract encoded data.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: barcode, code128, generation, detection, aspnet, aspose.barcode, memorystream, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and immediate detection using Aspose.BarCode.
/// In a real scenario this logic would reside in a web API endpoint that processes
/// uploaded image streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, writes it to a memory stream,
    /// and reads the stream to detect and decode the barcode.
    /// </summary>
    static void Main(string[] args)
    {
        // Simulate receiving an uploaded image stream in a web API by generating a barcode into a MemoryStream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Create a memory stream to hold the generated barcode image.
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading.
                barcodeStream.Position = 0;

                // Initialize the barcode reader with the image stream.
                using (var reader = new BarCodeReader(barcodeStream))
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}