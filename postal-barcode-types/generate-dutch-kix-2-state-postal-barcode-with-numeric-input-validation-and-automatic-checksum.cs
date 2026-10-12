// Title: Generate Dutch KIX 2‑state postal barcode with checksum
// Description: Demonstrates creating a Dutch KIX 2‑state postal barcode from a numeric string, validating input, calculating the checksum, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DutchKIX. It shows typical steps such as input validation, checksum calculation, setting barcode parameters, and exporting to an image file—common tasks for developers implementing postal barcode solutions.
// Prompt: Generate a Dutch KIX 2‑state postal barcode with numeric input validation and automatic checksum.
// Tags: dutch,kix,barcode,generation,checksum,validation,image,png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Dutch KIX 2‑state postal barcode with numeric validation and automatic checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates numeric input, appends checksum, generates barcode, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Input string to be encoded; must be numeric.
        string input = "123456";

        try
        {
            // Validate that the input contains only digits.
            string numeric = ValidateNumericInput(input);

            // Calculate and append the checksum digit.
            string codeText = AppendChecksum(numeric);

            // Determine a temporary file path for the output image.
            string outputPath = Path.Combine(Path.GetTempPath(), "DutchKIX.png");

            // Create a barcode generator for the Dutch KIX symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
            {
                // Set the X-dimension (module width) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Dutch KIX barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any validation or generation errors.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Ensures the provided text consists solely of numeric digits.
    /// </summary>
    /// <param name="text">The input string to validate.</param>
    /// <returns>The original string if validation succeeds.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or contains non‑digit characters.</exception>
    static string ValidateNumericInput(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Input cannot be null or empty.");

        foreach (char c in text)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("Input must contain only numeric digits.");
        }

        return text;
    }

    /// <summary>
    /// Calculates a simple modulo‑10 checksum and appends it to the numeric string.
    /// </summary>
    /// <param name="numeric">The numeric string without checksum.</param>
    /// <returns>The numeric string with the checksum digit appended.</returns>
    static string AppendChecksum(string numeric)
    {
        int sum = 0;

        // Sum all digit values.
        foreach (char c in numeric)
        {
            sum += c - '0';
        }

        // Modulo‑10 checksum.
        int checksum = sum % 10;

        // Append checksum to the original numeric string.
        return numeric + checksum.ToString();
    }
}