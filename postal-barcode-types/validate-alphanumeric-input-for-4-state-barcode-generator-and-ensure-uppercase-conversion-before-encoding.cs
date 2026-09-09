// Title: Generate RM4SCC Barcode with Input Validation and Uppercase Conversion
// Description: Demonstrates how to validate an alphanumeric string, convert it to uppercase, and generate a 4‑state RM4SCC barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.RM4SCC. It shows typical steps such as input validation, parameter configuration, and saving the barcode to an image file. Developers working with 4‑state barcodes often need to enforce character sets and visual settings, making this a useful reference for similar implementations.
// Prompt: Validate alphanumeric input for a 4‑state barcode generator and ensure uppercase conversion before encoding.
// Tags: barcode, generation, validation, rm4scc, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates validation and generation of an RM4SCC (4‑state) barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Validates sample input, generates the barcode, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Sample input (could be replaced with command‑line args)
        string input = "a1b2c3";

        try
        {
            // Validate and prepare the input for barcode encoding
            string validated = ValidateAndPrepareInput(input);
            // Generate the barcode image and obtain its file path
            string outputPath = GenerateRm4sccBarcode(validated);
            Console.WriteLine($"Barcode generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any validation or generation errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that the text is non‑empty, alphanumeric, and converts it to uppercase as required by RM4SCC.
    /// </summary>
    /// <param name="text">The raw input string.</param>
    /// <returns>Uppercase alphanumeric string ready for encoding.</returns>
    static string ValidateAndPrepareInput(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Input text cannot be null or empty.");

        // Allow only letters A‑Z and digits 0‑9
        if (!Regex.IsMatch(text, @"^[A-Za-z0-9]+$"))
            throw new ArgumentException("Input must be alphanumeric (letters and digits only).");

        // Convert to uppercase as required by 4‑state barcodes
        return text.ToUpperInvariant();
    }

    /// <summary>
    /// Generates an RM4SCC barcode image from the provided code text and saves it as a PNG file.
    /// </summary>
    /// <param name="codeText">Validated, uppercase alphanumeric string.</param>
    /// <returns>Full path to the generated PNG file.</returns>
    static string GenerateRm4sccBarcode(string codeText)
    {
        // Ensure output directory exists (system temporary folder)
        string tempDir = Path.GetTempPath();
        string fileName = "RM4SCC_" + Guid.NewGuid().ToString("N") + ".png";
        string fullPath = Path.Combine(tempDir, fileName);

        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Optional visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Save as PNG
            generator.Save(fullPath, BarCodeImageFormat.Png);
        }

        return fullPath;
    }
}