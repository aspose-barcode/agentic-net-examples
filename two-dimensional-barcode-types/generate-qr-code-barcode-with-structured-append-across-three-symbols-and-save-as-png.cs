// Title: Generate QR Code with Structured Append (3 symbols) and save as PNG
// Description: Demonstrates creating a QR Code barcode split into three structured‑append symbols and saving each part as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with Structured Append. It showcases the use of BarcodeGenerator, EncodeTypes, and QR‑specific parameters such as StructuredAppend.TotalCount, SequenceIndicator, and ParityByte. Developers often need to split large data across multiple QR symbols while preserving a single logical message, and this snippet illustrates the typical workflow for that scenario.
// Prompt: Generate a QR Code barcode with structured append across three symbols and save as PNG.
// Tags: qr code, structured append, barcode generation, png, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates three QR Code symbols using Structured Append
/// and saves each symbol as a separate PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR Code parts, calculates parity,
    /// configures Structured Append parameters, and writes PNG files to disk.
    /// </summary>
    static void Main()
    {
        // Define the three data fragments that will be combined via Structured Append.
        string[] messages = new string[]
        {
            "First part of the data",
            "Second part of the data",
            "Third part of the data"
        };

        // Calculate the parity byte required by Structured Append (XOR of UTF‑16BE bytes).
        byte parity = 0;
        foreach (string msg in messages)
        {
            foreach (char ch in msg)
            {
                // Split each character into low and high bytes (UTF‑16BE order) and XOR them.
                byte low = (byte)ch;
                byte high = (byte)(ch >> 8);
                parity ^= (byte)(low ^ high);
            }
        }

        // Generate each QR Code part with the appropriate Structured Append settings.
        for (int i = 0; i < messages.Length; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                // Set module size (pixel dimension) for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Configure Structured Append: total parts, sequence index, and parity byte.
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = 3;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;

                // Build the output file path (qr_part1.png, qr_part2.png, ...).
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), $"qr_part{i + 1}.png");

                // Save the QR Code part as a PNG image.
                generator.Save(fileName, BarCodeImageFormat.Png);

                // Inform the user about the saved file.
                Console.WriteLine($"Saved QR part {i + 1} to {fileName}");
            }
        }
    }
}