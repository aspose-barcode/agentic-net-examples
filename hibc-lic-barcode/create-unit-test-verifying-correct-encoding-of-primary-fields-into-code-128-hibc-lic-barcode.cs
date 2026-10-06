// Title: Unit Test for Encoding Primary Fields in a Code 128 HIBC LIC Barcode
// Description: Demonstrates generating a HIBC Code 128 LIC barcode with primary data fields, saving it, and verifying the encoded fields by decoding the image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, configure barcode parameters, and read back the barcode using BarCodeReader. Developers working with healthcare industry barcodes (HIBC) often need to encode product, labeler, and unit‑of‑measure information and validate it programmatically.
// Prompt: Create a unit test verifying correct encoding of primary fields into a Code 128 HIBC LIC barcode.
// Tags: barcode, hibc, code128, lic, complexbarcode, generation, recognition, unit-test, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates a simple unit‑test‑style verification of primary field encoding for a HIBC Code 128 LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode, decodes it, and validates the primary data fields.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "HIBCPrimary.png");

        // Define primary data values to be encoded
        string productNumber = "12345";
        string labelerCode = "A999";
        int unitOfMeasureId = 1;

        // Build the primary data codetext object
        HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = productNumber,
                LabelerIdentificationCode = labelerCode,
                UnitOfMeasureID = unitOfMeasureId
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10f; // Set module size
            generator.Save(barcodePath); // Save as PNG
        }

        // Verify that the barcode image file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Decode the barcode using the appropriate decode type
        BaseDecodeType decodeType = DecodeType.HIBCCode128LIC;
        bool testPassed = false;

        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to parse the complex HIBC LIC codetext
                HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                HIBCLICPrimaryDataCodetext decodedPrimary = complex as HIBCLICPrimaryDataCodetext;

                if (decodedPrimary == null)
                {
                    continue; // Not a primary data barcode; skip
                }

                // Compare each decoded field with the original values
                bool productMatch = decodedPrimary.Data.ProductOrCatalogNumber == productNumber;
                bool labelerMatch = decodedPrimary.Data.LabelerIdentificationCode == labelerCode;
                bool unitMatch = decodedPrimary.Data.UnitOfMeasureID == unitOfMeasureId;

                if (productMatch && labelerMatch && unitMatch)
                {
                    testPassed = true; // All fields match
                }
                else
                {
                    Console.WriteLine("FAILED: Decoded fields do not match expected values.");
                    Console.WriteLine($"Expected ProductOrCatalogNumber: {productNumber}, Got: {decodedPrimary.Data.ProductOrCatalogNumber}");
                    Console.WriteLine($"Expected LabelerIdentificationCode: {labelerCode}, Got: {decodedPrimary.Data.LabelerIdentificationCode}");
                    Console.WriteLine($"Expected UnitOfMeasureID: {unitOfMeasureId}, Got: {decodedPrimary.Data.UnitOfMeasureID}");
                }
            }
        }

        // Output final test result
        if (testPassed)
        {
            Console.WriteLine("PASSED: Primary fields encoded and decoded correctly.");
        }
        else
        {
            Console.WriteLine("FAILED: No valid HIBC LIC primary barcode was decoded.");
        }

        // Cleanup temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}