// Title: Numeric Validation for 2‑State Barcode Generation
// Description: Demonstrates how to validate that the input for a 2‑state barcode contains only numeric characters before generating the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and related parameter classes to create 1D barcodes. Typical scenarios include validating user input, handling exceptions for invalid data, and configuring visual properties. Developers often need to ensure data integrity before invoking the generation API, making this pattern a common prerequisite in barcode‑related workflows.
// Prompt: Validate numeric input for a 2‑state barcode generator and raise an exception for non‑numeric characters.
// Tags: barcode, symbology, validation, code128, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates numeric input and generates a 2‑state barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Validates the code text, generates a barcode, and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Sample numeric input for a 2‑state barcode.
        string codeText = "1234567890";

        // Ensure the input consists only of digits; otherwise throw an Aspose‑specific exception.
        if (!IsNumeric(codeText))
        {
            throw new InvalidCodeException("The code text contains non‑numeric characters.");
        }

        // Create a barcode generator for the Code128 symbology.
        // The generator implements IDisposable, so we use a using block to guarantee proper resource cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure visual appearance: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Define the output file path and save the generated image.
            string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Inform the user that the operation succeeded.
            Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
        }
    }

    // Helper method that returns true only if every character in the string is a digit (0‑9).
    private static bool IsNumeric(string text)
    {
        foreach (char c in text)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}