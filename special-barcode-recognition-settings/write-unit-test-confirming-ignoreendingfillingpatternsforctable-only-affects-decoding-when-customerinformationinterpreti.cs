using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and decoding of an Australia Post barcode
/// using different customer information interpreting types and flags.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, decodes it with various settings,
    /// and prints verification results to the console.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Australia Post format with a sample value.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
        {
            // Configure the generator to use the CTable interpreting type.
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image.
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Decode the image using CTable with the flag set to true.
                string resultCTableFlagTrue = Decode(barcodeImage, CustomerInformationInterpretingType.CTable, true);
                // Decode the image using CTable with the flag set to false.
                string resultCTableFlagFalse = Decode(barcodeImage, CustomerInformationInterpretingType.CTable, false);
                // Decode the image using NTable (flag should have no effect).
                string resultNTableFlagTrue = Decode(barcodeImage, CustomerInformationInterpretingType.NTable, true);

                // Output the raw decoding results.
                Console.WriteLine($"CTable, Flag=True : {resultCTableFlagTrue}");
                Console.WriteLine($"CTable, Flag=False: {resultCTableFlagFalse}");
                Console.WriteLine($"NTable, Flag=True : {resultNTableFlagTrue}");

                // Verify that the flag influences CTable decoding.
                bool flagAffectsCTable = !string.Equals(resultCTableFlagTrue, resultCTableFlagFalse, StringComparison.Ordinal);
                // Verify that the flag is ignored for non‑CTable interpreting types.
                bool flagIgnoredForNTable = string.Equals(resultCTableFlagFalse, resultNTableFlagTrue, StringComparison.Ordinal);

                // Print verification outcomes.
                Console.WriteLine();
                Console.WriteLine($"Flag affects CTable decoding: {(flagAffectsCTable ? "PASS" : "FAIL")}");
                Console.WriteLine($"Flag ignored for non-CTable interpreting type: {(flagIgnoredForNTable ? "PASS" : "FAIL")}");
            }
        }
    }

    /// <summary>
    /// Decodes a barcode image using the specified interpreting type and flag.
    /// </summary>
    /// <param name="image">The bitmap containing the barcode.</param>
    /// <param name="interpretingType">The customer information interpreting type to apply.</param>
    /// <param name="ignoreEndingFillingPatterns">
    /// When true, ending filling patterns are ignored for CTable decoding.
    /// </param>
    /// <returns>The decoded text, or an empty string if decoding fails.</returns>
    static string Decode(Bitmap image, CustomerInformationInterpretingType interpretingType, bool ignoreEndingFillingPatterns)
    {
        // Initialize a barcode reader for Australia Post format.
        using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
        {
            // Apply the requested interpreting type.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            // Set the flag that controls handling of ending filling patterns for CTable.
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreEndingFillingPatterns;

            // Iterate over detected barcodes (expecting at most one).
            foreach (var result in reader.ReadBarCodes())
            {
                // Return the first decoded text, or an empty string if null.
                return result.CodeText ?? string.Empty;
            }
        }

        // Return empty string if no barcode was read.
        return string.Empty;
    }
}