// Title: Generate QR Code with Structured Append (3 symbols)
// Description: Demonstrates creating a QR Code split into three parts using Structured Append and saving each part as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category, showcasing the use of BarcodeGenerator, QR structured append parameters, and image export. Developers often need to split large data across multiple QR symbols while preserving order, using the QR structured append feature to reconstruct the original message. The snippet illustrates setting total count, sequence indicator, parity byte, and error correction level, useful for applications like multi‑part data transmission or packaging labels.
// Prompt: Generate a QR Code barcode with structured append across three symbols and save as PNG.
// Tags: qr code, structured append, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code split into three symbols using Structured Append and saving each as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates three QR Code parts with structured append settings and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Define the data fragments that will be combined via Structured Append
        string[] parts = { "Hello ", "World", "!" };
        const int totalCount = 3;          // Total number of QR symbols in the sequence
        const byte parityByte = 0;         // Parity byte (any value is acceptable; 0 is used here)

        // Iterate over each fragment and generate a separate QR symbol
        for (int i = 0; i < parts.Length; i++)
        {
            // Initialize the generator with QR encoding and the current fragment
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, parts[i]))
            {
                // Configure Structured Append parameters for this symbol
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalCount;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i; // Zero‑based index
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Optional: set the desired error correction level (Level M is a common choice)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Build the output file name (e.g., qr_part1.png, qr_part2.png, ...)
                string fileName = $"qr_part{i + 1}.png";

                // Save the generated QR symbol as a PNG image
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }
    }
}