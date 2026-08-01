// Title: Read barcode from MemoryStream and verify checksum against file read
// Description: Demonstrates generating an EAN‑13 barcode, saving it to a file and a MemoryStream, then reading both sources with checksum validation to ensure they match.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, covering typical scenarios such as saving to different storage mediums, using MemoryStream for in‑memory processing, and enabling checksum validation. Developers often need these patterns when integrating barcode handling into web services, batch processors, or desktop applications.
// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: ean13, checksum, barcode, generation, recognition, memorystream, file, aspose.barcode, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an EAN‑13 barcode, saves it to both a file and a MemoryStream,
/// then reads the barcode from each source with checksum validation enabled to compare results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, storage, and verification steps.
    /// </summary>
    static void Main()
    {
        // Define the barcode data (EAN‑13 with checksum digit)
        string ean13Code = "1234567890128";

        // Determine the output file path in the current working directory
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a barcode generator for the specified symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, ean13Code))
        {
            // Save the generated barcode image to a physical file (PNG format)
            generator.Save(outputFile, BarCodeImageFormat.Png);

            // Also save the barcode image to an in‑memory stream for later reading
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position to the beginning for reading

                // -------------------- Read from file --------------------
                using (var readerFile = new BarCodeReader(outputFile, DecodeType.EAN13))
                {
                    // Enable checksum validation for the file‑based read
                    readerFile.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    var resultsFile = readerFile.ReadBarCodes();

                    // -------------------- Read from MemoryStream --------------------
                    using (var readerStream = new BarCodeReader(memoryStream, DecodeType.EAN13))
                    {
                        // Enable checksum validation for the stream‑based read
                        readerStream.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                        var resultsStream = readerStream.ReadBarCodes();

                        // Assume a single barcode result for each reader
                        var resultFile = resultsFile.Length > 0 ? resultsFile[0] : null;
                        var resultStream = resultsStream.Length > 0 ? resultsStream[0] : null;

                        // Validate that both reads succeeded
                        if (resultFile == null || resultStream == null)
                        {
                            Console.WriteLine("Failed to read barcode from one of the sources.");
                            return;
                        }

                        // Output the decoded values and checksum information
                        Console.WriteLine("File Read - CodeText: " + resultFile.CodeText);
                        Console.WriteLine("File Read - CheckSum: " + resultFile.Extended.OneD.CheckSum);
                        Console.WriteLine("Stream Read - CodeText: " + resultStream.CodeText);
                        Console.WriteLine("Stream Read - CheckSum: " + resultStream.Extended.OneD.CheckSum);

                        // Compare checksum and code text between the two sources
                        bool checksumMatches = resultFile.Extended.OneD.CheckSum == resultStream.Extended.OneD.CheckSum;
                        bool codeTextMatches = string.Equals(resultFile.CodeText, resultStream.CodeText, StringComparison.Ordinal);

                        Console.WriteLine("Checksum match: " + (checksumMatches ? "Yes" : "No"));
                        Console.WriteLine("CodeText match: " + (codeTextMatches ? "Yes" : "No"));
                    }
                }
            }
        }
    }
}