// Title: Extract barcode region from in-memory image using Aspose.BarCode
// Description: Demonstrates loading a generated QR code into a memory stream, reading it with BarCodeReader, and retrieving the barcode's placement region without writing the image to disk.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It shows how to generate a barcode, store it in a MemoryStream, and use BarCodeReader to decode and obtain the barcode's geometric region (rectangle and angle). Developers working with in‑memory barcode images, such as for web services or automated testing, can use these APIs (BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType) to avoid filesystem I/O.
// Prompt: Load image data from a memory stream and extract barcode placement region without saving to disk.
// Tags: barcode, qr, region, memorystream, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code, reading it from a memory stream,
/// and extracting its placement region using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it from memory, and prints barcode details.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate a QR code with the text "Hello World".
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                // Save the barcode image directly into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Initialize the barcode reader to decode any supported barcode type from the stream.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes found in the image.
                var results = reader.ReadBarCodes();

                // Iterate through each detected barcode and output its details.
                foreach (var result in results)
                {
                    // Retrieve the rectangle that defines the barcode's region.
                    var rect = result.Region.Rectangle;

                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Rectangle: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                    Console.WriteLine($"Angle: {result.Region.Angle}");
                }
            }
        }
    }
}