// Title: Australia Post barcode decoding with CTable filler handling
// Description: Demonstrates how the IgnoreEndingFillingPatternsForCTable setting influences decoding of Australia Post barcodes when using the CTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Developers often need to control filler pattern handling when decoding CTable encoded barcodes, making this pattern useful for testing and validation scenarios.
// Prompt: Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding when CustomerInformationInterpretingType is CTable.
// Tags: australia post, ctable, ntable, ignoreendingfillingpatterns, barcode generation, barcode recognition, aspose.barcode, unit test example

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and decoding of an Australia Post barcode to verify the effect of
/// <c>IgnoreEndingFillingPatternsForCTable</c> when using <c>CTable</c> versus <c>NTable</c> interpreting types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode image, decodes it under different settings,
    /// and outputs the comparison results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "AustraliaPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPostCTable.png");

        // Generate a barcode encoded with CTable and a filler that would be read as 'z' if not ignored
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567END"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4;
            gen.Parameters.Barcode.BarHeight.Pixels = 50;
            gen.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Decode the barcode under four different configurations
        // 1. CTable interpreting, ignore filler = false
        string resultCFalse = ReadBarcode(barcodePath, CustomerInformationInterpretingType.CTable, false);
        // 2. CTable interpreting, ignore filler = true
        string resultCTrue = ReadBarcode(barcodePath, CustomerInformationInterpretingType.CTable, true);
        // 3. NTable interpreting, ignore filler = false
        string resultNFalse = ReadBarcode(barcodePath, CustomerInformationInterpretingType.NTable, false);
        // 4. NTable interpreting, ignore filler = true
        string resultNTrue = ReadBarcode(barcodePath, CustomerInformationInterpretingType.NTable, true);

        // Determine whether the ignore setting had an effect for each interpreting type
        bool cTableEffect = !string.Equals(resultCFalse, resultCTrue, StringComparison.Ordinal);
        bool nTableEffect = !string.Equals(resultNFalse, resultNTrue, StringComparison.Ordinal);

        // Output the verification results
        Console.WriteLine($"CTable effect (should be true): {cTableEffect}");
        Console.WriteLine($"NTable effect (should be false): {nTableEffect}");

        Console.WriteLine($"Result CTable IgnoreEnding=false : {resultCFalse}");
        Console.WriteLine($"Result CTable IgnoreEnding=true  : {resultCTrue}");
        Console.WriteLine($"Result NTable IgnoreEnding=false : {resultNFalse}");
        Console.WriteLine($"Result NTable IgnoreEnding=true  : {resultNTrue}");

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads an Australia Post barcode from an image file using the specified interpreting type
    /// and filler‑ignoring setting.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="interpretingType">The customer information interpreting type (CTable or NTable).</param>
    /// <param name="ignoreEnding">Whether to ignore ending filling patterns for CTable.</param>
    /// <returns>The decoded text, or an empty string if decoding fails.</returns>
    static string ReadBarcode(string imagePath, CustomerInformationInterpretingType interpretingType, bool ignoreEnding)
    {
        BaseDecodeType decodeType = DecodeType.AustraliaPost;
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply the interpreting type and filler‑ignoring option
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = ignoreEnding;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results != null && results.Length > 0)
            {
                return results[0].CodeText ?? string.Empty;
            }
            return string.Empty;
        }
    }
}