using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string outputFile = "code39_invalid.png";

        // Create a BarcodeGenerator for Code39 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
        {
            // Enable exception throwing for incorrect codetext
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Set an invalid codetext (exclamation mark is not allowed in Code39)
            generator.CodeText = "ABC!";

            try
            {
                // Attempt to generate and save the barcode
                generator.Save(outputFile);
                Console.WriteLine($"Barcode saved successfully to '{outputFile}'.");
            }
            catch (InvalidCodeException ex)
            {
                // Handle invalid codetext characters
                Console.WriteLine("Error: Invalid characters in the codetext.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            catch (BarCodeException ex)
            {
                // Handle other barcode generation errors
                Console.WriteLine("Barcode generation failed.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                Console.WriteLine("An unexpected error occurred.");
                Console.WriteLine($"Details: {ex.Message}");
            }
        }
    }
}