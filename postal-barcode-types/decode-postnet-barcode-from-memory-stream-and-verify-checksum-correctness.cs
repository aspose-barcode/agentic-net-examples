// Title: Decode Postnet barcode from memory stream and validate checksum
// Description: Demonstrates generating a Postnet (ZIP+4) barcode, loading it from a memory stream, decoding it, and checking the embedded checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ChecksumValidation setting for verifying checksum integrity. Developers working with postal barcodes or needing to validate 1D symbologies can refer to this pattern for in‑memory processing without file I/O.
// Prompt: Decode a Postnet barcode from a memory stream and verify checksum correctness.
// Tags: postnet, barcode, checksum, generation, recognition, memory stream, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Postnet barcode, reads it from a memory stream,
/// decodes it, and displays checksum information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, decodes, and validates a Postnet barcode.
    /// </summary>
    static void Main()
    {
        // Define a sample Postnet code (ZIP+4 format)
        string postnetCode = "1159628792";

        // Create a barcode generator for the Postnet symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postnetCode))
        {
            // Set the X-dimension (module width) to 3 pixels for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode into a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning before reading
                ms.Position = 0;

                // Initialize a barcode reader to decode Postnet from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.Postnet))
                {
                    // Enable checksum validation (default is On for obligatory checksum)
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Iterate through all detected barcodes (should be only one)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Output the decoded text
                        Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                        // For 1D barcodes, checksum info is available in Extended.OneD
                        if (result.Extended?.OneD != null)
                        {
                            Console.WriteLine($"Checksum from barcode: {result.Extended.OneD.CheckSum}");
                        }
                        else
                        {
                            Console.WriteLine("No extended OneD data available.");
                        }
                    }
                }
            }
        }
    }
}