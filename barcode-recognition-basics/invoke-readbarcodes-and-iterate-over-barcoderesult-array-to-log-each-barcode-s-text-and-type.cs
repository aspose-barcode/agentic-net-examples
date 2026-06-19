using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, then reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode image in memory and reads it back, printing the type and text.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image as a bitmap in memory.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader to decode all supported barcode types from the bitmap.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the barcode type (e.g., Code128) to the console.
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        // Output the decoded text/value of the barcode.
                        Console.WriteLine("BarCode Text: " + result.CodeText);
                    }
                }
            }
        }
    }
}