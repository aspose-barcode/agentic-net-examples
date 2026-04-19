using System;

namespace Code128ChecksumCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample input string
            string input = "Hello";

            // Calculate checksum
            int checksum = CalculateCode128Checksum(input);

            Console.WriteLine($"Input: \"{input}\"");
            Console.WriteLine($"Code128 weighted‑position checksum: {checksum}");
        }

        /// <summary>
        /// Calculates the Code128 weighted‑position checksum for the given input string.
        /// Assumes Code Set B (ASCII 32‑126) where each character value = ASCII code - 32.
        /// The start code for Code Set B is 104.
        /// </summary>
        /// <param name="input">The data string to encode.</param>
        /// <returns>The checksum value (0‑102).</returns>
        static int CalculateCode128Checksum(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            const int startCodeB = 104; // Start Code B
            int sum = startCodeB;

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                int charValue = ch - 32; // Code Set B mapping

                if (charValue < 0 || charValue > 95)
                    throw new ArgumentException($"Character '{ch}' at position {i} is not supported in Code Set B.", nameof(input));

                int weight = i + 1; // Position index starts at 1
                sum += charValue * weight;
            }

            int checksum = sum % 103;
            return checksum;
        }
    }
}