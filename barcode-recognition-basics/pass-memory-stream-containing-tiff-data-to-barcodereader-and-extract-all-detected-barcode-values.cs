// Title: Read barcodes from a TIFF memory stream using Aspose.BarCode
// Description: Demonstrates how to generate a barcode, store it as TIFF in a memory stream, and then read all detected barcodes from that stream.
// Category-Description: This example belongs to the Aspose.BarCode image processing and barcode recognition category. It showcases the use of BarcodeGenerator to create barcodes, MemoryStream for in‑memory image handling, and BarCodeReader with DecodeType.AllSupportedTypes to detect any barcode present. Developers often need to process scanned documents or image buffers without writing to disk, and this pattern provides a quick way to extract barcode values directly from memory.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode, tiff, memorystream, barcodereader, decodeall, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it as TIFF in a memory stream,
/// and reading all detected barcodes from that stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, reads it from a memory stream,
    /// and prints detected barcode values to the console.
    /// </summary>
    static void Main(string[] args)
    {
        // Generate a sample Code128 barcode and save it as TIFF into a memory buffer
        byte[] tiffData;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Tiff);
                tiffData = ms.ToArray(); // Capture the TIFF bytes
            }
        }

        // Create a new memory stream from the TIFF byte array for reading
        using (var tiffStream = new MemoryStream(tiffData))
        {
            // Initialize the reader to detect any supported barcode type
            using (var reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
            {
                // Ensure the stream position is at the beginning before reading
                tiffStream.Position = 0;
                reader.SetBarCodeImage(tiffStream); // Explicitly set the image source

                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output each detected barcode value
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected barcode: {result.CodeText}");
                }

                // Inform the user if no barcodes were found
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected in the provided TIFF data.");
                }
            }
        }
    }
}