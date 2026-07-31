// Title: Read QR Code Structured‑Append Parity Data and Validate Segments
// Description: Demonstrates generating multiple QR code segments with Structured Append, reading them back, and verifying parity and sequence data.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation and recognition category. It showcases the BarcodeGenerator for creating QR codes with Structured Append settings and the BarCodeReader for extracting Extended QR properties. Developers often need to split large messages across several QR symbols, ensure correct ordering, and validate parity data; this snippet provides a concise reference for those common tasks.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr code, structured append, validation, barcode generation, barcode recognition, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a series of QR codes using Structured Append, reads them back,
/// and validates the total count, sequence indicator, and parity byte for each segment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates QR code segments, reads them, and prints validation results.
    /// </summary>
    static void Main()
    {
        const int totalSegments = 3;          // Number of QR code segments to generate
        const byte parityByte = 0xAB;         // Parity byte shared across all segments
        string baseText = "Segment ";         // Base text for each QR code payload

        // Store generated QR images in memory streams for later reading
        var qrStreams = new List<MemoryStream>();

        // ------------------------------------------------------------
        // Generate QR codes with Structured Append configuration
        // ------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, baseText + i))
            {
                // Set Structured Append parameters: total count, sequence index, and parity byte
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Save the QR code image to a memory stream (PNG format)
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading
                qrStreams.Add(ms);
            }
        }

        // ------------------------------------------------------------
        // Read each QR code and validate Structured Append metadata
        // ------------------------------------------------------------
        for (int i = 0; i < qrStreams.Count; i++)
        {
            var stream = qrStreams[i];
            using (var reader = new BarCodeReader(stream, DecodeType.QR))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract reader‑side Structured Append properties from the result
                    int detectedTotal = result.Extended.QR.StructuredAppendModeBarCodesQuantity;
                    int detectedIndex = result.Extended.QR.StructuredAppendModeBarCodeIndex;
                    int detectedParity = result.Extended.QR.StructuredAppendModeParityData;

                    // Compare detected values with the expected ones
                    bool totalMatch = detectedTotal == totalSegments;
                    bool indexMatch = detectedIndex == i;
                    bool parityMatch = detectedParity == parityByte;

                    // Output validation results to the console
                    Console.WriteLine($"Segment {i}:");
                    Console.WriteLine($"  Expected TotalCount = {totalSegments}, Detected = {detectedTotal} => {(totalMatch ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected SequenceIndicator = {i}, Detected = {detectedIndex} => {(indexMatch ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected ParityByte = 0x{parityByte:X2}, Detected = 0x{detectedParity:X2} => {(parityMatch ? "OK" : "FAIL")}");
                }
            }
        }

        // ------------------------------------------------------------
        // Cleanup: dispose all memory streams
        // ------------------------------------------------------------
        foreach (var ms in qrStreams)
        {
            ms.Dispose();
        }
    }
}