// Title: Code128 Weighted‑Position Checksum Example
// Description: Demonstrates calculating the Code 128 weighted‑position checksum for a given input string using Code Set B. Useful for validating barcode data before encoding.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: barcode, code128, checksum, weighted-position, codesetb, console

using System;

namespace Code128ChecksumDemo
{
    /// <summary>
    /// Provides a console demo for calculating a Code 128 weighted‑position checksum.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Calculates and displays the checksum for a sample input string.
        /// </summary>
        static void Main()
        {
            // Sample input string to be checksummed
            string input = "ABCD123";

            // Compute the checksum using the helper method
            int checksum = CalculateCode128Checksum(input);

            // Output the original input and its checksum
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Weighted‑position checksum (mod 103): {checksum}");
        }

        // Calculates the Code128 weighted‑position checksum (mod 103) using Code Set B.
        static int CalculateCode128Checksum(string input)
        {
            // Validate argument
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Start Code B value as defined by Code128 specification
            const int startCodeValue = 104;
            int sum = startCodeValue; // Initialize sum with start code value

            // Iterate over each character, applying weight based on position (1‑based)
            for (int i = 0; i < input.Length; i++)
            {
                // Convert character to its Code128 Code Set B numeric value
                int charValue = GetCode128ValueForCodeSetB(input[i]);

                // Position index starts at 1, so weight = i + 1
                int weight = i + 1;

                // Accumulate weighted value
                sum += charValue * weight;
            }

            // Return checksum as remainder modulo 103
            return sum % 103;
        }

        // Maps a character to its Code128 Code Set B value (ASCII 32–127 -> 0–95)
        static int GetCode128ValueForCodeSetB(char ch)
        {
            // Ensure character is within the valid range for Code Set B
            if (ch < 32 || ch > 127)
                throw new ArgumentException($"Character '{ch}' is not valid for Code128 Code Set B.");

            // Compute Code Set B value by offsetting ASCII code
            return ch - 32;
        }
    }
}