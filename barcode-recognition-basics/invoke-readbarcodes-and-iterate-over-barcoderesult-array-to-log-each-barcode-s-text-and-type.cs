// Title: Read and Log Barcodes from Generated Image
// Description: Generates a Code128 barcode in memory, reads it using Aspose.BarCode, and logs each detected barcode's type and text.
// Prompt: Invoke ReadBarCodes and iterate over the BarCodeResult array to log each barcode's text and type.
// Tags: barcode symbology, read operation, console output, aspose.barcode, code128

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a barcode, read it, and output the results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image, reads it, and logs each detected barcode's type and text.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image in memory with the value "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Render the barcode to a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader that scans the bitmap for all supported barcode types.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes found in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Log the barcode type (symbology) and the decoded text.
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}