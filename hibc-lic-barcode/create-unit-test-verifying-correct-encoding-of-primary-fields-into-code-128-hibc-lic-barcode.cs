using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare primary data for HIBC LIC Code128 barcode
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

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                // Decode the generated image with the appropriate DecodeType
                using (var reader = new BarCodeReader(image, DecodeType.HIBCCode128LIC))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Retrieve the raw codetext from the detected barcode
                    string rawCodeText = results[0].CodeText;

                    // Decode the complex HIBC LIC codetext back to its object representation
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(rawCodeText) as HIBCLICPrimaryDataCodetext;
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode complex codetext.");
                        return;
                    }

                    // Verify that the decoded primary fields match the original values
                    bool isMatch =
                        decoded.Data.ProductOrCatalogNumber == primaryCodetext.Data.ProductOrCatalogNumber &&
                        decoded.Data.LabelerIdentificationCode == primaryCodetext.Data.LabelerIdentificationCode &&
                        decoded.Data.UnitOfMeasureID == primaryCodetext.Data.UnitOfMeasureID;

                    Console.WriteLine(isMatch ? "Test passed." : "Test failed.");
                }
            }
        }
    }
}