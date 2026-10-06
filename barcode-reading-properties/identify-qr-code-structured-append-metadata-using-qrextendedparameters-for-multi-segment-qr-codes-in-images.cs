// Title: Identify QR Code Structured Append Metadata Using QrExtendedParameters
// Description: Demonstrates how to generate multi‑segment QR codes with Structured Append and then read back the append metadata using Aspose.BarCode's QrExtendedParameters.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the BarcodeGenerator, BarCodeReader, and QR‑specific parameters such as StructuredAppend and QrExtendedParameters. Developers commonly use these APIs to create split QR messages, combine them later, and extract segment information for custom processing in inventory, ticketing, or data‑link applications.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr, structured-append, barcode-generation, barcode-recognition, png, aspose.barcode, qrextendedparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of two QR code parts using Structured Append and reads their metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code segments, saves them, reads back Structured Append information, and outputs it.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a temporary directory to store generated QR code images
        string tempDir = Path.Combine(Path.GetTempPath(), "QrStructuredAppendDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Messages that will be split across two QR code segments
        string firstMessage = "Aspose";
        string secondMessage = "常に先を行く";

        // Calculate parity byte (XOR of UTF-16BE bytes) for Structured Append
        byte parity = 0;
        foreach (char ch in firstMessage)
        {
            if (ch <= 0xFF)
                parity ^= (byte)ch;
            else
            {
                byte low = (byte)ch;
                byte high = (byte)(ch >> 8);
                parity ^= (byte)(low ^ high);
            }
        }
        foreach (char ch in secondMessage)
        {
            if (ch <= 0xFF)
                parity ^= (byte)ch;
            else
            {
                byte low = (byte)ch;
                byte high = (byte)(ch >> 8);
                parity ^= (byte)(low ^ high);
            }
        }

        // Generate first QR code (segment 0) with Structured Append settings
        string file1 = Path.Combine(tempDir, "qr_part0.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, firstMessage))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = 2;
            gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 0;
            gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;
            gen.Save(file1, BarCodeImageFormat.Png);
        }

        // Generate second QR code (segment 1) with the same Structured Append settings
        string file2 = Path.Combine(tempDir, "qr_part1.png");
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, secondMessage))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = 2;
            gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 1;
            gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;
            gen.Save(file2, BarCodeImageFormat.Png);
        }

        // Output generated file paths for reference
        Console.WriteLine("Generated QR code parts:");
        Console.WriteLine(file1);
        Console.WriteLine(file2);
        Console.WriteLine();

        // Prepare to read Structured Append metadata from the generated images
        string[] files = new[] { file1, file2 };
        BaseDecodeType decodeType = DecodeType.QR;

        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Use BarCodeReader to decode each QR code and access extended QR parameters
            using (var reader = new BarCodeReader(file, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"BarCodesQuantity: {result.Extended.QR.StructuredAppendModeBarCodesQuantity}");
                    Console.WriteLine($"BarCodeIndex: {result.Extended.QR.StructuredAppendModeBarCodeIndex}");
                    Console.WriteLine($"ParityData: {result.Extended.QR.StructuredAppendModeParityData}");
                    Console.WriteLine();
                }
            }
        }

        // Optional cleanup: delete temporary directory and its contents
        // Directory.Delete(tempDir, true);
    }
}