// Title: Read barcodes from a TIFF memory stream using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, store it in a TIFF memory stream, and then read all detected barcodes from that stream.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the use of BarCodeReader with DecodeType.AllSupportedTypes to extract barcode information from image streams. It highlights key classes such as BarcodeGenerator, BarCodeReader, and BarCodeImageFormat, which developers commonly use for barcode generation and recognition in automated processing pipelines.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode symbology, read, tiff, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a TIFF image in a memory stream,
/// and then reads all detected barcodes from that stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, stores it in a TIFF memory stream,
    /// and extracts barcode values using BarCodeReader.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated TIFF image.
        using (var tiffStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "1234567890" and save it as TIFF into the stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);
            }

            // Reset the stream position to the beginning before reading.
            tiffStream.Position = 0;

            // Initialize BarCodeReader to detect all supported barcode types from the TIFF stream.
            using (var reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and value.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                    Console.WriteLine($"BarCode Value: {result.CodeText}");
                }
            }
        }
    }
}