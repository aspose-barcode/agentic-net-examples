// Title: Read Barcodes from a TIFF Memory Stream using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, storing it in a TIFF image within a memory stream, and then reading all barcodes from that stream.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, saving them as TIFF images, and BarCodeReader with DecodeType.AllSupportedTypes to detect any barcode present. Developers commonly use these APIs for batch processing of scanned documents, automated data capture, and validation workflows where barcodes are embedded in image files.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode, code128, tiff, memorystream, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a TIFF image in a memory stream,
/// and then reads all barcodes from that stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, writes it to a TIFF memory stream,
    /// and extracts every detected barcode value from the stream.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Prepare a memory stream to hold the TIFF image
            using (var tiffStream = new MemoryStream())
            {
                // Save the generated barcode as a TIFF image into the memory stream
                generator.Save(tiffStream, BarCodeImageFormat.Tiff);

                // Reset the stream position to the beginning before reading
                tiffStream.Position = 0;

                // Initialize BarCodeReader to scan the stream for any supported barcode types
                using (var reader = new BarCodeReader(tiffStream, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes and output their type and data
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                    }
                }
            }
        }
    }
}