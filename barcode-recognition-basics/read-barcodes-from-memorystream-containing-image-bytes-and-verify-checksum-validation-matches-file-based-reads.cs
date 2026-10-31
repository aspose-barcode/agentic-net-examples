// Title: Read barcode from file and memory stream and compare checksum validation
// Description: Demonstrates generating a Code11 barcode, saving it to a file and a MemoryStream, then reading both sources to verify that checksum validation results are identical.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, BarCodeReader to decode barcodes from files and streams, and how to access checksum information via the Extended.OneD.CheckSum property. Developers working with barcode scanning, validation, or migration between storage mediums can use these APIs to ensure consistent decoding results.
// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: code11, checksum, barcode generation, barcode recognition, memorystream, file, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code11 barcode, saves it to both a file and a memory stream,
/// reads the barcode from each source, and compares the checksum validation results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, saving, reading, and comparison logic.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string filePath = Path.Combine(tempDir, "code11.png");

        // Text to encode in the barcode
        string codeText = "123456";

        // Generate the barcode and save it to both a file and a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
        {
            // Set barcode image resolution (pixel size of X dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save barcode image to a physical file
            generator.Save(filePath, BarCodeImageFormat.Png);

            // Save barcode image to an in‑memory stream
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // -------------------- Read barcode from file --------------------
                string fileResultText = null;
                string fileChecksum = null;
                using (var readerFile = new BarCodeReader(filePath, DecodeType.Code11))
                {
                    // Use default checksum validation behavior
                    readerFile.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    // Iterate through detected barcodes (expecting a single result)
                    foreach (var result in readerFile.ReadBarCodes())
                    {
                        fileResultText = result.CodeText;
                        fileChecksum = result.Extended.OneD.CheckSum;
                    }
                }

                // -------------------- Read barcode from memory stream --------------------
                string memResultText = null;
                string memChecksum = null;
                using (var readerMem = new BarCodeReader(memoryStream, DecodeType.Code11))
                {
                    // Apply the same checksum validation setting as the file reader
                    readerMem.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    foreach (var result in readerMem.ReadBarCodes())
                    {
                        memResultText = result.CodeText;
                        memChecksum = result.Extended.OneD.CheckSum;
                    }
                }

                // -------------------- Compare results --------------------
                bool isMatch = fileResultText == memResultText && fileChecksum == memChecksum;

                Console.WriteLine("File read result:  Text = {0}, CheckSum = {1}", fileResultText ?? "null", fileChecksum ?? "null");
                Console.WriteLine("Memory read result: Text = {0}, CheckSum = {1}", memResultText ?? "null", memChecksum ?? "null");
                Console.WriteLine(isMatch
                    ? "Checksum validation matches between file and memory reads."
                    : "Mismatch in checksum validation results.");
            }
        }

        // Clean up the temporary directory and its contents
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup (e.g., file locks)
        }
    }
}