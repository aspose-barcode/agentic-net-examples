// Title: GS1 Composite Barcode Generation with Separator Validation
// Description: Demonstrates creating a GS1 Composite barcode, showing correct handling of the required '|' separator in the CodeText and how to catch exceptions when it is missing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It illustrates using the BarcodeGenerator class with EncodeTypes.GS1CompositeBar, configuring linear and 2‑D components, and enabling ThrowExceptionWhenCodeTextIncorrect to validate CodeText format. Developers working with GS1 barcodes often need to ensure proper separator placement for composite symbols, and this snippet shows typical setup and error handling.
// Prompt: Implement exception handling for missing ‘|’ separator in CodeText when creating a GS1 Composite barcode.
// Tags: gs1 composite, barcode generation, exception handling, code text validation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating GS1 Composite barcodes and handling missing separator errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a correct barcode and attempts an incorrect one, catching exceptions.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample CodeText values: one with the required '|' separator and one without
        string correctCodeText = "(01)01234567890128|HelloWorld";
        string missingSeparatorCodeText = "(01)01234567890128HelloWorld";

        // ------------------------------------------------------------
        // Generate barcode with correct separator
        // ------------------------------------------------------------
        string correctPath = Path.Combine(outputDir, "GS1Composite_Correct.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, correctCodeText))
        {
            // Configure linear component (GS1 Code128) and 2‑D component (CC-C)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Set PDF417 specific parameter (columns)
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Save the generated barcode as PNG
            generator.Save(correctPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Generated correct GS1 Composite barcode: {correctPath}");

        // ------------------------------------------------------------
        // Attempt to generate barcode missing the '|' separator
        // ------------------------------------------------------------
        string errorPath = Path.Combine(outputDir, "GS1Composite_MissingSeparator.png");
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, missingSeparatorCodeText))
            {
                // Same component configuration as the correct example
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
                generator.Parameters.Barcode.Pdf417.Columns = 30;

                // Enable exception throwing when CodeText format is incorrect
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // This call is expected to throw due to missing separator
                generator.Save(errorPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode (unexpectedly) without separator: {errorPath}");
            }
        }
        catch (Exception ex)
        {
            // Output the caught exception details
            Console.WriteLine("Exception caught while generating GS1 Composite barcode without '|':");
            Console.WriteLine(ex.Message);
        }
    }
}