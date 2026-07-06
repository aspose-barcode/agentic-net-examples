// Title: Barcode generation with error handling for invalid Code39 characters
// Description: Demonstrates how to generate a Code39 barcode and catch exceptions when the codetext contains characters not allowed by the symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and exception handling for invalid codetext. Developers often need to validate input before creating barcodes; this snippet shows how to enable strict validation and handle errors gracefully, a common requirement when integrating barcode creation into applications.
// Prompt: Implement error handling to catch exceptions when barcode generation fails due to invalid Codetext characters.
// Tags: code39, barcode generation, error handling, invalidcodetext, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with explicit error handling for invalid codetext characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code39 barcode and handles possible generation errors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "code39.png";

        try
        {
            // Create a BarcodeGenerator with an intentionally invalid Code39 text ("@" is not allowed).
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC@123"))
            {
                // Enable throwing an exception when the codetext does not conform to the symbology rules.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to save the barcode image to the specified path.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode successfully saved to '{outputPath}'.");
            }
        }
        // Catch specific exception for invalid characters in the codetext.
        catch (InvalidCodeException ex)
        {
            Console.WriteLine($"InvalidCodeException: {ex.Message}");
        }
        // Catch other barcode generation related exceptions.
        catch (BarCodeException ex)
        {
            Console.WriteLine($"BarCodeException: {ex.Message}");
        }
        // Catch any unexpected exceptions.
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}