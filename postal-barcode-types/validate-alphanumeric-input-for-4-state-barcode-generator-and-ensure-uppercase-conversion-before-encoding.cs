using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of an RM4SCC barcode from an alphanumeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Validates input, generates a barcode, and reports the saved file location.
    /// </summary>
    static void Main()
    {
        // Sample alphanumeric input
        string input = "a1b2c3";

        // Validate the input and convert it to uppercase
        string validated = ValidateAndUppercase(input);

        // Define the output file path for the generated barcode image
        string outputPath = "rm4scc.png";

        // Generate the RM4SCC barcode using the validated text
        GenerateRm4SccBarcode(validated, outputPath);

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }

    /// <summary>
    /// Validates that the provided text is non‑empty, alphanumeric, and returns it in uppercase.
    /// </summary>
    /// <param name="text">The input string to validate.</param>
    /// <returns>The uppercase version of the validated input.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or contains non‑alphanumeric characters.</exception>
    static string ValidateAndUppercase(string text)
    {
        // Ensure the input is not null, empty, or whitespace
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Input cannot be null or empty.");

        // Verify each character is a letter or digit
        foreach (char c in text)
        {
            if (!char.IsLetterOrDigit(c))
                throw new ArgumentException("Input must be alphanumeric.");
        }

        // Convert to uppercase using invariant culture for consistency
        return text.ToUpperInvariant();
    }

    /// <summary>
    /// Generates an RM4SCC barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode (must be uppercase alphanumeric).</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateRm4SccBarcode(string codeText, string outputPath)
    {
        // Ensure the output directory exists before saving the file
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create a barcode generator for the RM4SCC symbology with the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Throw an exception if the code text is not suitable for the selected symbology
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }
    }
}