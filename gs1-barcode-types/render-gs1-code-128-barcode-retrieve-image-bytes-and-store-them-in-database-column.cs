// Title: Render GS1 Code 128 barcode and store image bytes
// Description: Demonstrates generating a GS1 Code 128 barcode, extracting the PNG image bytes, and persisting them (simulated) as binary data.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1Code128. Typical use cases include creating GS1‑compliant barcodes for product identification, retrieving the image as a byte array, and saving it to a database (e.g., varbinary column). Developers often need to render barcodes on‑the‑fly and store the resulting images for later retrieval or printing.
// Prompt: Render a GS1 Code 128 barcode, retrieve image bytes, and store them in a database column.
// Tags: gs1, code128, barcode, generation, image-bytes, varbinary, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// converts it to a PNG byte array, and simulates storing the bytes in a database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, extracts image bytes,
    /// and writes them to a file (as a placeholder for a database column).
    /// </summary>
    static void Main()
    {
        // Define the barcode text with a valid GTIN‑14 Application Identifier (01)
        string codeText = "(01)01234567890128";

        // Initialize the barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Retrieve the raw image bytes from the stream
                byte[] imageBytes = memoryStream.ToArray();

                // Simulate persisting the image bytes to a database column.
                // In a real scenario, this byte array would be written to a varbinary column.
                // Here we write it to a local file as a placeholder.
                string outputFile = "barcode_image.bin";
                File.WriteAllBytes(outputFile, imageBytes);

                Console.WriteLine($"Barcode image bytes ({imageBytes.Length} bytes) saved to '{outputFile}'.");
            }
        }
    }
}