// Title: Demonstrate effect of IgnoreEndingFillingPatternsForCTable on AustraliaPost barcode decoding
// Description: Shows that the IgnoreEndingFillingPatternsForCTable flag only influences decoding when the CustomerInformationInterpretingType is set to CTable.
// Category-Description: This example belongs to the Aspose.BarCode decoding configuration category. It illustrates how to configure the AustraliaPost barcode reader using the CustomerInformationInterpretingType enum (CTable/NTable) and the IgnoreEndingFillingPatternsForCTable property. Developers working with Australian Post barcodes often need to control ending filling pattern handling to obtain correct customer information during decoding. The sample uses BarcodeGenerator, BarCodeReader, and related settings classes.
// Prompt: Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding when CustomerInformationInterpretingType is CTable.
// Tags: australiapost, barcode, decoding, ctable, ntable, ignoreendingfillingpatterns, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an AustraliaPost barcode and validates the impact of
/// <c>IgnoreEndingFillingPatternsForCTable</c> on decoding for different <c>CustomerInformationInterpretingType</c> settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it under three scenarios and prints test results.
    /// </summary>
    static void Main()
    {
        // Sample code text containing the ending filling pattern "333"
        const string originalCodeText = "5912345678AB333";

        // Generate an AustraliaPost barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            // Use CTable interpreting type for generation
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // ---------- Test 1: CTable with IgnoreEndingFillingPatternsForCTable = true ----------
                string decodedWithIgnore = DecodeBarcode(ms, CustomerInformationInterpretingType.CTable, true);

                // Reset stream for next read
                ms.Position = 0;

                // ---------- Test 2: CTable with IgnoreEndingFillingPatternsForCTable = false ----------
                string decodedWithoutIgnore = DecodeBarcode(ms, CustomerInformationInterpretingType.CTable, false);

                // Reset stream for next read
                ms.Position = 0;

                // ---------- Test 3: NTable with IgnoreEndingFillingPatternsForCTable = true ----------
                string decodedNTable = DecodeBarcode(ms, CustomerInformationInterpretingType.NTable, true);

                // Evaluate expectations
                bool test1Pass = decodedWithIgnore != decodedWithoutIgnore && decodedWithIgnore.EndsWith("z");
                bool test2Pass = decodedWithoutIgnore.EndsWith("333");
                bool test3Pass = decodedNTable == decodedWithoutIgnore; // property should have no effect for NTable

                // Output test results
                Console.WriteLine($"Test 1 (CTable + ignore): {(test1Pass ? "PASS" : "FAIL")} - Decoded: {decodedWithIgnore}");
                Console.WriteLine($"Test 2 (CTable + no ignore): {(test2Pass ? "PASS" : "FAIL")} - Decoded: {decodedWithoutIgnore}");
                Console.WriteLine($"Test 3 (NTable + ignore): {(test3Pass ? "PASS" : "FAIL")} - Decoded: {decodedNTable}");
            }
        }
    }

    /// <summary>
    /// Decodes an AustraliaPost barcode from a stream using the specified interpreting type and ignore flag.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <param name="interpretingType">Customer information interpreting type (CTable or NTable).</param>
    /// <param name="ignoreEndingFillingPatterns">Whether to ignore ending filling patterns for CTable.</param>
    /// <returns>The decoded code text, or an empty string if decoding fails.</returns>
    private static string DecodeBarcode(Stream imageStream, CustomerInformationInterpretingType interpretingType, bool ignoreEndingFillingPatterns)
    {
        using (var reader = new BarCodeReader(imageStream, DecodeType.AustraliaPost))
        {
            // Set the interpreting type (CTable or NTable)
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

            // Set the flag under test
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreEndingFillingPatterns;

            // Read barcodes and return the first result's CodeText
            foreach (var result in reader.ReadBarCodes())
            {
                return result.CodeText ?? string.Empty;
            }
        }

        // Return empty string if no barcode was read
        return string.Empty;
    }
}