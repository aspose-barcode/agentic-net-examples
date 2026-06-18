using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates calculation of a Code 39 checksum for a given data string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Computes and displays the checksum for sample data.
    /// </summary>
    static void Main()
    {
        // Sample Code 39 data to be checksummed.
        string data = "HELLO-123";

        // Compute the checksum character using the helper method.
        char checksum = ComputeCode39Checksum(data);

        // Output the original data and its checksum.
        Console.WriteLine($"Data: {data}");
        Console.WriteLine($"Checksum character: {checksum}");
    }

    /// <summary>
    /// Returns the Code 39 checksum character for the supplied data string.
    /// </summary>
    /// <param name="data">The data string to calculate the checksum for.</param>
    /// <returns>The checksum character according to the Code 39 specification.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="data"/> contains an invalid character.</exception>
    static char ComputeCode39Checksum(string data)
    {
        // Validate input.
        if (data == null) throw new ArgumentNullException(nameof(data));

        // Code 39 character set (43 symbols) in order.
        const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

        // Build a lookup table: character -> numeric value.
        var valueMap = new Dictionary<char, int>();
        for (int i = 0; i < charset.Length; i++)
        {
            valueMap[charset[i]] = i;
        }

        int sum = 0;

        // Iterate over each character, converting to upper case for case‑insensitivity.
        foreach (char ch in data.ToUpperInvariant())
        {
            // Retrieve the numeric value; throw if character is not part of the charset.
            if (!valueMap.TryGetValue(ch, out int val))
                throw new ArgumentException($"Invalid character '{ch}' for Code 39.", nameof(data));

            // Accumulate the sum of values.
            sum += val;
        }

        // Modulo 43 gives the checksum value.
        int checksumValue = sum % 43;

        // Return the corresponding checksum character from the charset.
        return charset[checksumValue];
    }
}