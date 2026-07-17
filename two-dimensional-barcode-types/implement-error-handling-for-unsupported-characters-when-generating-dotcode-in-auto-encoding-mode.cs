// Title: DotCode Barcode Generation with Auto Encoding and Error Handling
// Description: Demonstrates generating a DotCode barcode in Auto encoding mode while handling unsupported characters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes using the BarcodeGenerator class. It focuses on DotCode symbology, Auto encoding mode, and error handling for unsupported characters—common tasks for developers integrating barcode creation into .NET applications.
// Prompt: Implement error handling for unsupported characters when generating DotCode in Auto encoding mode.
// Tags: dotcode, barcode, generation, error handling, aspose.barcode, encoding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DotCode barcode using Auto encoding mode
/// and demonstrates handling of unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DotCode barcode, saves it to a file, and handles any encoding errors.
    /// </summary>
    static void Main()
    {
        // Input text containing characters that may not be supported by the selected encoding
        string codeText = "犬Right狗";

        // Output file name for the generated barcode image
        string outputFile = "dotcode.png";

        // Initialize the BarcodeGenerator with DotCode symbology and the input text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Configure DotCode parameters:
            // - Set encoding mode to Auto so the library chooses the best encoding
            // - Specify ISO-8859-1 as the ECI encoding for compatibility
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_1;

            try
            {
                // Attempt to save the generated barcode image to the specified file
                generator.Save(outputFile);
                Console.WriteLine($"Barcode successfully saved to '{outputFile}'.");
            }
            catch (InvalidCodeException ex)
            {
                // Handle cases where the input contains characters unsupported by the chosen encoding
                Console.WriteLine($"Error: Unsupported characters for the selected encoding. {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors during barcode generation
                Console.WriteLine($"Barcode generation failed: {ex.Message}");
            }
        }
    }
}