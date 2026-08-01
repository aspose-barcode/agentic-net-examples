// Title: Barcode recognition with timeout handling
// Description: Demonstrates generating a Code128 barcode, then recognizing it with a 5‑second timeout to abort long processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes and BarCodeReader with the Timeout property to control recognition duration. Developers often need to generate barcodes on the fly and ensure recognition does not hang, especially in high‑throughput or web services.
// Prompt: Set TimeOut property to five seconds to abort recognition if processing exceeds the specified limit.
// Tags: barcode, code128, timeout, recognition, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode and recognizing it with a timeout.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves to memory, and reads it with a 5‑second timeout.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Store the generated barcode image in a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading

                // Create a BarCodeReader to recognize Code128 from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Set the timeout to 5000 ms (5 seconds) to abort if recognition takes too long
                    reader.Timeout = 5000;

                    // Iterate through all recognized barcodes and output their details
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}