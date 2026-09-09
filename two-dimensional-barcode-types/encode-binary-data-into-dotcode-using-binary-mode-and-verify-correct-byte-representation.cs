// Title: Encode binary data into DotCode barcode and verify byte integrity
// Description: Demonstrates how to generate a DotCode barcode in Binary mode from a byte array and then decode it to confirm the original byte sequence is preserved.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator with EncodeTypes.DotCode, setting DotCodeEncodeMode to Binary, and using BarCodeReader with DecodeType.DotCode to read back the encoded bytes. Developers working with low‑level data encoding, such as binary payloads in DotCode symbols, can use these APIs to embed and retrieve raw byte streams.
// Prompt: Encode binary data into DotCode using Binary mode and verify correct byte representation.
// Tags: dotcode, binary, barcode generation, barcode recognition, aspose.barcode, c#, encoding, decoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding a byte array into a DotCode barcode using Binary mode
/// and verifying the decoded bytes match the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates the barcode, saves it, reads it back, and prints verification result.
    /// </summary>
    static void Main()
    {
        // Define the binary data to encode.
        byte[] data = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "dotcode_binary.png");

        // Generate a DotCode barcode in Binary mode using the byte array.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            generator.SetCodeText(data);
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify the barcode by decoding it and comparing the result with the original data.
        bool success = false;
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.DotCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                byte[] decoded = result.CodeBytes;
                if (decoded != null && decoded.Length == data.Length)
                {
                    success = true;
                    for (int i = 0; i < decoded.Length; i++)
                    {
                        if (decoded[i] != data[i])
                        {
                            success = false;
                            break;
                        }
                    }
                }
                Console.WriteLine("Decoded bytes: " + BitConverter.ToString(decoded ?? new byte[0]));
            }
        }

        // Output verification result to the console.
        Console.WriteLine(success
            ? "Verification succeeded: decoded bytes match original data."
            : "Verification failed: decoded bytes do not match original data.");
    }
}