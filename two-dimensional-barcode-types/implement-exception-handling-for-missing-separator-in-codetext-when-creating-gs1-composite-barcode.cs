// Title: GS1 Composite Barcode Generation with Separator Validation
// Description: Demonstrates creating a GS1 Composite barcode using Aspose.BarCode, showing how the library throws an exception when the required ‘|’ separator is missing in the CodeText and how to handle it.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It illustrates the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to configure linear and 2‑D components, a common task when generating composite barcodes for retail and logistics applications. Developers often need to validate CodeText format and handle errors gracefully.
// Prompt: Implement exception handling for missing ‘|’ separator in CodeText when creating a GS1 Composite barcode.
// Tags: gs1, composite, barcode, generation, exception handling, validation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates GS1 Composite barcode generation and handling of missing separator errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates both an invalid and a valid GS1 Composite barcode, handling exceptions accordingly.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Example 1: Attempt to generate a barcode with missing '|' separator
        // This should trigger an exception because the CodeText format is invalid.
        // ------------------------------------------------------------
        string invalidCodeText = "(01)01234567890123HelloWorld";
        string invalidPath = Path.Combine(outputDir, "InvalidGS1Composite.png");
        try
        {
            // Initialize the generator with GS1 Composite symbology and the invalid CodeText
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, invalidCodeText))
            {
                // Configure generator to throw on incorrect CodeText
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                // Set linear component type to GS1 Code128
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                // Set 2D component type to CC-C
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                // Adjust PDF417 specific settings
                generator.Parameters.Barcode.Pdf417.Columns = 30;
                // Attempt to save the barcode image (expected to fail)
                generator.Save(invalidPath, BarCodeImageFormat.Png);
                Console.WriteLine("Generated barcode (unexpectedly) at: " + invalidPath);
            }
        }
        catch (Exception ex)
        {
            // Expected path: capture and report the validation exception
            Console.WriteLine("Expected exception for missing separator: " + ex.Message);
        }

        // ------------------------------------------------------------
        // Example 2: Generate a barcode with the correct '|' separator
        // This should succeed and produce a valid barcode image.
        // ------------------------------------------------------------
        string validCodeText = "(01)01234567890123|HelloWorld";
        string validPath = Path.Combine(outputDir, "ValidGS1Composite.png");
        try
        {
            // Initialize the generator with valid CodeText
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, validCodeText))
            {
                // Enable exception throwing for incorrect CodeText (won't trigger here)
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                // Configure linear and 2D components as before
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                generator.Parameters.Barcode.Pdf417.Columns = 30;
                // Save the valid barcode image
                generator.Save(validPath, BarCodeImageFormat.Png);
                Console.WriteLine("Successfully generated barcode at: " + validPath);
            }
        }
        catch (Exception ex)
        {
            // Unexpected errors are reported here
            Console.WriteLine("Unexpected exception: " + ex.Message);
        }
    }
}