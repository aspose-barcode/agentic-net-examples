// Title: Read QR Code Structured Append Segments and Validate Parity
// Description: Demonstrates generating two QR code segments using Structured Append, calculating a parity byte, and verifying the structured‑append metadata during recognition.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the use of BarcodeGenerator for QR encoding with Structured Append settings, and BarCodeReader for extracting QR metadata such as total segment count, sequence index, and parity data. Developers working with multi‑segment QR codes often need to assemble messages reliably; this pattern illustrates how to create, persist, and validate each segment using key API classes like BarcodeGenerator, BarCodeReader, and QR‑specific parameters.
// Prompt: Read QR Code structured‑append parity data and validate against expected values for each segment.
// Tags: qr code, structured-append, parity, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates QR code segments with Structured Append, calculates a parity byte,
/// and validates the structured‑append metadata during decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two QR code images with Structured Append settings,
    /// then reads each image to verify total count, sequence index and parity byte.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated QR images
        string tempDir = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Messages that will be split across two QR code segments
        string firstMessage = "Aspose";
        string secondMessage = "常に先を行く";

        // Compute parity byte as XOR of UTF‑16BE bytes of both messages
        byte parity = 0;
        foreach (char ch in firstMessage)
        {
            parity ^= (ch <= 255) ? (byte)ch : (byte)(((byte)ch) ^ ((byte)((int)ch >> 8)));
        }
        foreach (char ch in secondMessage)
        {
            parity ^= (ch <= 255) ? (byte)ch : (byte)(((byte)ch) ^ ((byte)((int)ch >> 8)));
        }

        // Expected Structured Append values
        int expectedTotal = 2;          // total number of segments
        int expectedParity = parity;    // calculated parity byte

        // Generate first QR code (segment index 0)
        string firstPath = Path.Combine(tempDir, "QrSegment0.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, firstMessage))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4f;
            gen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            gen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = expectedTotal;
            gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 0;
            gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;
            gen.Save(firstPath, BarCodeImageFormat.Png);
        }

        // Generate second QR code (segment index 1)
        string secondPath = Path.Combine(tempDir, "QrSegment1.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, secondMessage))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4f;
            gen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            gen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = expectedTotal;
            gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 1;
            gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;
            gen.Save(secondPath, BarCodeImageFormat.Png);
        }

        // Validate the first QR segment against expected Structured Append data
        ValidateSegment(firstPath, expectedTotal, 0, expectedParity);

        // Validate the second QR segment against expected Structured Append data
        ValidateSegment(secondPath, expectedTotal, 1, expectedParity);
    }

    static void ValidateSegment(string imagePath, int expectedTotal, int expectedIndex, int expectedParity)
    {
        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use BarCodeReader to decode QR code and extract Structured Append metadata
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                var ext = result.Extended.QR;
                int total = ext.StructuredAppendModeBarCodesQuantity;   // total segments
                int index = ext.StructuredAppendModeBarCodeIndex;       // current segment index
                int parity = ext.StructuredAppendModeParityData;        // parity byte

                bool totalOk = total == expectedTotal;
                bool indexOk = index == expectedIndex;
                bool parityOk = parity == expectedParity;

                Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  TotalCount: {total} {(totalOk ? "OK" : $"Expected {expectedTotal}")}");
                Console.WriteLine($"  Index: {index} {(indexOk ? "OK" : $"Expected {expectedIndex}")}");
                Console.WriteLine($"  Parity: {parity} {(parityOk ? "OK" : $"Expected {expectedParity}")}");
            }
        }
    }
}