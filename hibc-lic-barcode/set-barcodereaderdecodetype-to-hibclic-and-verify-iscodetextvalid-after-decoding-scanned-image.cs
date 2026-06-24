using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and reading of a primary HIBC LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a HIBC Code128 LIC barcode, reads it back,
    /// and displays decoded information.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC LIC data with product details.
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

        // Generate the barcode image in memory using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        // Create a bitmap from the generated barcode.
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        // Use a memory stream to hold the PNG image data.
        using (var ms = new MemoryStream())
        {
            // Save the bitmap as PNG into the memory stream.
            bitmap.Save(ms, ImageFormat.Png);
            // Reset stream position to the beginning for reading.
            ms.Position = 0;

            // Initialize a barcode reader for the specific HIBC decode type.
            using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
            {
                // Read all barcodes found in the stream.
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // Simple validation: check that CodeText is not null or empty.
                    bool isValid = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"IsCodeTextValid (simulated): {isValid}");

                    // Decode complex HIBC codetext to retrieve original fields.
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText) as HIBCLICPrimaryDataCodetext;
                    if (decoded != null && decoded.Data != null)
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