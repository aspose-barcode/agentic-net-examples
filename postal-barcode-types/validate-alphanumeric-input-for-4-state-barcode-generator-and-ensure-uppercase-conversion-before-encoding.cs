// Title: Validate and Encode Alphanumeric Input for RM4SCC Barcode
// Description: Demonstrates how to validate an alphanumeric string, convert it to uppercase, and generate a 4‑state RM4SCC barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating common tasks such as input validation, text preprocessing, and barcode image creation with the BarcodeGenerator class. Developers working with 4‑state symbologies like RM4SCC often need to ensure data conforms to alphanumeric rules and proper case before encoding. The snippet shows typical usage of EncodeTypes, barcode parameters, and saving the result as PNG.
// Prompt: Validate alphanumeric input for a 4‑state barcode generator and ensure uppercase conversion before encoding.
// Tags: barcode, rm4scc, validation, alphanumeric, uppercase, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program demonstrating validation and generation of an RM4SCC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates input, converts to uppercase, generates barcode, and reports status.
    /// </summary>
    static void Main()
    {
        // Sample input that may contain mixed case letters and digits
        string input = "Abc123XYZ";

        try
        {
            // Validate the input and convert it to uppercase
            string validated = ValidateAndConvert(input);

            // Generate the barcode image using the validated text
            GenerateBarcode(validated);

            Console.WriteLine("Barcode generated successfully.");
        }
        catch (Exception ex)
        {
            // Output any validation or generation errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Ensures the provided text is non‑empty, alphanumeric, and returns it in uppercase.
    /// </summary>
    /// <param name="text">The raw input string to validate.</param>
    /// <returns>Uppercase version of the validated input.</returns>
    static string ValidateAndConvert(string text)
    {
        // Check for null or empty strings
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Input cannot be null or empty.");

        // Verify each character is a letter or digit
        foreach (char c in text)
        {
            if (!char.IsLetterOrDigit(c))
                throw new ArgumentException("Input must be alphanumeric (letters and digits only).");
        }

        // Convert to uppercase using invariant culture
        return text.ToUpperInvariant();
    }

    /// <summary>
    /// Generates an RM4SCC barcode image from the supplied text and saves it as PNG.
    /// </summary>
    /// <param name="codeText">Validated, uppercase text to encode.</param>
    static void GenerateBarcode(string codeText)
    {
        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "rm4scc.png");

        // Create a barcode generator for the RM4SCC symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}