// Title: Generate QR Code with Structured Append (3 symbols) and Save as PNG
// Description: Demonstrates how to create a QR Code barcode split across three structured‑append symbols and save each part as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with Structured Append. It showcases the use of BarcodeGenerator, EncodeTypes.QR, and QR‑specific parameters (StructuredAppend) to split data across multiple symbols. Developers working with large data payloads, multi‑page QR codes, or need to concatenate QR symbols will find this pattern useful.
// Prompt: Generate a QR Code barcode with structured append across three symbols and save as PNG.
// Tags: qr code, structured append, barcode generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates three QR Code symbols using Structured Append
/// and saves each symbol as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR Code parts, computes parity, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Data fragments to be encoded in separate QR symbols
        string[] messages = new string[]
        {
            "First part of data",
            "Second part of data",
            "Third part of data"
        };

        // Compute a single parity byte for all fragments (required by Structured Append)
        byte parity = ComputeParity(messages);

        // Generate each QR symbol with appropriate Structured Append settings
        for (int i = 0; i < messages.Length; i++)
        {
            string filePath = Path.Combine(outputFolder, $"qr_part{i}.png");

            // Create a barcode generator for QR encoding with the current fragment
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, messages[i]))
            {
                // Set module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure Structured Append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = messages.Length; // total symbols
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;        // position of this symbol (0‑based)
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parity;        // shared parity byte

                // Save the generated QR symbol as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR part {i}: {filePath}");
        }
    }

    /// <summary>
    /// Calculates the parity byte required for Structured Append by XOR‑ing all bytes of the input strings.
    /// </summary>
    /// <param name="parts">Array of string fragments to be encoded.</param>
    /// <returns>Computed parity byte.</returns>
    static byte ComputeParity(string[] parts)
    {
        byte parity = 0;
        foreach (string part in parts)
        {
            foreach (char ch in part)
            {
                if (ch <= 255)
                {
                    // Single‑byte character: XOR directly
                    parity ^= (byte)ch;
                }
                else
                {
                    // Multi‑byte character: split into low and high bytes before XOR
                    int val = ch;
                    byte low = (byte)val;
                    byte high = (byte)(val >> 8);
                    parity ^= (byte)(low ^ high);
                }
            }
        }
        return parity;
    }
}