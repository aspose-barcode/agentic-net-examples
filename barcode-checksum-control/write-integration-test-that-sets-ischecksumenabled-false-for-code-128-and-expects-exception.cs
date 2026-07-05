// Title: Integration test for Code128 checksum disabled exception
// Description: Demonstrates disabling checksum for Code 128 and verifies that an exception is thrown when the input text contains an invalid character.
// Prompt: Write an integration test that sets IsChecksumEnabled false for Code 128 and expects an exception.
// Tags: barcode symbology, operation, output format, aspose.barcode generation

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that attempts to generate a Code 128 barcode with checksum disabled
/// and expects an exception due to invalid input text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the integration test scenario.
    /// </summary>
    static void Main()
    {
        // Define an invalid Code128 string (contains a null control character)
        string invalidCode = "ABC\u0000DEF";

        try
        {
            // Create a barcode generator for Code128 with the invalid text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, invalidCode))
            {
                // Disable checksum generation for the barcode
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Configure the generator to throw an exception when the code text is invalid
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to generate and save the barcode image (should trigger an exception)
                generator.Save("output.png");

                // If no exception occurs, inform the user (this line should not be reached)
                Console.WriteLine("No exception was thrown.");
            }
        }
        catch (Exception ex)
        {
            // Expected path: capture and display the exception details
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
    }
}