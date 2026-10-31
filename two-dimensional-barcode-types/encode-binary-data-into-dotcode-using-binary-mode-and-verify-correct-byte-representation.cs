// Title: Encode binary data into DotCode barcode and verify byte representation
// Description: Demonstrates how to generate a DotCode barcode in binary mode from a byte array, save it as PNG, and read it back to confirm the encoded bytes match the original data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows the use of BarcodeGenerator with EncodeTypes.DotCode, setting DotCodeEncodeMode.Binary, and BarCodeReader for decoding. Typical use cases include encoding raw binary payloads (e.g., firmware, identifiers) into DotCode symbols and validating them programmatically. Developers often need to work with the BarcodeGenerator.Parameters.Barcode.DotCode API and the BarCodeReader.DecodeType enumeration to handle binary data.
// Prompt: Encode binary data into DotCode using Binary mode and verify correct byte representation.
// Tags: dotcode, binary, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding a byte array into a DotCode barcode using binary mode,
/// saving the image, and verifying the encoded bytes by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Prepare binary data to be encoded.
        byte[] binaryData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };

        // Create a temporary folder for the generated barcode image.
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBinary_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string imagePath = Path.Combine(outputFolder, "dotcode_binary.png");

        // Generate DotCode barcode in Binary mode and save it as PNG.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            generator.SetCodeText(binaryData);
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Binary;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the barcode by reading it back and comparing the decoded bytes.
        BaseDecodeType decodeType = DecodeType.DotCode;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            bool matchFound = false;

            // Iterate through all detected barcodes (there should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                byte[] readBytes = result.CodeBytes;

                // Check that the decoded byte array matches the original length and content.
                if (readBytes != null && readBytes.Length == binaryData.Length)
                {
                    matchFound = true;
                    for (int i = 0; i < readBytes.Length; i++)
                    {
                        if (readBytes[i] != binaryData[i])
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                Console.WriteLine(matchFound
                    ? "Success: Read bytes match the original binary data."
                    : "Failure: Read bytes do not match the original binary data.");
            }

            // If no matching barcode was found, report the failure.
            if (!matchFound)
            {
                Console.WriteLine("No barcode detected or byte mismatch.");
            }
        }

        // Clean up (optional): delete the temporary folder and its contents.
        // Directory.Delete(outputFolder, true);
    }
}