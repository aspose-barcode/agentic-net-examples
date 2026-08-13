// Title: Encode binary data into DotCode barcode and verify byte representation
// Description: Demonstrates how to generate a DotCode barcode in Binary mode from raw byte data, save it as PNG, then read it back and confirm that the decoded bytes match the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.DotCode, set DotCodeEncodeMode.Binary, and employ BarCodeReader for decoding. Developers working with high‑density 2‑D barcodes often need to embed arbitrary binary payloads, so this snippet illustrates the typical workflow of encoding raw bytes, saving the image, and validating the result.
// Prompt: Encode binary data into DotCode using Binary mode and verify correct byte representation.
// Tags: dotcode, binary, barcode generation, barcode recognition, aspnet, c#, aspose.barcode, png, encoding, decoding

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding binary data into a DotCode barcode using Binary mode,
/// saving the image, and verifying the decoded byte sequence.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DotCode barcode from a byte array,
    /// saves it as a PNG file, reads it back, and checks that the decoded bytes match the original data.
    /// </summary>
    static void Main()
    {
        // Sample binary data to encode
        byte[] originalData = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0xAB, 0xCD };

        // Path for the generated barcode image (temporary folder)
        string imagePath = Path.Combine(Path.GetTempPath(), "dotcode_binary.png");

        // ---------- Generate DotCode barcode in Binary mode ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Encode raw bytes directly
            generator.SetCodeText(originalData);

            // Set the DotCode encode mode to Binary to preserve exact byte values
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");

        // ---------- Decode the barcode and verify the byte representation ----------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Convert the decoded string back to bytes using ISO-8859-1 (1:1 byte mapping)
                byte[] decodedBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(result.CodeText);

                // Show original and decoded data as hexadecimal strings for easy comparison
                string originalHex = BitConverter.ToString(originalData);
                string decodedHex = BitConverter.ToString(decodedBytes);

                Console.WriteLine($"Original bytes : {originalHex}");
                Console.WriteLine($"Decoded  bytes : {decodedHex}");

                // Simple verification: compare the hex representations
                bool match = originalHex == decodedHex;
                Console.WriteLine($"Verification result: {(match ? "SUCCESS" : "FAILURE")}");
            }
        }
    }
}