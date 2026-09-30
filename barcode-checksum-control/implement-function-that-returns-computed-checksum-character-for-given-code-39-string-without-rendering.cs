// Title: Code 39 Checksum Calculation Example
// Description: Demonstrates how to compute the checksum character for a Code 39 string without generating a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode family of code‑39 operations. It shows how to use the standard Code 39 character‑value mapping to calculate a checksum, a common requirement when validating or constructing barcodes programmatically. Developers working with barcode generation, validation, or custom encoding often need to compute checksums manually, and this snippet illustrates the algorithm using basic .NET collections.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: barcode,code39,checksum,aspose.barcode,algorithm

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace Code39ChecksumDemo
{
    /// <summary>
    /// Provides a console demo that calculates the Code 39 checksum character for sample strings.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Iterates over sample inputs, computes their checksum, and writes the results to the console.
        /// </summary>
        static void Main()
        {
            // Define a set of sample strings to test the checksum calculation.
            string[] samples = { "CODE39", "HELLO-123", "A%Z" };

            // Process each sample and display the computed checksum or an error message.
            foreach (var text in samples)
            {
                try
                {
                    char checksum = ComputeCode39Checksum(text);
                    Console.WriteLine($"Input: {text} => Checksum Character: {checksum}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Input: {text} => Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Computes the Code 39 checksum character for the supplied text.
        /// Throws <see cref="ArgumentException"/> if the text contains characters not supported by Code 39.
        /// </summary>
        /// <param name="codeText">The data string (without start/stop characters).</param>
        /// <returns>The checksum character.</returns>
        static char ComputeCode39Checksum(string codeText)
        {
            // Validate input.
            if (codeText == null)
                throw new ArgumentException("Code text cannot be null.");

            // Mapping of Code 39 characters to their numeric values (0‑42).
            var charValues = new Dictionary<char, int>
            {
                // Digits
                {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
                {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
                // Uppercase letters
                {'A',10}, {'B',11}, {'C',12}, {'D',13}, {'E',14},
                {'F',15}, {'G',16}, {'H',17}, {'I',18}, {'J',19},
                {'K',20}, {'L',21}, {'M',22}, {'N',23}, {'O',24},
                {'P',25}, {'Q',26}, {'R',27}, {'S',28}, {'T',29},
                {'U',30}, {'V',31}, {'W',32}, {'X',33}, {'Y',34},
                {'Z',35},
                // Special symbols
                {'-',36}, {'.',37}, {' ',38}, {'$',39}, {'/',40},
                {'+',41}, {'%',42}
            };

            int sum = 0;

            // Accumulate the numeric values of each character, converting to uppercase for case‑insensitivity.
            foreach (char c in codeText)
            {
                char upper = char.ToUpperInvariant(c);
                if (!charValues.TryGetValue(upper, out int value))
                    throw new ArgumentException($"Character '{c}' is not valid in Code 39.");

                sum += value;
            }

            // Compute the checksum value as the remainder of division by 43.
            int checksumValue = sum % 43;

            // Reverse lookup: find the character that corresponds to the checksum value.
            foreach (var kvp in charValues)
            {
                if (kvp.Value == checksumValue)
                    return kvp.Key;
            }

            // This point should never be reached because the mapping covers all possible values (0‑42).
            throw new InvalidOperationException("Failed to map checksum value to character.");
        }
    }
}