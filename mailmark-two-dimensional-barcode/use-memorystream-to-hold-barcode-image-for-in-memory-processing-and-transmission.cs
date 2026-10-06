// Title: In‑Memory Barcode Generation and Recognition using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode, storing it in a MemoryStream, and reading it back without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode in‑memory processing category, showcasing how to generate barcodes with BarcodeGenerator, save them to a MemoryStream, and recognize them using BarCodeReader. Typical use cases include transmitting barcode images over network streams, embedding them in documents, or processing them in web services where disk I/O is undesirable. Developers often need to work with ImageFormat, EncodeTypes, and stream‑based APIs for efficient barcode handling.
// Prompt: Use a MemoryStream to hold the barcode image for in‑memory processing and transmission.
// Tags: barcode, code128, memorystream, in‑memory, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a MemoryStream,
/// and recognizing it directly from the stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, stores, and reads a barcode entirely in memory.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the X-dimension (module width) to 2 points for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Create a memory stream to hold the generated barcode image
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to memory stream, length = {ms.Length} bytes.");

                // Reset stream position to the beginning before reading
                ms.Position = 0;

                // Initialize a barcode reader to decode from the memory stream
                using (var reader = new BarCodeReader(ms))
                {
                    bool found = false;

                    // Iterate through all detected barcodes in the stream
                    foreach (var result in reader.ReadBarCodes())
                    {
                        found = true;
                        Console.WriteLine($"Detected type: {result.CodeTypeName}, text: {result.CodeText}");
                    }

                    // Inform if no barcode was detected
                    if (!found)
                    {
                        Console.WriteLine("No barcode detected in the memory stream.");
                    }
                }
            }
        }
    }
}