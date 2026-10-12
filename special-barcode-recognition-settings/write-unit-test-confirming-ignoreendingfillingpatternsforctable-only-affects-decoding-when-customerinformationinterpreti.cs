// Title: Demonstrate effect of IgnoreEndingFillingPatternsForCTable on AustraliaPost barcode decoding
// Description: This example generates an Australia Post barcode with CTable encoding, then decodes it with and without ignoring ending filler patterns to verify the setting's impact.
// Category-Description: Shows how to use Aspose.BarCode's AustraliaPost settings, specifically CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable, to control decoding behavior. Useful for developers working with postal barcodes who need to handle filler patterns correctly. Covers BarcodeGenerator, BarCodeReader, and related settings in typical encoding/decoding scenarios.
// Prompt: Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding when CustomerInformationInterpretingType is CTable.
// Tags: barcode symbology, australia post, ctable, ntable, decoding, encoding, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example that validates the IgnoreEndingFillingPatternsForCTable setting for Australia Post barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode, reads it with different settings, and reports whether the ignore‑filling flag behaves as expected.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AustraliaPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode using CTable encoding.
        // The data includes a trailing "END" that will produce filler patterns.
        // ------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567END"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4f;
            gen.Parameters.Barcode.BarHeight.Pixels = 50f;
            gen.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Local function that reads the barcode with the specified
        // CustomerInformationInterpretingType and ignore‑filling flag.
        // ------------------------------------------------------------
        string ReadBarcode(CustomerInformationInterpretingType type, bool ignoreFilling)
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = type;
                reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreFilling;
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    return string.Empty;
                }
                return results[0].CodeText;
            }
        }

        // ------------------------------------------------------------
        // Decode the barcode under four different configurations.
        // ------------------------------------------------------------
        string ctableNoIgnore = ReadBarcode(CustomerInformationInterpretingType.CTable, false);
        string ctableIgnore   = ReadBarcode(CustomerInformationInterpretingType.CTable, true);
        string ntableNoIgnore = ReadBarcode(CustomerInformationInterpretingType.NTable, false);
        string ntableIgnore   = ReadBarcode(CustomerInformationInterpretingType.NTable, true);

        // Determine whether the ignore‑filling flag changed the result for each table type
        bool ctableEffect = !ctableNoIgnore.Equals(ctableIgnore);
        bool ntableEffect = ntableNoIgnore.Equals(ntableIgnore);

        // Output the decoded values for manual inspection
        Console.WriteLine("CTable without IgnoreEndingFillingPatternsForCTable: " + ctableNoIgnore);
        Console.WriteLine("CTable with IgnoreEndingFillingPatternsForCTable: " + ctableIgnore);
        Console.WriteLine("NTable without IgnoreEndingFillingPatternsForCTable: " + ntableNoIgnore);
        Console.WriteLine("NTable with IgnoreEndingFillingPatternsForCTable: " + ntableIgnore);

        // ------------------------------------------------------------
        // Evaluate the test outcome based on the expected behavior.
        // ------------------------------------------------------------
        if (ctableEffect && ntableEffect)
        {
            Console.WriteLine("PASSED: IgnoreEndingFillingPatternsForCTable affects CTable decoding and has no effect on NTable.");
        }
        else if (!ctableEffect && ntableEffect)
        {
            Console.WriteLine("FAILED: IgnoreEndingFillingPatternsForCTable did not affect CTable decoding.");
        }
        else if (ctableEffect && !ntableEffect)
        {
            Console.WriteLine("FAILED: IgnoreEndingFillingPatternsForCTable affected NTable decoding.");
        }
        else
        {
            Console.WriteLine("FAILED: Neither condition behaved as expected.");
        }
    }
}