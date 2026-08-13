// Title: Verify Code 128 checksum character in generated barcode
// Description: Generates a Code 128 barcode, reads it back, and checks that the human‑readable text ends with the correct checksum character.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create a barcode with Code 128, enable checksum display, and validate the checksum using the BarCodeReader. It showcases key API classes such as BarcodeGenerator, BarCodeImageFormat, BarCodeReader, and ChecksumValidation, which are commonly used by developers for barcode creation, image export, and integrity verification.
// Prompt: Design a unit test that verifies the generated barcode string ends with the correct checksum character for Code 128.
// Tags: code128, checksum, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code 128 barcode, reading it back, and verifying that the
/// human‑readable text ends with the correct checksum character.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and validates the checksum.
    /// </summary>
    static void Main()
    {
        // Sample code text (without checksum)
        string codeText = "12345";

        // Create a Code128 barcode generator and enable checksum display in the human‑readable text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode the image from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Ensure that checksum validation is performed during decoding
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read the first detected barcode (if any)
                    var result = reader.ReadBarCodes().FirstOrDefault();
                    if (result == null)
                    {
                        Console.WriteLine("FAILED: No barcode detected.");
                        return;
                    }

                    // Retrieve the full decoded text and the checksum character
                    string fullCodeText = result.CodeText;
                    string checksum = result.Extended.OneD.CheckSum;

                    // Verify that the decoded text ends with the expected checksum character
                    if (!string.IsNullOrEmpty(fullCodeText) && !string.IsNullOrEmpty(checksum) &&
                        fullCodeText.EndsWith(checksum, StringComparison.Ordinal))
                    {
                        Console.WriteLine("PASSED: Checksum character is correct.");
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Expected checksum '{checksum}' at the end of '{fullCodeText}'.");
                    }
                }
            }
        }
    }
}