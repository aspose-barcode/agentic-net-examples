using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates encoding and decoding of a HIBC Code128 LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it back, and verifies the data.
    /// </summary>
    static void Main()
    {
        // Sample primary data for HIBC Code128 LIC
        const string productNumber = "12345";
        const string labelerId = "A999";
        const int unitOfMeasureId = 1;

        // Build the complex codetext object that holds the primary data
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = productNumber,
                LabelerIdentificationCode = labelerId,
                UnitOfMeasureID = unitOfMeasureId
            }
        };

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        using (Image image = generator.GenerateBarCodeImage())
        using (var ms = new MemoryStream())
        {
            // Save the generated image as PNG into the memory stream
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Read the barcode back from the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
            {
                // Attempt to read all barcodes in the image
                var results = reader.ReadBarCodes();

                // Verify that at least one barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("FAILED: No barcode detected.");
                    return;
                }

                // Decode the first barcode's codetext into a complex object
                var result = results[0];
                var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText) as HIBCLICPrimaryDataCodetext;

                // Compare decoded fields with the original input values
                bool passed = decoded != null &&
                              decoded.Data.ProductOrCatalogNumber == productNumber &&
                              decoded.Data.LabelerIdentificationCode == labelerId &&
                              decoded.Data.UnitOfMeasureID == unitOfMeasureId;

                // Output the verification result
                Console.WriteLine(passed
                    ? "PASSED: Primary fields encoded and decoded correctly."
                    : "FAILED: Decoded fields do not match original.");
            }
        }
    }
}