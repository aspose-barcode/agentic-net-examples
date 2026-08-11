// Title: Decode Postnet barcode from memory stream and validate checksum
// Description: Demonstrates decoding a Postnet barcode that was generated in‑memory, reading it from a MemoryStream, and confirming the checksum is correct.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a Postnet barcode, store it in a MemoryStream, and then use BarCodeReader to decode the image. Typical use cases include on‑the‑fly barcode generation for web services, automated verification of postal codes, and checksum validation in batch processing. Developers often work with the BarcodeGenerator, BarCodeReader, and related settings such as ChecksumValidation to ensure data integrity.
// Prompt: Decode a Postnet barcode from a memory stream and verify checksum correctness.
// Tags: postnet, barcode, decode, checksum, memorystream, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode, decodes it from a memory stream,
/// and validates the checksum using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Postnet barcode, reads it back,
    /// and prints decoding results along with checksum verification.
    /// </summary>
    static void Main()
    {
        // Define a sample ZIP code (5 digits). The Postnet checksum digit will be added automatically.
        const string zipCode = "12345";

        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate the Postnet barcode and save it as PNG into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zipCode))
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Initialize a barcode reader for Postnet symbology using the memory stream.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Postnet))
            {
                // Enable checksum validation (On = always validate if possible).
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes (there should be only one in this case).
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type   : {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text   : {result.CodeText}");

                    // For 1D barcodes, the extended parameters contain the checksum digit.
                    var checksum = result.Extended.OneD.CheckSum;
                    Console.WriteLine($"Checksum (from barcode) : {checksum}");

                    // Since ChecksumValidation is On, a null result would indicate a failure.
                    // Presence of a result means the checksum is valid.
                    Console.WriteLine("Checksum validation: Passed");
                }
            }
        }
    }
}