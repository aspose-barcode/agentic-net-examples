// Title: Decode Postnet barcode from memory stream and validate checksum
// Description: Demonstrates generating a Postnet barcode for a US ZIP code, decoding it from an in‑memory PNG image, and verifying the checksum both via the reader and manually.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a Postnet barcode, BarCodeReader to decode it from a stream, and how to enable and verify checksum validation. Developers working with postal barcodes, ZIP code automation, or any one‑dimensional symbology will find these API classes (BarcodeGenerator, BarCodeReader, BarCodeResult) and settings (ChecksumValidation) essential for typical encoding/decoding scenarios.
// Prompt: Decode a Postnet barcode from a memory stream and verify checksum correctness.
// Tags: postnet, barcode, decode, checksum, memory stream, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode, decodes it from a memory stream,
/// and validates the checksum using both the Aspose.BarCode reader and a manual calculation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, decodes, and validates a Postnet barcode.
    /// </summary>
    static void Main()
    {
        // Define a sample US ZIP code (without the checksum digit)
        string zipCode = "11596";

        // Create a Postnet barcode generator and configure its appearance
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zipCode))
        {
            // Set the X-dimension (module width) to 3 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode image into a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Prepare a barcode reader that expects Postnet symbology
                BaseDecodeType decodeType = DecodeType.Postnet;
                using (var reader = new BarCodeReader(ms, decodeType))
                {
                    // Enable default checksum validation (reader will report checksum if present)
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    // Iterate through all decoded barcodes (there will be only one in this case)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                        // Retrieve the checksum reported by the reader (if any)
                        string checksumFromResult = result.Extended.OneD.CheckSum;
                        Console.WriteLine($"Checksum reported by reader: {checksumFromResult}");

                        // Perform manual checksum verification
                        if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText.Length > 1)
                        {
                            // Separate data part and checksum digit
                            string dataPart = result.CodeText.Substring(0, result.CodeText.Length - 1);
                            char checksumChar = result.CodeText[result.CodeText.Length - 1];

                            // Calculate sum of digits in the data part
                            int sum = 0;
                            foreach (char c in dataPart)
                            {
                                if (char.IsDigit(c))
                                    sum += c - '0';
                            }

                            // Compute expected checksum according to Postnet specification
                            int expectedChecksum = (10 - (sum % 10)) % 10;
                            int actualChecksum = checksumChar - '0';

                            bool isValid = expectedChecksum == actualChecksum;
                            Console.WriteLine($"Manual checksum calculation: expected {expectedChecksum}, actual {actualChecksum}");
                            Console.WriteLine($"Checksum valid: {isValid}");
                        }
                        else
                        {
                            Console.WriteLine("CodeText is too short to verify checksum.");
                        }
                    }
                }
            }
        }
    }
}