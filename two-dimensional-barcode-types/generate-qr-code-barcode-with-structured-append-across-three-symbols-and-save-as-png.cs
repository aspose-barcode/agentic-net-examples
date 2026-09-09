// Title: Generate QR Code with Structured Append (3 symbols) and save as PNG
// Description: Demonstrates how to create a QR Code barcode split across three structured‑append symbols and save each part as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with Structured Append. It showcases the use of BarcodeGenerator, EncodeTypes.QR, and QR-specific parameters (StructuredAppend) to split data across multiple symbols. Developers often need this pattern for encoding long messages that exceed a single QR Code capacity, ensuring seamless scanning of sequential parts.
// Prompt: Generate a QR Code barcode with structured append across three symbols and save as PNG.
// Tags: qr code, structured append, barcode generation, png, aspose.barcode, encode types

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates three QR Code symbols using Structured Append
/// and saves each symbol as a separate PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where PNG files will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the three message parts that will be encoded across separate QR symbols.
        string[] messages = new string[]
        {
            "First part of the data",
            "Second part of the data",
            "Third part of the data"
        };

        // Calculate the parity byte required for Structured Append (XOR of all UTF‑16BE bytes).
        byte parity = 0;
        foreach (string msg in messages)
        {
            foreach (char ch in msg)
            {
                int val = ch;
                if (val <= 0xFF)
                {
                    parity ^= (byte)val;
                }
                else
                {
                    parity ^= (byte)val;
                    parity ^= (byte)(val >> 8);
                }
            }
        }

        // Generate each QR Code part with appropriate Structured Append settings.
        for (int i = 0; i < messages.Length; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                // Set visual size of the QR modules.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Configure Structured Append parameters.
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = messages.Length;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;

                // Build the file path for the current QR part and save it as PNG.
                string filePath = Path.Combine(outputDir, $"qr_part_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved QR part {i + 1} to: {filePath}");
            }
        }

        Console.WriteLine("QR Code generation with Structured Append completed.");
    }
}