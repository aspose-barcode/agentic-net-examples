// Title: Encode binary data into DotCode using Binary mode and verify byte representation
// Description: This example demonstrates how to generate a DotCode barcode from raw binary data using the Binary encode mode, then reads the barcode back to confirm the byte sequence matches the original.
// Category-Description: Aspose.BarCode examples for DotCode symbology illustrate generating and recognizing barcodes. This collection shows usage of BarcodeGenerator, BarCodeReader, and related parameter classes for encoding binary payloads, a common requirement in inventory, asset tracking, and data‑matrix applications where exact byte fidelity is needed.
// Prompt: Encode binary data into DotCode using Binary mode and verify correct byte representation.
// Tags: dotcode, binary, encoding, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates encoding binary data into a DotCode barcode using Binary mode,
/// saving it as PNG, and verifying the decoded bytes match the original payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Sample binary data to encode
        byte[] originalData = new byte[] { 0xFF, 0x00, 0xAB, 0x01, 0x7E, 0x55 };

        // Path for the generated barcode image
        string imagePath = "dotcode.png";

        // ---------- Generate DotCode barcode in Binary mode ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Set the binary payload directly
            generator.SetCodeText(originalData);

            // Configure the generator to use Binary encode mode for DotCode
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Binary;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ---------- Read the barcode and verify the decoded bytes ----------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            // Attempt to read all barcodes from the image
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Take the first detected result (only one expected)
            var result = results[0];

            // The decoded CodeText is a string where each character maps to a byte (ISO‑8859‑1)
            byte[] decodedData = Encoding.GetEncoding("ISO-8859-1").GetBytes(result.CodeText);

            // Compare original and decoded byte arrays for exact match
            bool match = originalData.Length == decodedData.Length;
            if (match)
            {
                for (int i = 0; i < originalData.Length; i++)
                {
                    if (originalData[i] != decodedData[i])
                    {
                        match = false;
                        break;
                    }
                }
            }

            // Output verification result
            Console.WriteLine(match
                ? "Success: Decoded bytes match the original data."
                : "Error: Decoded bytes do NOT match the original data.");

            // Optional: display the byte values for visual confirmation
            Console.WriteLine("Original : " + BitConverter.ToString(originalData));
            Console.WriteLine("Decoded  : " + BitConverter.ToString(decodedData));
        }
    }
}