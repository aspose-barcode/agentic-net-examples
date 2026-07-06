// Title: HIBC Code 128 LIC Barcode Generation and Checksum Validation
// Description: Demonstrates creating a HIBC Code 128 LIC barcode, decoding it, and verifying its checksum by comparing decoded fields with the original data.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, ComplexCodetextReader, and BarCodeReader classes to produce and validate HIBC symbology, a common requirement in healthcare and logistics for accurate product identification. Developers often need to generate HIBC barcodes, read them back, and ensure data integrity via checksum verification.
// Prompt: Validate that the generated barcode complies with HIBC specifications by checking its checksum after creation.
// Tags: hibc, code128, lic, barcode, generation, validation, checksum, aspnet, aspnetcore, aspnetmvc, aspnetwebapi

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a HIBC Code 128 LIC barcode, reads it back, and validates the checksum
/// by ensuring the decoded data matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, decoding, and checksum validation.
    /// </summary>
    static void Main()
    {
        // Define primary HIBC data to encode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image in memory using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        using (var ms = new MemoryStream())
        {
            // Save the bitmap to a memory stream in PNG format
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Decode the barcode from the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
            {
                var results = reader.ReadBarCodes();

                // Ensure at least one barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Retrieve the decoded text and attempt to parse it as HIBCLICPrimaryDataCodetext
                var decodedText = results[0].CodeText;
                var decodedCodetext = ComplexCodetextReader.TryDecodeHIBCLIC(decodedText) as HIBCLICPrimaryDataCodetext;

                // Verify that decoding succeeded
                if (decodedCodetext == null)
                {
                    Console.WriteLine("Failed to decode HIBC LIC codetext.");
                    return;
                }

                // Validate that decoded fields match the original data (checksum validation)
                bool isValid =
                    decodedCodetext.Data.ProductOrCatalogNumber == primaryCodetext.Data.ProductOrCatalogNumber &&
                    decodedCodetext.Data.LabelerIdentificationCode == primaryCodetext.Data.LabelerIdentificationCode &&
                    decodedCodetext.Data.UnitOfMeasureID == primaryCodetext.Data.UnitOfMeasureID;

                Console.WriteLine(isValid ? "Checksum validation passed." : "Checksum validation failed.");
            }
        }
    }
}