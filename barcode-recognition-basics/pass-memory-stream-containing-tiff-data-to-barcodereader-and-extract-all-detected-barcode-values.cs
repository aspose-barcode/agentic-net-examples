// Title: Read barcodes from a TIFF MemoryStream using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, store it in a TIFF image within a MemoryStream, and then read all detected barcodes from that stream.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, saving them in TIFF format, and BarCodeReader with DecodeType.AllSupportedTypes to detect multiple symbologies from an in‑memory image. Developers often need to process barcodes without writing temporary files, such as in web services or CI pipelines, and this pattern provides a fast, file‑less workflow.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode, tiff, memorystream, generation, recognition, alltypes, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it as a TIFF image in a memory stream,
/// and reading all detected barcodes from that stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation and recognition workflow.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the TIFF image data
        using (MemoryStream tiffStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "123456789"
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                // Save the generated barcode directly into the memory stream in TIFF format
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);
            }

            // Reset the stream position to the beginning before reading
            tiffStream.Position = 0;

            // Initialize the barcode reader to scan the TIFF stream for all supported types
            using (BarCodeReader reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and text
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        }
    }
}