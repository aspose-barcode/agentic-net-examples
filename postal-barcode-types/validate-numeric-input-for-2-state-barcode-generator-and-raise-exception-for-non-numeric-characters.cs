// Title: Numeric Validation for 2‑State Barcode Generation
// Description: Demonstrates how to ensure a barcode's input contains only digits before generating a 2‑state barcode with Aspose.BarCode, and how to handle invalid input via exceptions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and generator parameters. Typical scenarios include creating Code128 barcodes for inventory or shipping labels where numeric-only data is required. Developers often need to pre‑validate input, enable strict exception handling, and save the resulting image.
// Prompt: Validate numeric input for a 2‑state barcode generator and raise an exception for non‑numeric characters.
// Tags: barcode, validation, code128, exception, aspose.barcode, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates numeric validation for a 2‑state barcode generator using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that processes sample inputs, generates barcodes, and handles validation errors.
    /// </summary>
    static void Main()
    {
        // Sample inputs: one valid numeric string and one invalid string containing letters.
        string[] samples = { "1234567890", "12A4567890" };

        // Iterate over each sample, attempting to generate a barcode.
        foreach (var text in samples)
        {
            Console.WriteLine($"Processing input: \"{text}\"");
            try
            {
                // Validate the input and generate the barcode if valid.
                ValidateAndGenerate(text);
                Console.WriteLine("Barcode generated successfully.\n");
            }
            // Catch specific exception thrown by the barcode generator when the code text is incorrect.
            catch (InvalidCodeException ex)
            {
                Console.WriteLine($"InvalidCodeException: {ex.Message}\n");
            }
            // Catch argument validation errors from our pre‑validation logic.
            catch (ArgumentException ex)
            {
                Console.WriteLine($"ArgumentException: {ex.Message}\n");
            }
        }
    }

    // Validates that the input contains only digits and generates a barcode.
    // Throws InvalidCodeException if the barcode generator detects non‑numeric characters.
    static void ValidateAndGenerate(string codeText)
    {
        // Ensure the input is not null, empty, or whitespace.
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text cannot be null or empty.");

        // Optional pre‑validation: verify that every character is a digit.
        foreach (char c in codeText)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("Code text must contain only numeric characters.");
        }

        // Use Code128 as an example 2‑state barcode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable strict exception throwing when the code text does not meet symbology requirements.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Save the generated barcode image to a PNG file.
            string fileName = $"barcode_{codeText}.png";
            generator.Save(fileName, BarCodeImageFormat.Png);
        }
    }
}