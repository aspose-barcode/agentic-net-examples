// Title: Compute Code 39 checksum character without rendering
// Description: Demonstrates how to calculate the checksum character for a Code 39 barcode string using Aspose.BarCode utilities, without generating an image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology-specific calculations. It showcases the use of character‑to‑value mappings and checksum logic for Code 39, a common linear barcode. Developers often need to validate or generate checksum characters when creating custom barcode strings or integrating with legacy systems.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: barcode symbology, checksum, code39, generation, aspnet, csharp

using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates computing the Code 39 checksum character for a given input string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Computes and prints the checksum for a sample string.
    /// </summary>
    static void Main()
    {
        // Sample input string for which the checksum will be calculated
        string sample = "CODE39";

        // Compute the checksum character using the helper method
        char checksum = ComputeCode39Checksum(sample);

        // Output the original input and the resulting checksum character
        Console.WriteLine($"Input: {sample}");
        Console.WriteLine($"Checksum character: {checksum}");
    }

    /// <summary>
    /// Calculates the Code 39 checksum character for the provided text.
    /// </summary>
    /// <param name="text">The input string to calculate the checksum for. Must contain only valid Code 39 characters.</param>
    /// <returns>The checksum character as defined by the Code 39 specification.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or contains invalid characters.</exception>
    static char ComputeCode39Checksum(string text)
    {
        // Validate input
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Input text cannot be null or empty.", nameof(text));

        // Mapping of characters to their numeric values for Code 39
        Dictionary<char, int> charToValue = new Dictionary<char, int>
        {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
            {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
            {'A',10}, {'B',11}, {'C',12}, {'D',13}, {'E',14},
            {'F',15}, {'G',16}, {'H',17}, {'I',18}, {'J',19},
            {'K',20}, {'L',21}, {'M',22}, {'N',23}, {'O',24},
            {'P',25}, {'Q',26}, {'R',27}, {'S',28}, {'T',29},
            {'U',30}, {'V',31}, {'W',32}, {'X',33}, {'Y',34},
            {'Z',35}, {'-',36}, {'.',37}, {' ',38}, {'$',39},
            {'/',40}, {'+',41}, {'%',42}
        };

        // Reverse mapping from numeric value back to character
        char[] valueToChar = new char[43]
        {
            '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J',
            'K','L','M','N','O','P','Q','R','S','T',
            'U','V','W','X','Y','Z','-','.',' ','$',
            '/','+','%'
        };

        int sum = 0;

        // Iterate over each character, convert to uppercase, and accumulate the weighted sum
        foreach (char c in text.ToUpperInvariant())
        {
            // Retrieve the numeric value; throw if character is not part of Code 39
            if (!charToValue.TryGetValue(c, out int val))
                throw new ArgumentException($"Character '{c}' is not valid for Code 39.", nameof(text));

            // Add value to running total and keep it within the modulo 43 range
            sum = (sum + val) % 43;
        }

        // Convert the final sum back to its corresponding checksum character
        return valueToChar[sum];
    }
}