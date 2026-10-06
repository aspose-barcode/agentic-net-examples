// Title: Calculate Code128 Weighted‑Position Checksum
// Description: Demonstrates how to compute the weighted‑position checksum for a Code 128 B string, useful for validating barcode data before encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category. It shows how to use basic .NET logic alongside Aspose.BarCode concepts to calculate checksums, a common step when creating custom Code 128 barcodes. Developers often need to verify data integrity or generate manual checksum values when working with low‑level barcode APIs.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: barcode, code128, checksum, csharp, aspose.barcode, generation, validation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of calculating a Code 128 B weighted‑position checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Computes and displays the checksum for a sample string.
    /// </summary>
    static void Main()
    {
        // Sample data to be encoded in Code128 B
        string sample = "Aspose1234";

        // Compute checksum using the helper method
        int checksum = ComputeCode128Checksum(sample);

        // Output the input and resulting checksum
        Console.WriteLine($"Input: {sample}");
        Console.WriteLine($"Weighted‑position checksum (Code128 B): {checksum}");
    }

    /// <summary>
    /// Calculates the weighted‑position checksum for a Code 128 B input string.
    /// </summary>
    /// <param name="input">The string to be encoded; must contain only characters valid in Code 128 B.</param>
    /// <returns>The checksum value as defined by the Code 128 specification.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or contains invalid characters.</exception>
    static int ComputeCode128Checksum(string input)
    {
        // Validate input
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input string must not be null or empty.", nameof(input));

        // Code128 B start code value (104)
        const int startCode = 104;
        long sum = startCode; // Initialize sum with start code

        // Iterate over each character to compute weighted sum
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            // Map character to Code128 B value (ASCII 32‑127 maps to 0‑95)
            int value = c - 32;
            if (value < 0 || value > 95)
                throw new ArgumentException($"Character '{c}' at position {i} is not valid for Code128 B encoding.", nameof(input));

            int position = i + 1; // Positions are 1‑based for data characters
            sum += value * position; // Add weighted value to sum
        }

        // Modulo 103 yields the checksum
        int checksum = (int)(sum % 103);
        return checksum;
    }
}