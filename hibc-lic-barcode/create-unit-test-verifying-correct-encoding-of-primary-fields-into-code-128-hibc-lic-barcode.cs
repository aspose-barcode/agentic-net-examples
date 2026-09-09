// Title: Unit Test for Encoding Primary Fields into a Code 128 HIBC LIC Barcode
// Description: Demonstrates generating a HIBC Code 128 LIC barcode with primary data fields, then decoding it to verify the fields are correctly encoded.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, configure barcode parameters, save the image, and then read it back using BarCodeReader and ComplexCodetextReader. Developers working with healthcare barcodes, especially HIBC LIC, can use this pattern to validate encoding and decoding of primary data fields.
// Prompt: Create a unit test verifying correct encoding of primary fields into a Code 128 HIBC LIC barcode.
// Tags: barcode symbology, encoding, decoding, hibc, code128, lic, complexbarcode, unit-test, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC Code 128 LIC barcode with primary data,
/// decodes it, and validates that the encoded fields match the original values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs barcode generation, decoding, validation, and cleanup.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "HIBCLICPrimary.png");

        // Create primary data codetext and configure its properties.
        HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set the X-dimension (module width) to 10 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Resolve the appropriate decode type via reflection (handles possible member name changes).
        BaseDecodeType decodeType;
        var decodeField = typeof(DecodeType).GetField("HIBCCode128LIC");
        if (decodeField == null)
        {
            Console.WriteLine("FAILED: DecodeType HIBCCode128LIC not found.");
            return;
        }
        decodeType = (BaseDecodeType)decodeField.GetValue(null);

        // Read and decode the barcode from the saved image.
        using (BarCodeReader reader = new BarCodeReader(tempPath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected.");
                return;
            }

            bool allPassed = true;
            foreach (BarCodeResult result in results)
            {
                // Attempt to decode the complex codetext and cast to primary data type.
                HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                HIBCLICPrimaryDataCodetext decoded = complex as HIBCLICPrimaryDataCodetext;
                if (decoded == null)
                {
                    Console.WriteLine("FAILED: Decoded codetext is not primary data.");
                    allPassed = false;
                    continue;
                }

                // Compare each field with the original values.
                bool productMatch = decoded.Data.ProductOrCatalogNumber == primaryCodetext.Data.ProductOrCatalogNumber;
                bool labelerMatch = decoded.Data.LabelerIdentificationCode == primaryCodetext.Data.LabelerIdentificationCode;
                bool uomMatch = decoded.Data.UnitOfMeasureID == primaryCodetext.Data.UnitOfMeasureID;

                if (!productMatch || !labelerMatch || !uomMatch)
                {
                    Console.WriteLine("FAILED: Mismatch in decoded fields.");
                    Console.WriteLine($"Expected Product: {primaryCodetext.Data.ProductOrCatalogNumber}, Got: {decoded.Data.ProductOrCatalogNumber}");
                    Console.WriteLine($"Expected Labeler: {primaryCodetext.Data.LabelerIdentificationCode}, Got: {decoded.Data.LabelerIdentificationCode}");
                    Console.WriteLine($"Expected UOM: {primaryCodetext.Data.UnitOfMeasureID}, Got: {decoded.Data.UnitOfMeasureID}");
                    allPassed = false;
                }
            }

            Console.WriteLine(allPassed ? "PASS: Primary fields encoded and decoded correctly." : "FAILED: One or more checks failed.");
        }

        // Clean up the temporary barcode image file.
        try
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
        catch
        {
            // Ignore any errors during cleanup.
        }
    }
}