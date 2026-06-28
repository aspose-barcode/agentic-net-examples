using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Code128 barcode after validating input against the C40 character set.
/// </summary>
class Program
{
    /// <summary>
    /// Determines whether the supplied text contains only characters allowed in the C40 character set (basic subset).
    /// </summary>
    /// <param name="text">The text to validate.</param>
    /// <returns>True if the text is null, empty, or contains only valid C40 characters; otherwise, false.</returns>
    private static bool IsValidC40(string text)
    {
        // Empty or null strings are considered valid.
        if (string.IsNullOrEmpty(text))
            return true;

        // Examine each character in the string.
        foreach (char ch in text)
        {
            // Digits 0‑9 are allowed.
            if (ch >= '0' && ch <= '9')
                continue;

            // Uppercase letters A‑Z are allowed.
            if (ch >= 'A' && ch <= 'Z')
                continue;

            // Space character is allowed.
            if (ch == ' ')
                continue;

            // Allow a set of common punctuation characters.
            switch (ch)
            {
                case '-':
                case '/':
                case '.':
                case ',':
                case '+':
                case '*':
                case ':':
                case ';':
                case '?':
                case '!':
                    continue;
                default:
                    // Any other character is invalid for C40.
                    return false;
            }
        }

        // All characters passed validation.
        return true;
    }

    /// <summary>
    /// Entry point of the application. Validates sample data and generates a Code128 barcode if validation succeeds.
    /// </summary>
    static void Main()
    {
        // Sample non‑customer data that needs C40 validation.
        string nonCustomerData = "ABC-123/XYZ";

        // Abort if the data contains characters outside the allowed C40 set.
        if (!IsValidC40(nonCustomerData))
        {
            Console.WriteLine("Non‑customer data contains characters outside the C40 set. Generation aborted.");
            return;
        }

        // Construct the barcode's codetext, incorporating the validated data.
        string codeText = $"NONCUST:{nonCustomerData}";

        // Generate a simple Code128 barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set barcode height (in points) and enable anti‑aliasing for better visual quality.
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.UseAntiAlias = true;

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Save the generated barcode image to disk.
            generator.Save(outputPath);
            Console.WriteLine($"Barcode generated and saved to: {outputPath}");
        }
    }
}