// Title: Decode Postnet barcode from memory stream and verify checksum
// Description: Demonstrates decoding a Postnet barcode generated in‑memory and checking its checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator (EncodeTypes.Postnet) to create a barcode, store it in a MemoryStream, and then use BarCodeReader (DecodeType.Postnet) to read and validate the checksum. Developers working with postal barcodes often need to generate, transmit, and verify barcodes without persisting files, making in‑memory processing essential.
// Prompt: Decode a Postnet barcode from a memory stream and verify checksum correctness.
// Tags: postnet, barcode, decode, checksum, memory stream, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode, reads it from a memory stream,
/// and validates the checksum of the decoded value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, decodes, and validates a Postnet barcode.
    /// </summary>
    static void Main()
    {
        // Sample ZIP code (without checksum)
        string zip = "12345";

        // Create a memory stream to hold the generated barcode image
        using (var ms = new MemoryStream())
        {
            // Generate Postnet barcode and write it as PNG into the memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zip))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading
            ms.Position = 0;

            // Initialize a barcode reader for Postnet from the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.Postnet))
            {
                // Turn on checksum validation during reading
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes found in the stream
                var results = reader.ReadBarCodes();

                // If no barcode was detected, inform the user and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No Postnet barcode detected.");
                    return;
                }

                // Process each decoded barcode result
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                    // Verify checksum manually if the decoded text includes the check digit
                    if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText.Length > zip.Length)
                    {
                        // Extract the check digit (last character of the decoded text)
                        char decodedCheckChar = result.CodeText[result.CodeText.Length - 1];

                        // Compute the expected check digit from the original ZIP code
                        int sum = 0;
                        foreach (char c in zip)
                        {
                            if (char.IsDigit(c))
                                sum += c - '0';
                        }
                        int expectedCheck = (10 - (sum % 10)) % 10;

                        // Compare decoded check digit with the expected one
                        bool checksumMatches = decodedCheckChar - '0' == expectedCheck;
                        Console.WriteLine($"Checksum validation result: {(checksumMatches ? "Valid" : "Invalid")}");
                    }
                    else
                    {
                        Console.WriteLine("Checksum digit not present in decoded text.");
                    }
                }
            }
        }
    }
}