// Title: Encode primary fields into Code 128 HIBC LIC barcode and verify via unit test
// Description: Demonstrates creating a HIBC LIC barcode from primary data fields, generating an image, and decoding it to confirm correct encoding.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, image generation, and BarCodeReader to validate encoding. Developers working with healthcare product identification (HIBC) often need to encode primary fields into a Code 128 LIC barcode and verify the result programmatically.
// Prompt: Create a unit test verifying correct encoding of primary fields into a Code 128 HIBC LIC barcode.
// Tags: barcode symbology, encoding, png, complexbarcode, generator, reader

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC LIC Code 128 barcode from primary data,
/// then reads it back to verify that the encoded fields match the original values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, image saving,
    /// decoding, and validation of primary data fields.
    /// </summary>
    static void Main()
    {
        // Define primary data fields that will be encoded into the barcode
        const string productNumber = "12345";
        const string labelerCode = "A999";
        const int unitOfMeasure = 1;

        // Build the complex codetext object with the required primary data
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = productNumber,
                LabelerIdentificationCode = labelerCode,
                UnitOfMeasureID = unitOfMeasure
            }
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        // Render the barcode to an image object
        using (var image = generator.GenerateBarCodeImage())
        // Prepare a memory stream to hold the PNG data
        using (var ms = new MemoryStream())
        {
            // Save the image as PNG into the memory stream
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Decode the barcode from the memory stream using BarCodeReader
            using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
            {
                var results = reader.ReadBarCodes();

                // Verify that at least one barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                    return;
                }

                // Extract the raw codetext from the first detection result
                var decodedText = results[0].CodeText;

                // Parse the complex codetext back into a HIBCLICPrimaryDataCodetext object
                var decodedComplex = ComplexCodetextReader.TryDecodeHIBCLIC(decodedText) as HIBCLICPrimaryDataCodetext;

                // Ensure decoding succeeded and returned the expected type
                if (decodedComplex == null)
                {
                    Console.WriteLine("FAILED: Decoding returned null or wrong type.");
                    return;
                }

                // Compare each primary field with the original values
                bool passed = decodedComplex.Data.ProductOrCatalogNumber == productNumber &&
                              decodedComplex.Data.LabelerIdentificationCode == labelerCode &&
                              decodedComplex.Data.UnitOfMeasureID == unitOfMeasure;

                // Output the test result
                Console.WriteLine(passed ? "PASSED" : "FAILED");
            }
        }
    }
}