using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a structured-append QR code split into multiple symbols using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates three QR code parts from a sample string and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Sample data to be split into three QR symbols
        const string fullData = "HelloWorldStructuredAppendExample";

        // Split the data into three roughly equal parts
        string[] parts = SplitIntoParts(fullData, 3);

        // Common structured append settings
        const int totalSymbols = 3;
        const byte parityByte = 0; // parity can be calculated, using 0 for simplicity

        // Generate each QR part
        for (int i = 0; i < parts.Length; i++)
        {
            // Determine output file name for the current part
            string outputPath = $"qr_part{i + 1}.png";

            // Create a barcode generator for QR code with the current data segment
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, parts[i]))
            {
                // Configure structured append parameters
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSymbols;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i; // index starts from 0
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Save the QR symbol as a PNG file
                generator.Save(outputPath);
                Console.WriteLine($"Saved QR part {i + 1} to {Path.GetFullPath(outputPath)}");
            }
        }
    }

    /// <summary>
    /// Splits the specified text into the given number of parts.
    /// The last part may be longer if the text length is not evenly divisible.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="partsCount">The number of parts to create.</param>
    /// <returns>An array containing the split text segments.</returns>
    private static string[] SplitIntoParts(string text, int partsCount)
    {
        if (partsCount <= 0) throw new ArgumentOutOfRangeException(nameof(partsCount));

        string[] result = new string[partsCount];
        int partLength = text.Length / partsCount;
        int remainder = text.Length % partsCount;
        int index = 0;

        // Distribute characters across parts, adding one extra character to the first 'remainder' parts
        for (int i = 0; i < partsCount; i++)
        {
            int currentPartLength = partLength + (i < remainder ? 1 : 0);
            result[i] = text.Substring(index, currentPartLength);
            index += currentPartLength;
        }

        return result;
    }
}