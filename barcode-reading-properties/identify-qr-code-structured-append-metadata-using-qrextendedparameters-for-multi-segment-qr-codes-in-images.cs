// Title: Identify QR Code Structured Append Metadata Using Aspose.BarCode
// Description: Demonstrates generating multi‑segment QR codes with Structured Append and reading their metadata (total count, sequence index, parity) from image files.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes with Structured Append parameters and BarCodeReader with QrExtendedParameters for extracting Structured Append metadata. Developers working with large data that must be split across multiple QR symbols, or needing to validate multi‑segment QR codes, will find these APIs essential.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr code, structured append, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates QR code parts using Structured Append and reads their metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates QR code segments, saves them, and reads Structured Append information.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated QR code images.
        string tempDir = Path.Combine(Path.GetTempPath(), "QrStructuredAppendDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Messages to be encoded as separate QR code parts.
        string[] messages = { "Aspose", "常に先を行く" };

        // Calculate parity byte required for Structured Append (XOR of all characters).
        byte parity = 0;
        foreach (char ch in messages[0])
            parity ^= (ch <= 255) ? (byte)ch : (byte)((byte)ch ^ (byte)((int)ch >> 8));
        foreach (char ch in messages[1])
            parity ^= (ch <= 255) ? (byte)ch : (byte)((byte)ch ^ (byte)((int)ch >> 8));

        // Generate each QR code part with Structured Append parameters.
        for (int i = 0; i < messages.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"qr_part{i}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                // Set QR code visual density.
                gen.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure Structured Append settings.
                gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = messages.Length;
                gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;

                // Save the QR code image.
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Reading QR Code Structured Append metadata:");

        // Read each saved QR code image and output Structured Append metadata.
        for (int i = 0; i < messages.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"qr_part{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"BarCodesQuantity: {result.Extended.QR.StructuredAppendModeBarCodesQuantity}");
                    Console.WriteLine($"BarCodeIndex: {result.Extended.QR.StructuredAppendModeBarCodeIndex}");
                    Console.WriteLine($"ParityData: {result.Extended.QR.StructuredAppendModeParityData}");
                }
            }
        }
    }
}