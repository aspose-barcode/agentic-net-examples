// Title: Read barcode from file and memory stream with checksum validation
// Description: Demonstrates generating a Code39 barcode, saving it to a file and a MemoryStream, then reading both sources with checksum validation enabled to verify consistent results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, emphasizing checksum validation—a common requirement when ensuring data integrity in inventory, shipping, and tracking systems. Developers often need to compare barcode reads from different sources (files, streams) to guarantee consistent decoding behavior across environments.
// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: code39, barcode, checksum, generation, recognition, memorystream, file, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code39 barcode, saves it to both a file and a memory stream,
/// then reads the barcode from each source with checksum validation enabled to confirm identical results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, saving, reading, and verification steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for storing the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string filePath = Path.Combine(tempFolder, "code39.png");

        // --------------------------------------------------------------------
        // Define the barcode data to encode.
        // --------------------------------------------------------------------
        string codeText = "12345";

        // --------------------------------------------------------------------
        // Generate the barcode and persist it to both a file and a memory stream.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Adjust the X-dimension for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode image to a physical file.
            generator.Save(filePath, BarCodeImageFormat.Png);

            // Save the same barcode image to an in‑memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.

                // ----------------------------------------------------------------
                // Read the barcode from the file with checksum validation enabled.
                // ----------------------------------------------------------------
                BaseDecodeType decodeType = DecodeType.Code39;
                BarCodeResult[] fileResults;
                using (BarCodeReader fileReader = new BarCodeReader(filePath, decodeType))
                {
                    fileReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    fileResults = fileReader.ReadBarCodes();
                }

                // ----------------------------------------------------------------
                // Read the barcode from the memory stream with checksum validation enabled.
                // ----------------------------------------------------------------
                BarCodeResult[] memoryResults;
                using (BarCodeReader memReader = new BarCodeReader(ms, decodeType))
                {
                    memReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    memoryResults = memReader.ReadBarCodes();
                }

                // ----------------------------------------------------------------
                // Verify that both reads succeeded and that the decoded texts match.
                // ----------------------------------------------------------------
                bool fileSuccess = fileResults != null && fileResults.Length > 0;
                bool memSuccess = memoryResults != null && memoryResults.Length > 0;

                Console.WriteLine($"File read success: {fileSuccess}");
                Console.WriteLine($"Memory read success: {memSuccess}");

                if (fileSuccess && memSuccess)
                {
                    string fileText = fileResults[0].CodeText;
                    string memText = memoryResults[0].CodeText;
                    bool textsMatch = string.Equals(fileText, memText, StringComparison.Ordinal);

                    Console.WriteLine($"CodeText from file: {fileText}");
                    Console.WriteLine($"CodeText from memory: {memText}");
                    Console.WriteLine($"Texts match: {textsMatch}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: delete the temporary folder and its contents.
        // --------------------------------------------------------------------
        try
        {
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the demo flow.
        }
    }
}