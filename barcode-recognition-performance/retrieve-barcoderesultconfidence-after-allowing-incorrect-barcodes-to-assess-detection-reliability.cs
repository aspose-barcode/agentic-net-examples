using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code39 barcode, saving it to a memory stream,
/// and then reading it back using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, and prints detection details to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode text (Code39). Checksum is optional, so detection may be tolerant.
        string codeText = "12345";

        // Initialize a barcode generator for Code39 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Create a memory stream to hold the generated barcode image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reading.
                ms.Position = 0;

                // Initialize a barcode reader that can decode all supported symbologies.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Allow the reader to accept barcodes with incorrect checksum or damaged data.
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Iterate through all detected barcodes and output their details.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                    }
                }
            }
        }
    }
}