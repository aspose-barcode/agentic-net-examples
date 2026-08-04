// Title: Generate and Validate HIBC Code128 LIC Barcode with Checksum
// Description: Demonstrates creating a HIBC Code128 LIC barcode, saving it as an image, and verifying its checksum by decoding and comparing the generated codetext.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to construct HIBC barcodes and BarCodeReader to decode them, a common workflow for developers needing to ensure barcode compliance and data integrity in healthcare and logistics applications. Typical use cases include label creation, automated scanning validation, and regulatory compliance checks.
// Prompt: Validate that the generated barcode complies with HIBC specifications by checking its checksum after creation.
// Tags: hibc, code128, lic, barcode generation, barcode validation, checksum, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC Code128 LIC barcode, saves it to a file,
/// and validates its checksum by decoding the image and comparing the codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and checksum validation.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for HIBC Code128 LIC barcode
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Construct the codetext object that defines the barcode type and data
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // Define the output image path
        string imagePath = "hibc_lic.png";

        // Generate the barcode image and save it to the specified file
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Optional: set colors or other parameters here if needed
            generator.Save(imagePath);
        }

        // Read the generated barcode image and verify checksum by comparing decoded text
        using (var reader = new BarCodeReader(imagePath, DecodeType.HIBCCode128LIC))
        {
            // Ensure checksum validation is enabled (default for HIBC)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool valid = false;

            // Iterate through all detected barcodes (should be one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                // If the decoded text matches the original codetext, checksum is correct
                if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == hibcCodetext.GetConstructedCodetext())
                {
                    valid = true;
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
                else
                {
                    Console.WriteLine($"Decoded CodeText does not match expected value: {result.CodeText}");
                }
            }

            // Output the overall validation result
            Console.WriteLine(valid
                ? "HIBC barcode checksum validation succeeded."
                : "HIBC barcode checksum validation failed.");
        }
    }
}