// Title: Compute Code 128 Set B Weighted‑Position Checksum
// Description: Demonstrates how to calculate the weighted‑position checksum for a Code 128 barcode using Set B, useful for validating barcode data before generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category. It showcases the use of Aspose.BarCode.Generation classes to compute checksums, a common requirement when creating custom Code 128 barcodes. Developers often need to verify checksum values to ensure barcode integrity and compliance with standards.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: barcode, symbology, code128, checksum, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for the checksum calculation example.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the example: computes and displays the weighted‑position checksum for a sample string using Code 128 Set B.
    /// </summary>
    static void Main()
    {
        // Sample input string to be encoded.
        string sample = "Hello123";

        // Compute the checksum using the dedicated method.
        int checksum = ComputeCode128WeightedPositionChecksum(sample);

        // Output the results to the console.
        Console.WriteLine($"Input: {sample}");
        Console.WriteLine($"Weighted‑position checksum (Code128, Set B): {checksum}");
    }

    // Calculates the weighted‑position checksum for Code128 using Code Set B.
    // Start Code B value = 104. Each character value = ASCII code - 32.
    // Checksum = (StartCode + Σ(value_i * position_i)) mod 103
    static int ComputeCode128WeightedPositionChecksum(string codeText)
    {
        // Validate input.
        if (codeText == null)
            throw new ArgumentNullException(nameof(codeText));

        const int startCodeB = 104; // Start code for Set B.
        int sum = startCodeB;       // Initialize sum with the start code value.

        // Iterate over each character to compute weighted sum.
        for (int i = 0; i < codeText.Length; i++)
        {
            char ch = codeText[i];
            int charValue = ch - 32; // Map character to Code Set B value.

            // Ensure character is within the valid range for Set B.
            if (charValue < 0 || charValue > 95)
                throw new ArgumentException($"Character '{ch}' at position {i} is not valid for Code128 Set B.", nameof(codeText));

            int position = i + 1; // Positions are 1‑based.
            sum += charValue * position;
        }

        // Compute final checksum modulo 103.
        int checksum = sum % 103;
        return checksum;
    }
}