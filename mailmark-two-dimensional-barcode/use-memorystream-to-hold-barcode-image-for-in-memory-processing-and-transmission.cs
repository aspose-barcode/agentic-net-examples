// Title: Generate and Decode a Code128 Barcode Using MemoryStream
// Description: Demonstrates creating a Code128 barcode, saving it to a MemoryStream, and decoding it directly from the stream without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes and BarCodeReader to read them, both operating on in‑memory streams. Developers often need to generate barcodes for web responses, email attachments, or other transient scenarios where persisting to the file system is undesirable.
// Prompt: Use a MemoryStream to hold the barcode image for in‑memory processing and transmission.
// Tags: code128, barcode generation, barcode recognition, memorystream, png, aspose.barcode, in‑memory processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in a MemoryStream,
/// and decoding it directly from the stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it to a MemoryStream,
    /// then reads and decodes the barcode from the same stream.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the barcode text.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode: use first argument if provided, otherwise default.
        string codeText = args.Length > 0 ? args[0] : "12345678";

        // Choose the barcode symbology (Code128) for encoding.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a BarcodeGenerator with the selected type and text.
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Prepare an in‑memory stream to hold the generated image.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the barcode image as PNG into the MemoryStream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated and saved to MemoryStream. Size: {memoryStream.Length} bytes.");

                // Reset the stream position to the beginning before reading.
                memoryStream.Position = 0;

                // Initialize a BarCodeReader to decode the barcode from the stream.
                using (BarCodeReader reader = new BarCodeReader(memoryStream, DecodeType.Code128))
                {
                    // Iterate through all detected barcodes (should be one in this case).
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded barcode type: {result.CodeTypeName}, text: {result.CodeText}");
                    }
                }
            }
        }
    }
}