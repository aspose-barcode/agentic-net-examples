// Title: Calculate weighted‑position checksum for Code 128 (Subset B) strings
// Description: Demonstrates how to compute the Code 128 subset B checksum using the weighted‑position algorithm, useful for validating barcode data before encoding.
// Category-Description: This example belongs to the Aspose.BarCode checksum calculation category, illustrating how to generate a Code 128 checksum without generating the full barcode image. It highlights the use of basic .NET string handling and arithmetic to prepare data for barcode generation, a common step for developers integrating barcode validation into inventory or shipping systems. Typical use cases include pre‑validation of data, custom barcode generation pipelines, and ensuring data integrity before encoding with Aspose.BarCode classes such as BarcodeGenerator.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: code128, checksum, console, aspose.barcode

using System;

/// <summary>
/// Provides functionality to calculate the weighted‑position checksum for Code 128 (Subset B) strings and demonstrates its usage.
/// </summary>
class Program
{
    // Calculates the weighted‑position checksum for Code 128 (subset B) input string.
    // The algorithm:
    //   checksum = (StartCodeB + Σ(charValue * position)) mod 103
    //   where charValue = ASCII code - 32 for characters 32‑127.
    static int CalculateCode128Checksum(string input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        const int startCodeB = 104; // Start Code B value
        int sum = startCodeB;

        // Iterate over each character, applying the weighted calculation.
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (c < 32 || c > 127)
                throw new ArgumentException("Input contains characters not supported by Code128 subset B.", nameof(input));

            int charValue = c - 32;          // Code128 B character value
            int weight = i + 1;              // Position index starts at 1
            sum += charValue * weight;
        }

        int checksum = sum % 103;
        return checksum;
    }

    /// <summary>
    /// Entry point of the program that demonstrates checksum calculation for a sample input string.
    /// </summary>
    static void Main()
    {
        // Sample input
        string sample = "Hello123";

        try
        {
            // Compute checksum for the sample string.
            int checksum = CalculateCode128Checksum(sample);
            Console.WriteLine($"Input: \"{sample}\"");
            Console.WriteLine($"Code128 weighted‑position checksum (subset B): {checksum}");
        }
        catch (Exception ex)
        {
            // Output any validation errors.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}