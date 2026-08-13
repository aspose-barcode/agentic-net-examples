// Title: Save barcode to MemoryStream and convert to Base64 string
// Description: Demonstrates generating a Code128 barcode, saving it directly to a MemoryStream in PNG format, and converting the image bytes to a Base64 string for easy transport or embedding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to create barcodes, work with in‑memory streams, and produce Base64‑encoded output. Typical use cases include embedding barcodes in JSON payloads, HTML pages, or transmitting them over APIs without writing files to disk. Developers often need to generate barcodes on the fly and serialize them for web or mobile applications.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: barcode, code128, generation, memorystream, base64, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates saving a barcode image to a MemoryStream and converting it to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, writes it to a MemoryStream in PNG format,
    /// converts the stream to a Base64 string, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create a memory stream to hold the generated barcode image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image directly into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes stored in the memory stream to a Base64 string
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string to the console
                Console.WriteLine(base64String);
            }
        }
    }
}