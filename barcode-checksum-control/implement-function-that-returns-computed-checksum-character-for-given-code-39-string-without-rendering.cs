using System;
using System.Collections.Generic;

class Program
{
    // Mapping of Code 39 characters to their numeric values.
    private static readonly Dictionary<char, int> CharToValue = new Dictionary<char, int>
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
        // Note: '*' is start/stop character and is not part of data checksum calculation.
    };

    // Reverse mapping from value to character.
    private static readonly char[] ValueToChar = new char[]
    {
        '0','1','2','3','4','5','6','7','8','9',
        'A','B','C','D','E','F','G','H','I','J',
        'K','L','M','N','O','P','Q','R','S','T',
        'U','V','W','X','Y','Z','-','.',' ','$',
        '/','+','%'
    };

    // Computes the Code 39 checksum character for the supplied data string.
    // The input should contain only characters valid in the Code 39 basic set (excluding '*').
    static char ComputeCode39Checksum(string data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));

        int sum = 0;
        foreach (char ch in data)
        {
            char upper = char.ToUpperInvariant(ch);
            if (!CharToValue.TryGetValue(upper, out int value))
                throw new ArgumentException($"Invalid character '{ch}' for Code 39 checksum.", nameof(data));

            sum += value;
        }

        int checksumValue = sum % 43;
        return ValueToChar[checksumValue];
    }

    static void Main()
    {
        // Example usage:
        string input = "CODE39";
        char checksum = ComputeCode39Checksum(input);
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Computed Code 39 checksum character: {checksum}");
    }
}