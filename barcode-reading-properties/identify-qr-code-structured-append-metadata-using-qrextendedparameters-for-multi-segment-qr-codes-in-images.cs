// Title: Identify QR Code Structured Append Metadata Using Aspose.BarCode
// Description: Demonstrates generating multi‑segment QR codes with structured‑append information and reading that metadata from the images.
// Category-Description: This example belongs to the Aspose.BarCode QR code operations collection. It showcases the use of BarcodeGenerator for QR code creation with Structured Append parameters and BarCodeReader for extracting QrExtendedParameters. Developers working with multi‑segment QR codes, such as those needing to split large messages across several symbols, can learn how to set total count, sequence indicator, and parity byte, then retrieve these values during recognition.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr code, structured-append, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating QR codes with Structured Append metadata and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates QR code images with structured‑append data, reads them, and prints the metadata.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder to store generated QR code images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppendDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample messages that will be split into separate QR code symbols
        string[] messages = new string[] { "Aspose", "BarCode" };
        int totalCount = messages.Length;

        // Calculate the parity byte (XOR of UTF‑16BE encoded bytes) required for Structured Append
        byte parity = 0;
        foreach (string msg in messages)
        {
            foreach (char ch in msg)
            {
                if (ch <= 0xFF)
                {
                    parity ^= (byte)ch;
                }
                else
                {
                    // Split Unicode character into low and high bytes for XOR calculation
                    byte low = (byte)ch;
                    byte high = (byte)((int)ch >> 8);
                    parity ^= low;
                    parity ^= high;
                }
            }
        }

        // Generate QR code images, embedding Structured Append metadata in each symbol
        for (int i = 0; i < totalCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{i + 1}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                // Set visual density of the QR code
                gen.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure Structured Append parameters
                gen.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalCount;          // Total number of symbols
                gen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;          // Zero‑based index of this symbol
                gen.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;            // Calculated parity byte

                // Save the QR code as a PNG image
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Reading QR Code structured-append metadata:");

        // Read each generated QR code and output its Structured Append metadata
        for (int i = 0; i < totalCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{i + 1}.png");
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
                    Console.WriteLine();
                }
            }
        }

        // Cleanup temporary folder (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}