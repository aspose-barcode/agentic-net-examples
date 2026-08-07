// Title: Validate Mailmark field lengths and values
// Description: Demonstrates how to validate Mailmark data fields against their defined capacity before generating a Mailmark barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating validation of Mailmark codetext fields. It uses the MailmarkCodetext and ComplexBarcodeGenerator classes to ensure data conforms to the Mailmark specification (field lengths, numeric ranges) before barcode creation. Developers creating Mailmark barcodes commonly need to verify input data to avoid generation errors and to meet postal service requirements.
/// Prompt: Validate that customer data length does not exceed capacity for the selected Mailmark type.
/// Tags: mailmark, validation, barcode, aspose.barcode, complexbarcode, generation, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of validating Mailmark data fields and generating a Mailmark barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates Mailmark fields against their defined capacity and value ranges.
    /// Throws <see cref="ArgumentException"/> when a field does not meet the specification.
    /// </summary>
    /// <param name="mailmark">The <see cref="MailmarkCodetext"/> instance to validate.</param>
    static void ValidateMailmark(MailmarkCodetext mailmark)
    {
        // Class must be a single character string.
        if (mailmark.Class == null || mailmark.Class.Length != 1)
            throw new ArgumentException("Class must be a single character.");

        // DestinationPostCodePlusDPS must be exactly 9 characters (including trailing spaces).
        if (mailmark.DestinationPostCodePlusDPS == null || mailmark.DestinationPostCodePlusDPS.Length != 9)
            throw new ArgumentException("DestinationPostCodePlusDPS must be exactly 9 characters (including trailing spaces).");

        // ItemID must be between 0 and 99,999,999 (max 8 digits).
        if (mailmark.ItemID < 0 || mailmark.ItemID > 99999999)
            throw new ArgumentException("ItemID exceeds the maximum allowed value of 99,999,999.");

        // SupplychainID must be positive and not exceed 999 (covers both C and L types).
        if (mailmark.SupplychainID < 0 || mailmark.SupplychainID > 999)
            throw new ArgumentException("SupplychainID exceeds the maximum allowed value of 999.");

        // VersionID is typically a single digit; enforce 0‑9 range.
        if (mailmark.VersionID < 0 || mailmark.VersionID > 9)
            throw new ArgumentException("VersionID must be a single digit (0‑9).");
    }

    /// <summary>
    /// Entry point of the program. Constructs a Mailmark codetext, validates it, and generates a barcode image.
    /// </summary>
    static void Main()
    {
        // Construct a Mailmark 4‑state codetext with sample data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // 9 characters, trailing space required
        };

        try
        {
            // Perform validation before barcode generation.
            ValidateMailmark(mailmark);

            // Generate the Mailmark barcode using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save("mailmark.png");
                Console.WriteLine("Mailmark barcode generated successfully: mailmark.png");
            }
        }
        catch (ArgumentException ex)
        {
            // Output validation errors.
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors gracefully.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}