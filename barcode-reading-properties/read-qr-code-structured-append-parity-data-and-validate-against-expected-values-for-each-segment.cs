using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creation and validation of QR Code structured‑append segments using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates QR code segments with structured‑append settings, reads them back,
    /// and verifies that the encoded parameters match the expected values.
    /// </summary>
    static void Main()
    {
        // Define QR structured‑append parameters.
        const int totalSegments = 3;               // Total number of QR segments.
        const int parityByte = 0xAB;               // Example parity byte shared across segments.
        string[] segmentTexts = { "Segment One", "Segment Two", "Segment Three" };

        // Prepare a temporary output folder for generated QR images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeQRCodeSA");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // --------------------------------------------------------------------
        // Generate QR code segments with structured‑append configuration.
        // --------------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            // Build the file path for the current segment image.
            string filePath = Path.Combine(outputFolder, $"qr_segment_{i}.png");

            // Create a barcode generator for a QR code containing the segment text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, segmentTexts[i]))
            {
                // Configure structured‑append settings on the generator side.
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments; // Total segments.
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;      // Current segment index.
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;   // Shared parity byte.

                // Save the generated QR code image to disk.
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // Read and validate each generated QR segment.
        // --------------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            // Resolve the file path for the current segment.
            string filePath = Path.Combine(outputFolder, $"qr_segment_{i}.png");

            // Verify that the file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Open a barcode reader for the QR code image.
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                // Iterate over all detected barcodes (should be one per image).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract QR‑specific extended information.
                    var qrExt = result.Extended.QR;

                    // Compare detected structured‑append values with expected ones.
                    bool totalMatch = qrExt.StructuredAppendModeBarCodesQuantity == totalSegments;
                    bool indexMatch = qrExt.StructuredAppendModeBarCodeIndex == i;
                    bool parityMatch = qrExt.StructuredAppendModeParityData == parityByte;

                    // Output verification results to the console.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Expected Total: {totalSegments}, Detected: {qrExt.StructuredAppendModeBarCodesQuantity} => {(totalMatch ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Index: {i}, Detected: {qrExt.StructuredAppendModeBarCodeIndex} => {(indexMatch ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Parity: 0x{parityByte:X2}, Detected: 0x{qrExt.StructuredAppendModeParityData:X2} => {(parityMatch ? "OK" : "FAIL")}");
                }
            }
        }

        // Optional cleanup: remove the temporary folder and its contents.
        // Directory.Delete(outputFolder, true);
    }
}