// Title: Barcode Generation and Recognition with Proper Resource Disposal
// Description: Demonstrates generating a Code128 barcode, reading it back, and disposing all unmanaged resources using using blocks.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: barcode, code128, generation, recognition, using, disposal, aspose, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, reads it, and ensures all unmanaged resources are released.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image, reads it, and writes detection results to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a Bitmap object
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Create a BarCodeReader to decode all supported barcode types from the bitmap
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Iterate through each detected barcode and output its type and text
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                } // BarCodeReader disposed here, releasing unmanaged resources
            } // Bitmap disposed here
        } // BarcodeGenerator disposed here
    }
}