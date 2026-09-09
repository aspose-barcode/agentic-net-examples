// Title: Generate and read a Code128 barcode using an in‑memory stream
// Description: Demonstrates creating a Code128 barcode, storing it in a MemoryStream, and decoding it without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and MemoryStream for in‑memory handling. Developers often need to generate barcodes on the fly and process them directly in memory for web services, APIs, or messaging systems.
// Prompt: Use a MemoryStream to hold the barcode image for in‑memory processing and transmission.
// Tags: barcode, code128, memorystream, generation, recognition, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode with an in‑memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it to a MemoryStream, then reads it back.
    /// </summary>
    static void Main(string[] args)
    {
        // Text to encode in the barcode
        string codeText = "12345678";

        // Use a MemoryStream to hold the generated barcode image in memory
        using (MemoryStream ms = new MemoryStream())
        {
            // Generate the barcode and save it as PNG into the memory stream
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            ms.Position = 0;

            // Read and decode the barcode directly from the memory stream
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded type: {result.CodeTypeName}, text: {result.CodeText}");
                }
            }

            // Output the size of the generated barcode image in bytes
            Console.WriteLine($"Barcode image size in bytes: {ms.Length}");
        }
    }
}