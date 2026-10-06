// Title: GS1 Composite Barcode Generation with Separator Validation
// Description: Demonstrates creating a GS1 Composite barcode, handling the required '|' separator in the CodeText, and capturing errors when the separator is missing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It shows how to configure linear and 2‑D components, set encoding options, and use the ThrowExceptionWhenCodeTextIncorrect property to validate CodeText format. Developers working with GS1 barcodes often need to ensure proper separators and handle exceptions during generation.
// Prompt: Implement exception handling for missing ‘|’ separator in CodeText when creating a GS1 Composite barcode.
// Tags: gs1, composite, barcode, generation, validation, exception handling, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode, validates the presence of the required
/// '|' separator in the CodeText, and demonstrates exception handling for invalid input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a valid and an invalid GS1 Composite barcode,
    /// handling any exceptions that arise from missing separators.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory to store generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        Directory.CreateDirectory(outputDir);

        // -------------------- Valid GS1 Composite barcode (contains '|') --------------------
        string validCodeText = "(01)01234567890128|HelloWorld";
        string validPath = Path.Combine(outputDir, "valid_gs1_composite.png");

        try
        {
            // Initialize the barcode generator with GS1 Composite symbology and valid CodeText
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, validCodeText))
            {
                // Configure linear component (GS1 Code128) and 2‑D component (CC-C)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

                // Allow non‑GS1 encoding for demonstration purposes
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                // Set PDF417 specific parameters (used for the 2‑D component)
                generator.Parameters.Barcode.Pdf417.Columns = 30;

                // Enable strict validation of CodeText format
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Save the generated barcode image to file
                generator.Save(validPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Valid barcode saved to: {validPath}");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors during valid barcode generation
            Console.WriteLine($"Unexpected error while generating valid barcode: {ex.Message}");
        }

        // -------------------- Invalid GS1 Composite barcode (missing '|') --------------------
        string invalidCodeText = "(01)01234567890128HelloWorld";
        string invalidPath = Path.Combine(outputDir, "invalid_gs1_composite.png");

        try
        {
            // Initialize the barcode generator with GS1 Composite symbology and invalid CodeText
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, invalidCodeText))
            {
                // Apply the same component and encoding settings as the valid case
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
                generator.Parameters.Barcode.Pdf417.Columns = 30;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to save; this is expected to throw because the '|' separator is missing
                generator.Save(invalidPath, BarCodeImageFormat.Png);
                Console.WriteLine("Unexpectedly succeeded in generating barcode without separator.");
            }
        }
        catch (Exception ex)
        {
            // Expected error handling for missing separator in CodeText
            Console.WriteLine($"Expected error for missing separator: {ex.Message}");
        }
    }
}