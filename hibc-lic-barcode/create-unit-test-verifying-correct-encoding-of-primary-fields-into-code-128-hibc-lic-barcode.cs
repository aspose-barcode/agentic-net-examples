// Title: Unit Test for Encoding Primary Fields in Code 128 HIBC LIC Barcode
// Description: Demonstrates generating a Code 128 HIBC LIC barcode from primary data, then decoding it to verify that the encoded fields match the original values.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use the ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, EncodeTypes.HIBCCode128LIC, and BarCodeReader to create and validate HIBC‑LIC barcodes. Developers working with healthcare or logistics labeling often need to encode primary product information into HIBC barcodes and confirm correctness via decoding.
// Prompt: Create a unit test verifying correct encoding of primary fields into a Code 128 HIBC LIC barcode.
// Tags: barcode, code128, hibc, lic, complexbarcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates a simple verification of primary field encoding for a Code 128 HIBC LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode from primary data, decodes it, and checks field integrity.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for HIBC LIC Code128 barcode
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Build complex codetext containing only the primary data
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Decode the barcode from the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
            {
                var results = reader.ReadBarCodes();

                // Verify that a barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                    return;
                }

                // Extract the decoded text and attempt to parse it as primary data codetext
                var decodedText = results[0].CodeText;
                var decodedCodetext = ComplexCodetextReader.TryDecodeHIBCLIC(decodedText) as HIBCLICPrimaryDataCodetext;

                // Compare each field of the decoded data with the original primary data
                bool passed = decodedCodetext != null &&
                              decodedCodetext.Data.ProductOrCatalogNumber == primaryData.ProductOrCatalogNumber &&
                              decodedCodetext.Data.LabelerIdentificationCode == primaryData.LabelerIdentificationCode &&
                              decodedCodetext.Data.UnitOfMeasureID == primaryData.UnitOfMeasureID;

                // Output the test result
                Console.WriteLine(passed ? "PASSED: Primary fields encoded and decoded correctly."
                                         : "FAILED: Decoded data does not match original.");
            }
        }
    }
}