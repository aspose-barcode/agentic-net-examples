// Title: Read and Log Barcodes from Generated Image
// Description: Generates a Code128 barcode in memory, reads it using Aspose.BarCode, and logs each detected barcode's type and text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create a barcode image with BarcodeGenerator, detect barcodes using BarCodeReader, and process BarCodeResult objects. Developers commonly need to generate barcodes on the fly and immediately verify them by reading back the encoded data, useful in testing, batch processing, or dynamic document creation.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: code128, barcode generation, barcode recognition, read, console output, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, reading it, and outputting its type and text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it, and writes results to console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image in memory (as a bitmap)
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a BarCodeReader to detect all supported barcode types in the bitmap
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Read all detected barcodes and iterate over the results
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Log the barcode type (e.g., Code128) to the console
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        // Log the decoded text of the barcode to the console
                        Console.WriteLine("BarCode Text: " + result.CodeText);
                    }
                }
            }
        }
    }
}