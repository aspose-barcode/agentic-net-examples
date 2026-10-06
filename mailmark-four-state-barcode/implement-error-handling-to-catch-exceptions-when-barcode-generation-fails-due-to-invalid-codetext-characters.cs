// Title: Generate ITF6 barcode with error handling for invalid codetext
// Description: Demonstrates generating an ITF6 barcode using Aspose.BarCode and handling errors when the provided codetext does not meet the symbology requirements.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure the BarcodeGenerator, set validation options, and catch exceptions for invalid input. It showcases key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers use to create barcodes in various formats and need robust error handling for incorrect codetext.
// Prompt: Implement error handling to catch exceptions when barcode generation fails due to invalid Codetext characters.
// Tags: itf6, barcode, error-handling, generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates ITF6 barcode generation with validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an ITF6 barcode, validates codetext, and handles generation errors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "invalid_itf6.png");

        // ITF6 requires a 6‑digit numeric codetext; "12" is intentionally invalid to trigger an error
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.ITF6, "12"))
        {
            // Instruct the generator to throw an exception when the codetext does not satisfy the symbology rules
            gen.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Attempt to save the barcode image; this will fail due to invalid codetext
                gen.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode generated successfully: " + outputPath);
            }
            catch (Exception ex)
            {
                // Capture and display the error message for debugging or logging purposes
                Console.WriteLine("Barcode generation failed: " + ex.Message);
            }
        }
    }
}