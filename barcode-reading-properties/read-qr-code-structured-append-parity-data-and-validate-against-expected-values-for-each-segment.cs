// Title: Read QR Code Structured-Append Segments and Validate Parity
// Description: Demonstrates generating two QR code segments using Structured Append, calculating a combined parity byte, and validating each segment's index and parity data.
// Category-Description: This example belongs to the Aspose.BarCode QR code operations collection, showcasing the use of BarcodeGenerator for QR code creation with Structured Append settings and BarCodeReader for extracting extended QR metadata. Developers working with multi-part QR codes can learn how to configure segment count, sequence indicators, and parity bytes, then verify the encoded information during recognition.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr code,structured append,parity,validation,barcode generation,barcode recognition,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates QR code segments with Structured Append, calculates parity, and validates the
/// encoded segment metadata using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates two QR code segments,
    /// validates each segment, and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated QR images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Messages to encode in the two QR code segments.
        string firstMessage = "Aspose";
        string secondMessage = "常に先を行く";

        // Calculate the combined parity byte for both messages.
        byte parity = CalculateParity(firstMessage);
        parity ^= CalculateParity(secondMessage);

        // Generate QR code images for each segment with Structured Append settings.
        GenerateQrSegment(firstMessage, 0, parity, tempFolder);
        GenerateQrSegment(secondMessage, 1, parity, tempFolder);

        // Validate the generated QR code images against expected index and parity.
        ValidateQrSegment(Path.Combine(tempFolder, "segment0.png"), 0, parity);
        ValidateQrSegment(Path.Combine(tempFolder, "segment1.png"), 1, parity);

        // Attempt to delete the temporary folder; ignore any errors.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
        }
    }

    /// <summary>
    /// Calculates a simple parity byte for a given text string by XOR‑ing each character's byte value.
    /// </summary>
    /// <param name="text">The text to calculate parity for.</param>
    /// <returns>The resulting parity byte.</returns>
    static byte CalculateParity(string text)
    {
        byte parity = 0;
        foreach (char ch in text)
        {
            if (ch <= 255)
                parity ^= (byte)ch;
            else
                parity ^= (byte)(((byte)ch) ^ ((int)ch >> 8));
        }
        return parity;
    }

    /// <summary>
    /// Generates a QR code image for a single Structured Append segment.
    /// </summary>
    /// <param name="message">The text to encode.</param>
    /// <param name="index">The zero‑based sequence indicator for this segment.</param>
    /// <param name="parity">The shared parity byte for all segments.</param>
    /// <param name="folder">The folder where the image will be saved.</param>
    static void GenerateQrSegment(string message, int index, byte parity, string folder)
    {
        string filePath = Path.Combine(folder, $"segment{index}.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, message))
        {
            // Set QR code visual density.
            gen.Parameters.Barcode.XDimension.Pixels = 4;

            // Configure Structured Append parameters.
            gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = 2;          // Total number of segments.
            gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = index; // This segment's index.
            gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;   // Shared parity byte.

            // Save the generated QR code as a PNG image.
            gen.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Validates a QR code image by reading its Structured Append metadata and comparing it to expected values.
    /// </summary>
    /// <param name="filePath">Path to the QR code image file.</param>
    /// <param name="expectedIndex">The expected sequence indicator for this segment.</param>
    /// <param name="expectedParity">The expected parity byte shared across all segments.</param>
    static void ValidateQrSegment(string filePath, int expectedIndex, byte expectedParity)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Use BarCodeReader to decode the QR code and extract extended QR metadata.
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                int quantity = result.Extended.QR.StructuredAppendModeBarCodesQuantity; // Total segments.
                int index = result.Extended.QR.StructuredAppendModeBarCodeIndex;        // Detected segment index.
                byte parity = (byte)result.Extended.QR.StructuredAppendModeParityData; // Detected parity byte.

                bool isValid = quantity == 2 && index == expectedIndex && parity == expectedParity;

                Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Expected Index: {expectedIndex}, Detected Index: {index}");
                Console.WriteLine($"  Expected Parity: {expectedParity}, Detected Parity: {parity}");
                Console.WriteLine($"  Total Segments: {quantity}");
                Console.WriteLine($"  Validation: {(isValid ? "PASS" : "FAIL")}");
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }
    }
}