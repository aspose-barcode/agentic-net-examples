// Title: Read and compare EAN13 barcode checksum from file and MemoryStream
// Description: Demonstrates generating an EAN13 barcode, saving it to a file and a MemoryStream, then reading both sources with checksum validation to ensure they match.
// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: ean13, checksum, memorystream, file, barcode, generation, recognition, aspose

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, saving, and checksum validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an EAN13 barcode, saves it to a file and MemoryStream, then reads both with checksum validation and compares results.
    /// </summary>
    static void Main()
    {
        // Sample EAN13 barcode with a valid checksum
        const string codeText = "1234567890128";
        const string filePath = "barcode.png";

        // Generate the barcode image and save it to a file and a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            // Save the generated barcode to a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);

            // Save the generated barcode to a MemoryStream
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // Read barcode from file with checksum validation enabled
                using (var fileReader = new BarCodeReader(filePath, DecodeType.EAN13))
                {
                    fileReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    BarCodeResult fileResult = GetFirstResult(fileReader);

                    // Read barcode from memory stream with the same checksum setting
                    using (var streamReader = new BarCodeReader(memoryStream, DecodeType.EAN13))
                    {
                        streamReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                        BarCodeResult streamResult = GetFirstResult(streamReader);

                        // Verify that both reads produce identical code text and checksum
                        bool match = fileResult != null && streamResult != null &&
                                     fileResult.CodeText == streamResult.CodeText &&
                                     fileResult.Extended.OneD.CheckSum == streamResult.Extended.OneD.CheckSum;

                        Console.WriteLine($"Checksum validation match: {match}");
                        if (fileResult != null)
                        {
                            Console.WriteLine($"File read - CodeText: {fileResult.CodeText}, CheckSum: {fileResult.Extended.OneD.CheckSum}");
                        }
                        if (streamResult != null)
                        {
                            Console.WriteLine($"MemoryStream read - CodeText: {streamResult.CodeText}, CheckSum: {streamResult.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
        }
    }

    // Helper to obtain the first detected barcode result
    private static BarCodeResult GetFirstResult(BarCodeReader reader)
    {
        foreach (var result in reader.ReadBarCodes())
        {
            return result;
        }
        return null;
    }
}