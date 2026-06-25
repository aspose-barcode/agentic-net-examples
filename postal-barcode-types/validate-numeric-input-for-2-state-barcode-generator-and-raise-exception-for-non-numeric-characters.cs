using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a OneCode (2‑state) barcode after validating that the input
/// consists solely of numeric characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Validates the sample code text, generates a OneCode barcode,
    /// and handles any validation or generation errors.
    /// </summary>
    static void Main()
    {
        // Sample code text – replace with any value to test validation
        string codeText = "1234567890";

        try
        {
            // Validate that the code text contains only digits
            ValidateNumeric(codeText);

            // Create a barcode generator for the OneCode symbology using the validated text
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
            {
                // Configure the generator to throw an exception if the code text is invalid
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Save the generated barcode image to a PNG file
                generator.Save("OneCode.png");

                // Inform the user that the barcode was generated successfully
                Console.WriteLine("Barcode generated successfully: OneCode.png");
            }
        }
        // Handle validation errors (e.g., non‑numeric characters)
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Input validation error: {ex.Message}");
        }
        // Handle any other errors that may occur during barcode generation
        catch (Exception ex)
        {
            Console.WriteLine($"Barcode generation error: {ex.Message}");
        }
    }

    // Validates that the provided string contains only numeric characters.
    // Throws ArgumentException if any non‑numeric character is found.
    static void ValidateNumeric(string value)
    {
        // Ensure the input is not null or empty
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Code text cannot be null or empty.");

        // Check each character to confirm it is a digit
        foreach (char c in value)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException("Code text must contain only numeric characters.");
        }
    }
}