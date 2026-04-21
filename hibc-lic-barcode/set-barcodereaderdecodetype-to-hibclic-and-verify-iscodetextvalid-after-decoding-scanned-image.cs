using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create primary data codetext for HIBC LIC Code128
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

        // Generate the barcode image in memory
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Read the barcode using the appropriate DecodeType
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.HIBCCode128LIC))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Verify that the decoded text is not empty (simulating IsCodeTextValid)
                        bool isValid = !string.IsNullOrEmpty(result.CodeText);
                        Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                        Console.WriteLine($"IsCodeTextValid (simulated): {isValid}");

                        // Decode the complex codetext back to primary data
                        var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText) as HIBCLICPrimaryDataCodetext;
                        if (decoded?.Data != null)
                        {
                            Console.WriteLine($"ProductOrCatalogNumber: {decoded.Data.ProductOrCatalogNumber}");
                            Console.WriteLine($"LabelerIdentificationCode: {decoded.Data.LabelerIdentificationCode}");
                            Console.WriteLine($"UnitOfMeasureID: {decoded.Data.UnitOfMeasureID}");
                        }
                    }
                }
            }
        }
    }
}