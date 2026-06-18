using System;

/// <summary>
/// Demonstrates calculation of a Code128 weighted‑position checksum using Code Set B.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates the Code128 weighted‑position checksum (mod 103) for the supplied text using Code Set B.
    /// </summary>
    /// <param name="codeText">The string to calculate the checksum for.</param>
    /// <returns>The checksum value (0‑102).</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="codeText"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when a character is outside the valid Code128 Set B range.</exception>
    static int CalculateCode128Checksum(string codeText)
    {
        // Validate input
        if (codeText == null)
            throw new ArgumentNullException(nameof(codeText));

        const int startCodeB = 104; // Start Code B value per specification
        int sum = startCodeB;       // Initialize sum with the start code

        // Iterate over each character to compute weighted sum
        for (int i = 0; i < codeText.Length; i++)
        {
            char ch = codeText[i];
            int charValue = ch - 32; // Map character to Code Set B value (0‑95)

            // Ensure character is within the valid range for Code Set B
            if (charValue < 0 || charValue > 95)
                throw new ArgumentException($"Character '{ch}' at position {i} is not valid for Code128 Set B.");

            int position = i + 1; // Position index starts at 1 per checksum algorithm
            sum += charValue * position; // Add weighted value to running total
        }

        // Return checksum as modulo 103 of the accumulated sum
        return sum % 103;
    }

    /// <summary>
    /// Entry point of the program. Calculates and displays the checksum for a sample string.
    /// </summary>
    static void Main()
    {
        // Sample data to demonstrate checksum calculation
        string sample = "1234567890";

        // Compute checksum using the helper method
        int checksum = CalculateCode128Checksum(sample);

        // Output the result to the console
        Console.WriteLine($"Code128 weighted‑position checksum for \"{sample}\" is: {checksum}");
    }
}