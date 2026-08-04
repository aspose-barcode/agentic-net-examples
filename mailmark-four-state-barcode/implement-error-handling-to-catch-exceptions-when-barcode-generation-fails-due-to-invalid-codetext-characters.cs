// Title: Barcode Generation with Error Handling for Invalid Codetext
// Description: Demonstrates generating a Code39 barcode and handling errors when the codetext contains characters not allowed by the symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set generation parameters, and implement robust exception handling. Developers often need to validate codetext before creating barcodes for printing, labeling, or inventory systems; this snippet illustrates typical use cases such as catching InvalidCodeException and other BarCodeException types.
// Prompt: Implement error handling to catch exceptions when barcode generation fails due to invalid Codetext characters.
// Tags: barcode, code39, error handling, exception, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code39 barcode and demonstrates error handling
/// for invalid codetext characters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it to a PNG file,
    /// and catches any exceptions related to invalid codetext or other generation errors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "code39.png";

        // Create a BarcodeGenerator for Code39 with an intentionally invalid character '@'.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC@123"))
        {
            // Configure the generator to throw an exception when the codetext is incorrect.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Attempt to save the barcode image to the specified path in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully and saved to '{outputPath}'.");
            }
            catch (InvalidCodeException ex)
            {
                // Handles invalid characters in the codetext specific to the chosen symbology.
                Console.WriteLine($"InvalidCodeException: {ex.Message}");
            }
            catch (BarCodeException ex)
            {
                // Handles other barcode generation errors (e.g., configuration issues).
                Console.WriteLine($"BarCodeException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handles any unexpected errors that may occur.
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}