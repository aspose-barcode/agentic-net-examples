using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, saving it to a memory stream,
/// and then reading it back using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, writes it to a PNG stream,
    /// and reads the barcode back to display its type and text.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the given data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Create an in‑memory stream to hold the generated image
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize a barcode reader that will attempt to decode any supported type
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Set Timeout to zero to allow unlimited processing time
                    reader.Timeout = 0;

                    // Iterate through all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the barcode type (e.g., Code128) and the decoded text
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}