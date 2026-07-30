// Title: Code 128 Weighted‑Position Checksum Calculation Example
// Description: Demonstrates how to compute the weighted‑position checksum for a Code 128 barcode using Code Set B and generate the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on checksum calculation and barcode rendering. It showcases the use of BarcodeGenerator, EncodeTypes, and checksum‑related parameters, which are common tasks for developers creating Code 128 barcodes that require explicit checksum handling.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: code128, checksum, barcode, generation, aspnet, aspose.barcode, codesetb

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to compute a Code 128 weighted‑position checksum and generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates the Code 128 weighted‑position checksum using Code Set B.
    /// The checksum formula is (StartCode * 1 + Σ(charValue * position)) mod 103,
    /// where the position index starts at 2 for the first data character.
    /// </summary>
    /// <param name="text">The input string to encode (must be compatible with Code Set B).</param>
    /// <returns>The checksum value (0‑102) for the supplied text.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when a character is outside the Code Set B range.</exception>
    static int ComputeCode128Checksum(string text)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text));

        // Start Code B value is 104 (weight 1).
        int sum = 104;

        // Iterate over each character, applying its weighted value.
        for (int i = 0; i < text.Length; i++)
        {
            // Code Set B maps ASCII 32‑127 to values 0‑95.
            int charValue = text[i] - 32;
            if (charValue < 0 || charValue > 95)
                throw new ArgumentException($"Character '{text[i]}' is not supported in Code Set B.", nameof(text));

            // Position index for weighting (first character weight = 2).
            int weight = i + 2;
            sum += charValue * weight;
        }

        // Return the modulo‑103 checksum.
        return sum % 103;
    }

    /// <summary>
    /// Entry point of the example. Computes the checksum for a sample string,
    /// displays it, and generates a Code 128 barcode image with the checksum shown.
    /// </summary>
    static void Main()
    {
        // Sample input to encode.
        string input = "Hello123";

        // Compute and display the checksum.
        int checksum = ComputeCode128Checksum(input);
        Console.WriteLine($"Checksum for \"{input}\" is {checksum}");

        // Generate a Code 128 barcode with checksum enabled and displayed.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, input))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save("code128.png");
        }
    }
}