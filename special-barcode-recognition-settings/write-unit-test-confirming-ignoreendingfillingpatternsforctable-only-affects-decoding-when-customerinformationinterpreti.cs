// Title: Demonstrate effect of IgnoreEndingFillingPatternsForCTable on Australia Post barcode decoding
// Description: Shows how the IgnoreEndingFillingPatternsForCTable flag influences decoding of Australia Post barcodes when using CTable versus NTable.
// Category-Description: This example belongs to the Aspose.BarCode decoding configuration category. It illustrates the use of BarCodeReader settings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable, which are essential when working with Australia Post symbology. Developers often need to control filler‑character handling to obtain correct data from CTable‑encoded barcodes.
// Prompt: Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding when CustomerInformationInterpretingType is CTable.
// Tags: australia post, barcode decoding, ctable, ntable, ignoreendingfillingpatterns, aspose.barcode, unit test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an Australia Post barcode and verifies that the
/// <c>IgnoreEndingFillingPatternsForCTable</c> setting only influences decoding when
/// <c>CustomerInformationInterpretingType</c> is set to <c>CTable</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it under different settings,
    /// and prints the verification result.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate an Australia Post barcode using CTable encoding and filler characters
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567END"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Parameters.Barcode.BarHeight.Pixels = 50;
            gen.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Decode with CTable and IgnoreEndingFillingPatternsForCTable = false
        string resultCFalse = DecodeBarcode(barcodePath, CustomerInformationInterpretingType.CTable, false);
        // Decode with CTable and IgnoreEndingFillingPatternsForCTable = true
        string resultCTrue = DecodeBarcode(barcodePath, CustomerInformationInterpretingType.CTable, true);
        // Decode with NTable and IgnoreEndingFillingPatternsForCTable = true (should have no effect)
        string resultNTrue = DecodeBarcode(barcodePath, CustomerInformationInterpretingType.NTable, true);

        // Output the decoding results for manual inspection
        Console.WriteLine($"CTable, ignore=false : {resultCFalse}");
        Console.WriteLine($"CTable, ignore=true  : {resultCTrue}");
        Console.WriteLine($"NTable, ignore=true  : {resultNTrue}");

        // Verify that the flag only changes the result for CTable
        bool condition1 = resultCFalse != resultCTrue; // different when flag toggles
        bool condition2 = resultNTrue == resultCFalse; // unchanged for NTable

        if (condition1 && condition2)
        {
            Console.WriteLine("Test Passed: IgnoreEndingFillingPatternsForCTable only affects CTable decoding.");
        }
        else
        {
            Console.WriteLine("Test Failed: Unexpected decoding behavior.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome
        }
    }

    /// <summary>
    /// Decodes an Australia Post barcode from an image file using the specified
    /// interpreting type and the <c>IgnoreEndingFillingPatternsForCTable</c> flag.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="interpretingType">CTable or NTable interpreting mode.</param>
    /// <param name="ignoreEnding">Whether to ignore ending filling patterns for CTable.</param>
    /// <returns>The decoded text, or an empty string if decoding fails.</returns>
    static string DecodeBarcode(string imagePath, CustomerInformationInterpretingType interpretingType, bool ignoreEnding)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return string.Empty;
        }

        // Initialize the reader for Australia Post symbology
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Apply the test‑specific settings
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreEnding;

            // Return the first decoded result
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                return result.CodeText ?? string.Empty;
            }
        }

        // No barcode found
        return string.Empty;
    }
}