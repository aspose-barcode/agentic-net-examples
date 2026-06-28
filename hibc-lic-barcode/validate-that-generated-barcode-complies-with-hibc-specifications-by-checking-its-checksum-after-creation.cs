using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation, encoding, and validation of a HIBC LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a HIBC primary data barcode, reads it back,
    /// decodes the complex codetext, and validates the checksum and data integrity.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC data (product number, labeler code, unit of measure)
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
            // Enable checksum generation (required for HIBC)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Generate bitmap and write it to a memory stream in PNG format
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Read the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
                {
                    var results = reader.ReadBarCodes();

                    // Ensure at least one barcode was detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    var result = results[0];

                    // Decode the complex HIBC codetext back into its structured form
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText) as HIBCLICPrimaryDataCodetext;
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode HIBC codetext.");
                        return;
                    }

                    // Verify that decoded fields match the original data
                    bool isValid =
                        decoded.Data.ProductOrCatalogNumber == primaryCodetext.Data.ProductOrCatalogNumber &&
                        decoded.Data.LabelerIdentificationCode == primaryCodetext.Data.LabelerIdentificationCode &&
                        decoded.Data.UnitOfMeasureID == primaryCodetext.Data.UnitOfMeasureID;

                    // Output validation result
                    Console.WriteLine(isValid ? "Checksum validation passed." : "Checksum validation failed.");
                }
            }
        }
    }
}