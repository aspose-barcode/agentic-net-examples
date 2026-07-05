// Title: Code 39 checksum calculation example
// Description: Demonstrates how to compute the checksum character for a Code 39 string without rendering it, useful for validation or barcode generation.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: barcode symbology, checksum, code39, console, csharp

using System;
using System.Collections.Generic;

namespace Code39ChecksumDemo
{
    /// <summary>
    /// Demonstrates computing Code 39 checksum characters.
    /// </summary>
    class Program
    {
        // Character set for Code 39 (order defines the value of each character)
        private static readonly string Code39Charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

        // Mapping from character to its numeric value for quick lookup
        private static readonly Dictionary<char, int> CharValueMap = CreateCharValueMap();

        /// <summary>
        /// Entry point. Processes sample strings and displays their checksum characters or errors.
        /// </summary>
        static void Main()
        {
            // Sample inputs to demonstrate checksum calculation
            string[] samples = { "HELLO", "CODE39", "123ABC", "ASP.NET" };

            foreach (var text in samples)
            {
                try
                {
                    // Compute checksum for each sample string
                    char checksum = ComputeCode39Checksum(text);
                    Console.WriteLine($"Input: {text}  =>  Checksum character: {checksum}");
                }
                catch (ArgumentException ex)
                {
                    // Report any validation errors (e.g., invalid characters)
                    Console.WriteLine($"Input: {text}  =>  Error: {ex.Message}");
                }
            }
        }

        // Computes the Code 39 checksum character for the supplied data string.
        // The algorithm sums the values of each character (according to the Code 39 charset)
        // and returns the character whose value equals (sum mod 43).
        private static char ComputeCode39Checksum(string data)
        {
            if (data == null)
                throw new ArgumentException("Input cannot be null.");

            int sum = 0;
            foreach (char ch in data)
            {
                // Code 39 is case‑insensitive; convert to upper case.
                char upper = char.ToUpperInvariant(ch);

                // The asterisk (*) is used only as start/stop delimiter and must not appear in data.
                if (upper == '*')
                    throw new ArgumentException("Asterisk (*) is not allowed in the data portion of a Code 39 barcode.");

                // Look up the numeric value for the character; throw if not found.
                if (!CharValueMap.TryGetValue(upper, out int value))
                    throw new ArgumentException($"Invalid character '{ch}' for Code 39.");

                sum += value;
            }

            int checksumValue = sum % 43;
            // The charset string includes the asterisk at the end; its index is 43.
            // Since checksumValue is in range 0‑42, we can safely index into the string.
            return Code39Charset[checksumValue];
        }

        // Builds a dictionary mapping each allowed character to its numeric value.
        private static Dictionary<char, int> CreateCharValueMap()
        {
            var map = new Dictionary<char, int>();
            for (int i = 0; i < Code39Charset.Length; i++)
            {
                char c = Code39Charset[i];
                // The asterisk is only a delimiter; we still map it for completeness,
                // but it will be rejected during checksum calculation.
                map[c] = i;
            }
            return map;
        }
    }
}