// Title: DataMatrix barcode generation with C40 encoding validation
// Description: Demonstrates validating non‑customer fields against the C40 character set before generating a DataMatrix barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology and character set validation. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEncodeMode classes to produce PNG images, a common requirement for developers needing compliant barcode output in logistics and inventory systems.
// Prompt: Validate that all non‑customer fields conform to the C40 character set before generation.
// Tags: datamatrix, c40, validation, png, barcodegenerator, datamatrixencodemode, aspnet.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode using C40 encoding after validating the input string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates the code text against the C40 charset and creates a PNG barcode if valid.
    /// </summary>
    static void Main()
    {
        // Sample code text representing non‑customer fields
        string codeText = "HELLO WORLD 123!";

        // Validate that the code text conforms to the C40 character set
        if (!IsC40String(codeText))
        {
            Console.WriteLine("Warning: CodeText contains characters not allowed in the C40 charset. Generation skipped.");
            return;
        }

        // Create a DataMatrix barcode generator with C40 encoding mode
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set the DataMatrix encode mode to C40
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.C40;

            // Optionally control image size (using unit members)
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image
            string outputPath = "datamatrix_c40.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }

    // Checks if every character in the string is allowed in the C40 charset
    static bool IsC40String(string text)
    {
        foreach (char ch in text)
        {
            if (!IsC40Char(ch))
                return false;
        }
        return true;
    }

    // Determines whether a single character is part of the C40 charset
    static bool IsC40Char(char ch)
    {
        // Uppercase letters
        if (ch >= 'A' && ch <= 'Z')
            return true;

        // Digits
        if (ch >= '0' && ch <= '9')
            return true;

        // Space
        if (ch == ' ')
            return true;

        // Basic punctuation allowed in C40
        const string punctuation = "!\"#%&'()*+,-./:;<=>?";
        return punctuation.IndexOf(ch) >= 0;
    }
}