using System;

namespace Code128ChecksumDemo
{
    class Program
    {
        static void Main()
        {
            // Example usage
            string data = "HelloWorld";
            int checksum = CalculateCode128Checksum(data);
            Console.WriteLine($"Input: {data}");
            Console.WriteLine($"Weighted‑position checksum (Code 128): {checksum}");
        }

        /// <summary>
        /// Calculates the weighted‑position checksum for Code 128 using Code Set B.
        /// The algorithm:
        ///   startCode = 104 (Start B)
        ///   checksum = (startCode + Σ(value_i * position_i)) mod 103
        /// where value_i = ASCII(code) - 32 for characters in the range 32‑126.
        /// </summary>
        /// <param name="input">The string to encode (must contain only characters supported by Code Set B).</param>
        /// <returns>The checksum value (0‑102).</returns>
        /// <exception cref="ArgumentException">Thrown when input contains unsupported characters.</exception>
        static int CalculateCode128Checksum(string input)
        {
            if (input == null)
                throw new ArgumentException("Input cannot be null.", nameof(input));

            const int startCodeB = 104; // Start Code B value
            int sum = startCodeB;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c < 32 || c > 126)
                    throw new ArgumentException($"Character '{c}' at position {i} is not supported by Code Set B.", nameof(input));

                int value = c - 32; // Code Set B value mapping
                int position = i + 1; // Positions are 1‑based for data characters
                sum += value * position;
            }

            int checksum = sum % 103;
            return checksum;
        }
    }
}