// Title: Demonstrate effect of IgnoreEndingFillingPatternsForCTable on Australia Post barcode decoding
// Description: Shows how the IgnoreEndingFillingPatternsForCTable flag influences decoding of Australia Post barcodes when using CTable interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It illustrates using BarcodeGenerator, BarCodeReader, and related settings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Developers often need to control how trailing filler patterns are handled during decoding, especially when working with CTable customer information.
// Prompt: Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding when CustomerInformationInterpretingType is CTable.
// Tags: australia post, barcode generation, barcode recognition, ctable, ntable, ignoreendingfillingpatterns, unit test, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates the impact of the IgnoreEndingFillingPatternsForCTable flag on decoding
/// Australia Post barcodes with different CustomerInformationInterpretingType settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates an Australia Post barcode, decodes it under various
    /// configurations, and prints verification results.
    /// </summary>
    static void Main()
    {
        // Sample code text for an Australia Post barcode
        const string codeText = "5912345678AB";

        // Generate a barcode image using CTable interpreting type
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Decode with CTable interpreting type, flag set to false
                string resultCFalse = Decode(barcodeImage, CustomerInformationInterpretingType.CTable, false);
                // Decode with CTable interpreting type, flag set to true
                string resultCTrue = Decode(barcodeImage, CustomerInformationInterpretingType.CTable, true);

                // Decode with NTable interpreting type, flag set to false
                string resultNFalse = Decode(barcodeImage, CustomerInformationInterpretingType.NTable, false);
                // Decode with NTable interpreting type, flag set to true
                string resultNTrue = Decode(barcodeImage, CustomerInformationInterpretingType.NTable, true);

                // Verify that the flag influences decoding only when interpreting type is CTable
                bool cTableEffect = resultCFalse != resultCTrue; // should differ
                bool nTableEffect = resultNFalse == resultNTrue; // should be the same

                Console.WriteLine($"CTable flag effect (should differ): {(cTableEffect ? "PASS" : "FAIL")}");
                Console.WriteLine($"NTable flag effect (should be same): {(nTableEffect ? "PASS" : "FAIL")}");

                // Optional: output decoded texts for manual inspection
                Console.WriteLine($"CTable false:  {resultCFalse ?? "null"}");
                Console.WriteLine($"CTable true:   {resultCTrue ?? "null"}");
                Console.WriteLine($"NTable false:  {resultNFalse ?? "null"}");
                Console.WriteLine($"NTable true:   {resultNTrue ?? "null"}");
            }
        }
    }

    // Helper method to decode a barcode image with specified settings
    static string Decode(Bitmap image, CustomerInformationInterpretingType interpretingType, bool ignoreEnding)
    {
        using (BarCodeReader reader = new BarCodeReader(image, DecodeType.AustraliaPost))
        {
            // Set the interpreting type (CTable or NTable)
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            // Set whether to ignore ending filling patterns for CTable
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreEnding;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                return results[0].CodeText;
            }
            return null;
        }
    }
}