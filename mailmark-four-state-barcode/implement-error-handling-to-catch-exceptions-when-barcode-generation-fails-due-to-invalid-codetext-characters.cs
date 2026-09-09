// Title: Barcode Generation with Error Handling for Invalid CodeText
// Description: Demonstrates generating barcodes using Aspose.BarCode and handling exceptions when the provided CodeText does not meet the symbology requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with different EncodeTypes, configure parameters such as ThrowExceptionWhenCodeTextIncorrect, and implement try‑catch blocks to capture validation errors. Developers working with barcode creation, especially when validating input data for symbologies like Code128 and ITF6, can refer to this snippet for best practices in error handling and output image saving.
// Prompt: Implement error handling to catch exceptions when barcode generation fails due to invalid Codetext characters.
// Tags: barcode, code128, itf6, error handling, exception, generation, aspnet, aspose.barcode, png, codetext validation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and error handling using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a valid barcode and attempts to generate an invalid one,
    /// catching any exceptions caused by incorrect CodeText.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Example 1: Valid CodeText (should generate without exception)
        string validPath = Path.Combine(outputFolder, "valid.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "VALID123"))
        {
            // No need to change ThrowExceptionWhenCodeTextIncorrect for valid text
            gen.Save(validPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated valid barcode: {validPath}");
        }

        // Example 2: Invalid CodeText with exception enabled
        string invalidPath = Path.Combine(outputFolder, "invalid.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.ITF6, "12")) // ITF6 expects 6 digits
        {
            // Enable exception throwing for incorrect CodeText
            gen.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
            try
            {
                // This will throw because the CodeText is invalid for ITF6
                gen.Save(invalidPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated invalid barcode (unexpected): {invalidPath}");
            }
            catch (Exception ex)
            {
                // Capture and display the validation error
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }

        // Clean up: optionally delete the temporary folder (commented out to keep files for inspection)
        // Directory.Delete(outputFolder, true);
    }
}