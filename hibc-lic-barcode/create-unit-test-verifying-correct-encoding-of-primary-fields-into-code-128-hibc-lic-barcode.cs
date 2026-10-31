// Title: Unit Test for Encoding Primary Fields into a Code 128 HIBC LIC Barcode
// Description: Demonstrates generating a HIBC QR LIC barcode with primary data, then reading it back to verify that the product number, labeler code, and unit of measure are correctly encoded.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, configure barcode parameters, save the image, and decode it using BarCodeReader. Developers working with healthcare barcodes, especially HIBC QR LIC symbology, can use these APIs to embed and validate primary data fields in automated tests or production workflows.
// Prompt: Create a unit test verifying correct encoding of primary fields into a Code 128 HIBC LIC barcode.
// Tags: barcode symbology, unit test, hibc, complex barcode, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates a simple unit‑test‑style verification of primary data encoding in a HIBC QR LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode with primary data, reads it back, and validates the encoded fields.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and define the output file path
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "primary.png");

        // Define the expected primary data values
        string expectedProductNumber = "12345";
        string expectedLabelerCode = "A999";
        int expectedUnitOfMeasureId = 1;

        // Build the complex codetext containing primary data
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // Use the documented HIBC QR LIC symbology
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = expectedProductNumber,
                LabelerIdentificationCode = expectedLabelerCode,
                UnitOfMeasureID = expectedUnitOfMeasureId
            }
        };

        // Generate the barcode image and save it to the temporary location
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 10; // Set module size for better readability
            gen.Save(barcodePath);
        }

        bool testPassed = false;
        string failureReason = "No barcode detected.";

        // Read and decode the saved barcode image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.HIBCQRLIC))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Ensure at least one barcode was found and contains codetext
            if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
            {
                // Attempt to parse the complex codetext back into a strongly‑typed object
                HIBCLICComplexCodetext decoded = ComplexCodetextReader.TryDecodeHIBCLIC(results[0].CodeText);

                // Verify that the decoded object is primary data and contains the expected fields
                if (decoded is HIBCLICPrimaryDataCodetext primaryDecoded && primaryDecoded.Data != null)
                {
                    bool productMatch = primaryDecoded.Data.ProductOrCatalogNumber == expectedProductNumber;
                    bool labelerMatch = primaryDecoded.Data.LabelerIdentificationCode == expectedLabelerCode;
                    bool unitMatch = primaryDecoded.Data.UnitOfMeasureID == expectedUnitOfMeasureId;

                    if (productMatch && labelerMatch && unitMatch)
                    {
                        testPassed = true;
                    }
                    else
                    {
                        failureReason = $"Field mismatch. Product:{primaryDecoded.Data.ProductOrCatalogNumber}, Labeler:{primaryDecoded.Data.LabelerIdentificationCode}, Unit:{primaryDecoded.Data.UnitOfMeasureID}";
                    }
                }
                else
                {
                    failureReason = "Decoded codetext is not primary data.";
                }
            }
        }

        // Output the test result
        Console.WriteLine(testPassed ? "TEST PASSED: Primary fields encoded correctly." : $"TEST FAILED: {failureReason}");
    }
}