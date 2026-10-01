// Title: Read barcode from MemoryStream and compare with file‑based read
// Description: Demonstrates generating a Code128 barcode, reading it from a MemoryStream, and verifying that checksum validation yields the same result as reading the same image from a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, BarCodeReader to decode barcodes from streams and files, and how to enable checksum validation. Developers commonly need to process barcodes in memory (e.g., when receiving images over a network) and ensure that in‑memory decoding behaves identically to file‑based decoding.
// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: barcode, code128, memorystream, checksum, generation, recognition, file, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it from a MemoryStream,
/// then reads the same image from a temporary file to verify that checksum validation
/// produces identical results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, performs in‑memory and file‑based reads,
    /// and outputs the verification result to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode text; Code128 automatically includes a checksum.
        string codeText = "1234567890";

        // Create a BarcodeGenerator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Store the generated barcode image in a MemoryStream.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as PNG into the stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset position for reading.

                // Set up a BarCodeReader to decode from the MemoryStream.
                BaseDecodeType decodeType = DecodeType.Code128;
                using (var readerFromMemory = new BarCodeReader(memoryStream, decodeType))
                {
                    // Enable checksum validation (default is On, but set explicitly for clarity).
                    readerFromMemory.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Decode barcodes from the memory stream.
                    var results = readerFromMemory.ReadBarCodes();
                    string memoryResult = results.Length > 0 ? results[0].CodeText : null;
                    Console.WriteLine($"Memory read result: {memoryResult}");

                    // Write the same image to a temporary file for file‑based decoding.
                    string tempFilePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.Position = 0; // Ensure stream is at the beginning.
                        memoryStream.CopyTo(fileStream);
                    }

                    // Decode the barcode from the temporary file.
                    using (var readerFromFile = new BarCodeReader(tempFilePath, decodeType))
                    {
                        readerFromFile.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                        var fileResults = readerFromFile.ReadBarCodes();
                        string fileResult = fileResults.Length > 0 ? fileResults[0].CodeText : null;
                        Console.WriteLine($"File read result: {fileResult}");

                        // Compare the two results to confirm they match and checksum validation succeeded.
                        bool isMatch = string.Equals(memoryResult, fileResult, StringComparison.Ordinal);
                        Console.WriteLine(isMatch
                            ? "Success: Memory and file reads match and checksum is valid."
                            : "Failure: Results differ or checksum validation failed.");
                    }

                    // Clean up the temporary file.
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
            }
        }
    }
}