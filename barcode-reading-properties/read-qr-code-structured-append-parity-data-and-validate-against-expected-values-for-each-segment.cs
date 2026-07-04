// Title: QR Code Structured‑Append Parity Validation Example
// Description: Demonstrates generating QR codes with structured‑append settings, reading them, and verifying the parity bytes for each segment.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr code, structured-append, parity validation, barcode generation, barcode recognition, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates QR code segments with structured‑append settings,
/// reads them back, and validates the parity bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code segments, reads each, and checks parity data.
    /// </summary>
    static void Main()
    {
        // Define structured‑append parameters
        const int totalCount = 3;
        string[] segmentTexts = { "First segment", "Second segment", "Third segment" };
        byte[] expectedParity = { 0xAA, 0xAB, 0xAC }; // sample parity bytes for each segment

        // Loop through each segment to generate, read, and validate
        for (int i = 0; i < totalCount; i++)
        {
            // Create QR generator with the current segment text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, segmentTexts[i]))
            {
                // Configure structured‑append settings for this segment
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalCount;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = expectedParity[i];

                // Generate barcode image in memory
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Initialize reader to decode the generated QR code
                    using (var reader = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        // Iterate over all recognized barcodes (should be one per image)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Retrieve parity data from the recognized barcode
                            int parity = result.Extended.QR.StructuredAppendModeParityData;

                            // Validate parity against the expected value
                            bool isValid = parity == expectedParity[i];

                            // Output validation results
                            Console.WriteLine($"Segment {i + 1}:");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                            Console.WriteLine($"  Expected Parity: 0x{expectedParity[i]:X2}");
                            Console.WriteLine($"  Detected Parity: 0x{parity:X2}");
                            Console.WriteLine($"  Parity Valid: {isValid}");
                        }
                    }
                }
            }
        }
    }
}