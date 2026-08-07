// Title: C40 Character Set Validation for Non‑Customer Fields Before Barcode Generation
// Description: Demonstrates how to validate that all non‑customer data fields contain only characters allowed by the C40 encoding set prior to generating a barcode.
// Category-Description: This example belongs to the Aspose.BarCode data validation category, illustrating the use of Aspose.BarCode.Generation.BarcodeGenerator and related classes to ensure input data complies with specific character sets (C40) before barcode creation. Developers often need to pre‑validate fields such as product codes or descriptions to avoid encoding errors. The snippet shows typical validation logic, field filtering, and barcode generation with Code128.
// Prompt: Validate that all non‑customer fields conform to the C40 character set before generation.
// Tags: barcode symbology, validation, c40, code128, png, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates validation of non‑customer fields against the C40 character set and generates a Code128 barcode.
/// </summary>
class Program
{
    // Allowed characters for C40 encoding: digits, uppercase letters, space and common punctuation.
    private static readonly HashSet<char> C40AllowedChars = new HashSet<char>
    {
        ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
        ':', ';', '<', '=', '>', '?'
    };

    /// <summary>
    /// Entry point that validates fields, ensures output directory, and creates a barcode image.
    /// </summary>
    static void Main()
    {
        // Sample data fields. Fields named "CustomerName" are considered customer fields and are excluded from validation.
        var fields = new Dictionary<string, string>
        {
            { "CustomerName", "Acme Corp" },          // Customer field – skip validation
            { "ProductCode", "ABC123" },              // Non‑customer field – must be C40 compliant
            { "Description", "NEW PRODUCT! RELEASE" } // Non‑customer field – must be C40 compliant
        };

        // Validate non‑customer fields.
        foreach (var kvp in fields)
        {
            if (IsCustomerField(kvp.Key))
                continue; // Skip customer fields.

            if (!IsC40Compliant(kvp.Value))
            {
                Console.WriteLine($"Field \"{kvp.Key}\" contains characters not allowed in C40 encoding.");
                // Abort further processing.
                return;
            }
        }

        // All validations passed – generate a barcode.
        const string barcodeText = "VALIDDATA";
        const string outputPath = "barcode.png";

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Example of setting a barcode property (XDimension) correctly.
            generator.Parameters.Barcode.XDimension.Point = 2.5f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated successfully at \"{outputPath}\".");
    }

    // Determines whether a field name represents a customer field.
    private static bool IsCustomerField(string fieldName)
    {
        // Simple rule: field name contains the word "Customer".
        return fieldName.IndexOf("Customer", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    // Checks if a string contains only characters allowed in C40 encoding.
    private static bool IsC40Compliant(string text)
    {
        foreach (char ch in text)
        {
            if (char.IsDigit(ch) || (ch >= 'A' && ch <= 'Z') || C40AllowedChars.Contains(ch))
                continue;

            // Lowercase letters are not part of C40; they must be converted or cause failure.
            return false;
        }
        return true;
    }
}