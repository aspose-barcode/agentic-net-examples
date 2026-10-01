// Title: Read QR Code Structured‑Append Parity Data and Validate Segments
// Description: Demonstrates generating QR code segments with structured‑append information, then reading each segment to verify total count, sequence index, and parity byte.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes with StructuredAppend settings and BarCodeReader for extracting QR extended data. Developers working with multi‑segment QR codes—such as those used in inventory, ticketing, or data‑splitting scenarios—can use these APIs to assemble, validate, and process structured‑append barcodes efficiently.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr, structured-append, barcode, generation, recognition, parity, csharp, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates QR code segments with Structured‑Append information, reads them back,
/// and validates the total segment count, sequence index, and parity byte.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary QR images, validates their
    /// Structured‑Append metadata, and optionally cleans up the generated files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated QR codes
        string tempFolder = Path.Combine(Path.GetTempPath(), "QR_SA_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define Structured‑Append parameters
        const byte totalSegments = 3;
        const byte parityByte = 0xAA; // Example parity value

        // Store expected values for validation (file path and expected 1‑based index)
        var expectedData = new List<(string FilePath, byte Index)>();

        // Generate QR code segments with Structured‑Append information
        for (byte i = 0; i < totalSegments; i++)
        {
            string codeText = $"Segment{i + 1}";
            string filePath = Path.Combine(tempFolder, $"qr_{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Configure Structured‑Append settings
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = (byte)(i + 1); // 1‑based index
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Record the expected index for later validation
            expectedData.Add((filePath, (byte)(i + 1)));
        }

        // Read each generated QR code and validate Structured‑Append data
        foreach (var (filePath, expectedIndex) in expectedData)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.QR))
                {
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(filePath)}");
                        continue;
                    }

                    var result = results[0];
                    var qrExt = result.Extended.QR;

                    // Validate total segment count, index, and parity byte
                    bool totalOk = qrExt.StructuredAppendModeBarCodesQuantity == totalSegments;
                    bool indexOk = qrExt.StructuredAppendModeBarCodeIndex == expectedIndex;
                    bool parityOk = qrExt.StructuredAppendModeParityData == parityByte;

                    // Output validation results
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Expected TotalSegments: {totalSegments}, Detected: {qrExt.StructuredAppendModeBarCodesQuantity} => {(totalOk ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Index: {expectedIndex}, Detected: {qrExt.StructuredAppendModeBarCodeIndex} => {(indexOk ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Parity: 0x{parityByte:X2}, Detected: 0x{qrExt.StructuredAppendModeParityData:X2} => {(parityOk ? "OK" : "FAIL")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        // Cleanup: optionally delete the temporary folder
        // Directory.Delete(tempFolder, true);
    }
}