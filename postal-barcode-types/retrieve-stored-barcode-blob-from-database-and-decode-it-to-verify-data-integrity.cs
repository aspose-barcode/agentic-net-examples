// Title: Retrieve and Decode a Barcode BLOB from a Database (In-Memory Simulation)
// Description: Demonstrates generating a barcode, storing it as a byte array (simulating a database BLOB), retrieving it, and decoding the image to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include persisting barcode images in databases, transmitting them over services, and later validating the encoded information. Developers often need to handle barcode BLOBs, convert streams, and ensure the decoded data matches the original payload.
/// Prompt: Retrieve a stored barcode BLOB from the database and decode it to verify data integrity.
/// Tags: barcode symbology, generation, recognition, blob, database, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, simulates storing it as a BLOB,
/// retrieves the BLOB, and decodes it to confirm the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a memory stream,
    /// simulates database storage, then reads and decodes the barcode to verify integrity.
    /// </summary>
    static void Main()
    {
        // Define the data to encode and the barcode symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate the barcode image and store it in a byte array (simulating a BLOB column).
        byte[] barcodeBytes;
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Adjust barcode appearance: set X-dimension (module width) in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the memory stream to a byte array for storage.
            barcodeBytes = ms.ToArray();
        }

        // ----------------------------------------------------------------------
        // In a real application, barcodeBytes would be persisted to a database
        // BLOB column using ADO.NET (e.g., SqlCommand with varbinary parameters).
        // Retrieval would return the same byte array.
        // ----------------------------------------------------------------------

        // Decode the retrieved barcode bytes to verify that the stored data is correct.
        using (var ms = new MemoryStream(barcodeBytes))
        {
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                // Iterate through all detected barcodes (typically one in this example).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"Decoded CodeType: {result.CodeType}");
                    Console.WriteLine($"Decoded CodeTypeName: {result.CodeTypeName}");
                }
            }
        }

        // Note: Replace the in‑memory handling with actual database read/write logic
        // as needed for production scenarios.
    }
}