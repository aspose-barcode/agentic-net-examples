// Title: Code39 Checksum Calculator
// Description: Demonstrates how to compute the checksum character for a Code 39 barcode string without rendering the barcode.
// Category-Description: This example belongs to the Aspose.BarCode code‑39 symbology utilities category. It shows how to use the character‑value mapping tables to calculate the modulo‑43 checksum, a common step when generating Code 39 barcodes programmatically. Developers often need this logic to validate data or to embed the checksum in custom barcode generation pipelines.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: barcode symbology, checksum, code39, utility, aspnet, csharp

using System;
using System.Collections.Generic;

/// <summary>
/// Provides a console example that computes Code 39 checksum characters.
/// </summary>
class Program
{
    // Mapping of Code39 characters to their numeric values (0‑42).
    private static readonly Dictionary<char, int> CharValues = new Dictionary<char, int>
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
        {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        {'A',10}, {'B',11}, {'C',12}, {'D',13}, {'E',14},
        {'F',15}, {'G',16}, {'H',17}, {'I',18}, {'J',19},
        {'K',20}, {'L',21}, {'M',22}, {'N',23}, {'O',24},
        {'P',25}, {'Q',26}, {'R',27}, {'S',28}, {'T',29},
        {'U',30}, {'V',31}, {'W',32}, {'X',33}, {'Y',34},
        {'Z',35}, {'-',36}, {'.',37}, {' ',38}, {'$',39},
        {'/',40}, {'+',41}, {'%',42}
        // Note: '*' (start/stop) is not part of checksum calculation.
    };

    // Reverse mapping from numeric value back to the corresponding Code39 character.
    private static readonly char[] ValueToChar = new char[43]
    {
        '0','1','2','3','4','5','6','7','8','9',
        'A','B','C','D','E','F','G','H','I','J',
        'K','L','M','N','O','P','Q','R','S','T',
        'U','V','W','X','Y','Z','-','.',' ','$',
        '/','+','%'
    };

    /// <summary>
    /// Computes the Code39 checksum character for the supplied data string.
    /// The input must contain only characters valid in the Code39 charset
    /// (excluding the start/stop '*'). Throws <see cref="ArgumentException"/> for invalid input.
    /// </summary>
    /// <param name="data">The data to encode (without start/stop characters).</param>
    /// <returns>The checksum character.</returns>
    public static char ComputeCode39Checksum(string data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        int sum = 0;
        // Iterate over each character, convert to upper case, and accumulate its value.
        foreach (char ch in data.ToUpperInvariant())
        {
            if (!CharValues.TryGetValue(ch, out int value))
                throw new ArgumentException($"Invalid character '{ch}' for Code39.", nameof(data));

            sum += value;
        }

        // Modulo‑43 yields the checksum value; map it back to the character.
        int checksumValue = sum % 43;
        return ValueToChar[checksumValue];
    }

    /// <summary>
    /// Entry point that demonstrates checksum calculation for sample strings.
    /// </summary>
    static void Main()
    {
        // Sample data strings to process.
        string[] samples = { "CODE39", "HELLO-123", "ASP.NET" };

        foreach (string sample in samples)
        {
            try
            {
                char checksum = ComputeCode39Checksum(sample);
                Console.WriteLine($"Data: {sample} => Checksum: {checksum}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing \"{sample}\": {ex.Message}");
            }
        }
    }
}