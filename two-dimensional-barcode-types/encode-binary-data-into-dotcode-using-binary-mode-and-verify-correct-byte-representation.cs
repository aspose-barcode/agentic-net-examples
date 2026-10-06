// Title: Encode Binary Data into DotCode Barcode and Verify Byte Accuracy
// Description: Demonstrates how to encode a byte array into a DotCode barcode using binary mode, save it as PNG, and then read back the barcode to confirm the original byte sequence is preserved.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating DotCode symbols with binary encoding, and BarCodeReader for decoding. Developers working with low‑level data transmission, inventory systems, or any scenario requiring exact byte‑level fidelity can use these APIs to embed and retrieve binary payloads in DotCode barcodes.
// Prompt: Encode binary data into DotCode using Binary mode and verify correct byte representation.
// Tags: dotcode, binary encoding, barcode generation, barcode recognition, aspose.barcode, png, c#
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates encoding binary data into a DotCode barcode, saving it as an image,
/// and verifying that the decoded bytes match the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DotCode barcode from a byte array,
    /// saves it to a temporary PNG file, reads it back, and validates the byte content.
    /// </summary>
    static void Main()
    {
        // Define the binary data that will be encoded into the barcode.
        byte[] originalData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Determine a temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "DotCodeBinary.png");

        // --------------------------------------------------------------------
        // Generate DotCode barcode in Binary mode
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Set the raw byte array as the code text.
            generator.SetCodeText(originalData);

            // Configure the DotCode encoder to use binary encoding.
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode and verify the decoded bytes match the original data
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.DotCode))
        {
            bool matchFound = false;

            // Iterate through all detected barcodes (there should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the decoded byte array from the barcode.
                byte[] decodedBytes = result.CodeBytes;

                // Initial length check.
                matchFound = decodedBytes != null && decodedBytes.Length == originalData.Length;

                // If lengths match, compare each byte.
                if (matchFound)
                {
                    for (int i = 0; i < decodedBytes.Length; i++)
                    {
                        if (decodedBytes[i] != originalData[i])
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                // Output the original and decoded byte sequences and verification result.
                Console.WriteLine("Original Bytes : " + BitConverter.ToString(originalData));
                Console.WriteLine("Decoded  Bytes : " + (decodedBytes != null ? BitConverter.ToString(decodedBytes) : "null"));
                Console.WriteLine("Verification    : " + (matchFound ? "Success" : "Failure"));
            }

            // If no matching barcode was found, inform the user.
            if (!matchFound)
            {
                Console.WriteLine("No matching barcode found or decoding failed.");
            }
        }
    }
}