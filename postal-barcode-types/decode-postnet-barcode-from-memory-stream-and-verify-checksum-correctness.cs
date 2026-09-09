// Title: Decode Postnet barcode from memory stream and verify checksum
// Description: Demonstrates generating a Postnet barcode, loading it into a memory stream, decoding it, and retrieving the checksum value.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a Postnet barcode, BarCodeReader to decode it from a stream, and the checksum validation feature. Developers working with postal barcodes often need to generate printable codes, read them from images or streams, and ensure data integrity via checksum verification.
// Prompt: Decode a Postnet barcode from a memory stream and verify checksum correctness.
// Tags: postnet, barcode, decode, checksum, memory-stream, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode, decodes it from a memory stream,
/// and displays checksum information using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, reads, and validates a Postnet barcode.
    /// </summary>
    static void Main()
    {
        // Define a sample ZIP code (Postnet without checksum; generator adds checksum automatically)
        string postnetCode = "11596";

        // Create a memory stream to hold the generated barcode image
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Generate the Postnet barcode and write it to the memory stream
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, postnetCode))
            {
                // Set the X-dimension (module width) to 3 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 3;

                // Save the barcode as a PNG image into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            memoryStream.Position = 0;

            // Initialize a barcode reader for Postnet symbology using the memory stream
            using (BarCodeReader reader = new BarCodeReader(memoryStream, DecodeType.Postnet))
            {
                // Enable default checksum validation (required to retrieve checksum data)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Iterate through all detected barcodes (should be only one in this case)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // Attempt to read the checksum value from the extended result data
                    try
                    {
                        var checksum = result.Extended.OneD.CheckSum;
                        Console.WriteLine($"Checksum: {checksum}");
                    }
                    catch (Exception ex)
                    {
                        // If checksum information is unavailable, output the error message
                        Console.WriteLine($"Checksum not available: {ex.Message}");
                    }
                }
            }
        }
    }
}