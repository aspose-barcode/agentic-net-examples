// Title: Compute Code 39 checksum character without rendering
// Description: Demonstrates how to calculate the checksum character for a Code 39 barcode string using Aspose.BarCode utilities, without generating any visual output.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category, focusing on checksum computation. It showcases the use of standard .NET collections and string handling to map Code 39 characters to values, sum them, and derive the modulo‑43 checksum. Developers working with barcode symbology often need to validate or generate checksum characters programmatically before encoding data.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: barcode, code39, checksum, symbology, aspnet, aspose.barcode, console

using System;
using System.Collections.Generic;
using Aspose.BarCode;

/// <summary>
/// Provides an example of computing a Code 39 checksum character without rendering a barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that demonstrates checksum calculation for a sample input.
    /// </summary>
    static void Main()
    {
        // Sample data to compute checksum for
        string input = "CODE39";

        // Compute the checksum character using the helper method
        char checksum = ComputeCode39Checksum(input);

        // Output the original input and its checksum character
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Checksum character: {checksum}");
    }

    /// <summary>
    /// Calculates the Code 39 checksum character for the provided data string.
    /// </summary>
    /// <param name="data">The string to calculate the checksum for. Must contain only valid Code 39 characters.</param>
    /// <returns>The checksum character according to Code 39 specifications.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="data"/> contains an invalid character.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the checksum value cannot be mapped back to a character (should never happen).</exception>
    static char ComputeCode39Checksum(string data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        // Mapping of Code 39 characters (excluding start/stop '*') to their numeric values
        var charToValue = new Dictionary<char, int>
        {
            {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
            {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
            {'A',10}, {'B',11}, {'C',12}, {'D',13}, {'E',14},
            {'F',15}, {'G',16}, {'H',17}, {'I',18}, {'J',19},
            {'K',20}, {'L',21}, {'M',22}, {'N',23}, {'O',24},
            {'P',25}, {'Q',26}, {'R',27}, {'S',28}, {'T',29},
            {'U',30}, {'V',31}, {'W',32}, {'X',33}, {'Y',34},
            {'Z',35},
            {' ',36}, {'-',37}, {'.',38}, {'$',39}, {'/',40},
            {'+',41}, {'%',42}
        };

        int sum = 0;

        // Sum the numeric values of each character in the input string
        foreach (char ch in data)
        {
            char upper = char.ToUpperInvariant(ch);
            if (!charToValue.TryGetValue(upper, out int value))
                throw new ArgumentException($"Invalid character '{ch}' for Code 39.", nameof(data));

            sum += value;
        }

        // Compute checksum value as modulo 43 of the sum
        int checksumValue = sum % 43;

        // Reverse lookup: find the character that corresponds to the checksum value
        foreach (var kvp in charToValue)
        {
            if (kvp.Value == checksumValue)
                return kvp.Key;
        }

        // This point should never be reached; included for safety
        throw new InvalidOperationException("Checksum value could not be mapped to a character.");
    }
}