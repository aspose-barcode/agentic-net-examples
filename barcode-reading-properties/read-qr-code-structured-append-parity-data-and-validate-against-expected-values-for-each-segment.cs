// Title: QR Code Structured Append Generation and Validation
// Description: Demonstrates creating QR Code segments with structured‑append metadata, then reading them back to verify total count, sequence index, and parity byte.
// Category-Description: This example belongs to the Aspose.BarCode QR code operations collection, showcasing the use of BarcodeGenerator and BarCodeReader for structured‑append handling. It covers setting QR parameters such as total segment count, sequence indicator, and parity data, and reading extended QR metadata. Developers working with multi‑segment QR codes can use this pattern to assemble and validate data across multiple images.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr, structured-append, parity, barcode, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of QR code segments with structured‑append metadata and validation of the read metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two QR code images with structured‑append information, reads them back, and validates total count, index, and parity.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated QR code images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the messages for each QR segment
        string firstMessage = "Aspose";
        string secondMessage = "常に先を行く";

        // Compute the parity byte (XOR of UTF‑16BE bytes) for the combined message
        byte parity = ComputeParity(firstMessage + secondMessage);

        int totalCount = 2;                     // Total number of segments
        string[] messages = { firstMessage, secondMessage };
        string[] filePaths = new string[totalCount];

        // Generate QR codes with structured‑append metadata for each segment
        for (int i = 0; i < totalCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"QrSegment{i}.png");
            filePaths[i] = filePath;

            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 4;
                gen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalCount;
                gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read each QR code image and validate the structured‑append metadata
        for (int i = 0; i < totalCount; i++)
        {
            string filePath = filePaths[i];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    int readTotal = result.Extended.QR.StructuredAppendModeBarCodesQuantity;
                    int readIndex = result.Extended.QR.StructuredAppendModeBarCodeIndex;
                    int readParity = result.Extended.QR.StructuredAppendModeParityData;

                    bool totalOk = readTotal == totalCount;
                    bool indexOk = readIndex == i;
                    bool parityOk = readParity == parity;

                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Expected Total: {totalCount}, Read: {readTotal} => {(totalOk ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Index: {i}, Read: {readIndex} => {(indexOk ? "OK" : "FAIL")}");
                    Console.WriteLine($"  Expected Parity: {parity}, Read: {readParity} => {(parityOk ? "OK" : "FAIL")}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Computes the parity byte by XOR‑ing each byte of the UTF‑16BE representation of the input text.
    /// </summary>
    /// <param name="text">The text to compute parity for.</param>
    /// <returns>The computed parity byte.</returns>
    static byte ComputeParity(string text)
    {
        byte parity = 0;
        foreach (char ch in text)
        {
            if (ch <= 0xFF)
            {
                parity ^= (byte)ch;
            }
            else
            {
                byte low = (byte)ch;
                byte high = (byte)((int)ch >> 8);
                parity ^= (byte)(low ^ high);
            }
        }
        return parity;
    }
}