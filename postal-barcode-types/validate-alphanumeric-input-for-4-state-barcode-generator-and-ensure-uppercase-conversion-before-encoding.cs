// Title: Validate and Encode Alphanumeric Input for 4‑State Mailmark Barcode
// Description: Demonstrates how to validate an alphanumeric string, convert it to uppercase, and generate a 4‑state Mailmark barcode saved as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of the ComplexBarcodeGenerator and MailmarkCodetext classes to create 4‑state Mailmark symbols. Developers often need to generate specialized postal barcodes for mail processing, requiring precise data formatting and validation. The snippet shows typical steps such as input validation, codetext construction, and image export, which are common tasks when working with postal symbologies.
// Prompt: Validate alphanumeric input for a 4‑state barcode generator and ensure uppercase conversion before encoding.
// Tags: mailmark, 4-state, validation, encoding, png, complexbarcodegenerator, mailmarkcodetext

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that validates alphanumeric input, converts it to uppercase,
/// and generates a 4‑state Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates that the input contains only letters and digits.
    /// Throws <see cref="ArgumentException"/> if validation fails.
    /// Returns the input converted to uppercase.
    /// </summary>
    /// <param name="input">Raw input string to validate.</param>
    /// <returns>Upper‑cased, validated input string.</returns>
    static string ValidateAndUppercase(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input cannot be null or empty.");

        foreach (char c in input)
        {
            if (!char.IsLetterOrDigit(c))
                throw new ArgumentException("Input must be alphanumeric (letters and digits only).");
        }

        return input.ToUpperInvariant();
    }

    /// <summary>
    /// Entry point of the program. Performs validation, constructs the Mailmark codetext,
    /// generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample alphanumeric input; in a real scenario this could come from arguments or other sources.
        string rawInput = "ef61ah8t";

        string validatedInput;
        try
        {
            // Validate input and convert to uppercase.
            validatedInput = ValidateAndUppercase(rawInput);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return;
        }

        // DestinationPostCodePlusDPS for Mailmark requires a trailing space.
        if (!validatedInput.EndsWith(" "))
            validatedInput += " ";

        // Construct Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = validatedInput
        };

        // Generate the 4‑state Mailmark barcode and save it as PNG.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save("mailmark.png");
        }

        Console.WriteLine("Mailmark barcode generated successfully as 'mailmark.png'.");
    }
}