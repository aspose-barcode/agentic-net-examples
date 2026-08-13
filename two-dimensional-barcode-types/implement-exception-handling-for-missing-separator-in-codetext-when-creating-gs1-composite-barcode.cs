// Title: GS1 Composite barcode generation with separator validation
// Description: Demonstrates creating a GS1 Composite barcode using Aspose.BarCode, including validation that the CodeText contains the required '|' separator and handling the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to produce combined linear and 2D barcodes. Developers often need to generate composite barcodes for supply‑chain applications, requiring proper formatting and error handling for the CodeText input.
// Prompt: Implement exception handling for missing ‘|’ separator in CodeText when creating a GS1 Composite barcode.
// Tags: barcode, gs1 composite, validation, exception handling, png, aspose.barcode, encode types, generator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates GS1 Composite barcode generation with validation of the CodeText separator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with correct and incorrect CodeText, handling any exceptions.
    /// </summary>
    static void Main()
    {
        // Sample correct GS1 Composite code text (linear|2D)
        string correctCodeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";
        string correctOutput = "gs1_composite_correct.png";

        // Sample incorrect GS1 Composite code text (missing '|')
        string incorrectCodeText = "(01)03212345678906(21)A1B2C3D4E5F6G7H8";
        string incorrectOutput = "gs1_composite_incorrect.png";

        // Attempt to generate barcode with correct code text
        try
        {
            GenerateGs1CompositeBarcode(correctCodeText, correctOutput);
            Console.WriteLine($"Barcode generated successfully: {correctOutput}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate barcode (correct input): {ex.Message}");
        }

        // Attempt to generate barcode with incorrect code text
        try
        {
            GenerateGs1CompositeBarcode(incorrectCodeText, incorrectOutput);
            Console.WriteLine($"Barcode generated successfully: {incorrectOutput}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode (incorrect input): {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a GS1 Composite barcode image.
    /// Throws ArgumentException if the required '|' separator is missing.
    /// </summary>
    /// <param name="codeText">The code text containing linear and 2D parts separated by '|'.</param>
    /// <param name="outputPath">File path to save the generated barcode image.</param>
    static void GenerateGs1CompositeBarcode(string codeText, string outputPath)
    {
        // Validate presence of the '|' separator required for GS1 Composite barcodes
        if (string.IsNullOrEmpty(codeText) || !codeText.Contains("|"))
        {
            throw new ArgumentException("CodeText must contain a '|' separator between linear and 2D components for GS1 Composite barcodes.");
        }

        // Create the barcode generator with GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure linear and 2D component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image to the specified path (default PNG format)
            generator.Save(outputPath);
        }
    }
}