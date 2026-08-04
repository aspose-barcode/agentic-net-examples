// Title: Decode HIBC Code128 LIC barcode and validate decoded text
// Description: Demonstrates generating a HIBC LIC barcode, decoding it with BarCodeReader using the HIBCCode128LIC decode type, and checking that the decoded text is present.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the ComplexBarcodeGenerator for creating HIBC (Health Industry Bar Code) LIC (Labeler Identification Code) barcodes and the BarCodeReader for decoding them. Developers working with medical or pharmaceutical labeling often need to generate HIBC barcodes, scan them from images, and verify the extracted data using classes such as HIBCLICPrimaryDataCodetext, ComplexBarcodeGenerator, BarCodeReader, and DecodeType.
// Prompt: Set BarCodeReader.DecodeType to HIBCLIC and verify IsCodeTextValid after decoding a scanned image.
// Tags: hibc, lic, barcode, decode, validation, aspose.barcode, complexbarcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a HIBC Code128 LIC barcode, decodes it,
/// and simulates validation of the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it back,
    /// and prints the decoded text along with a simple validity check.
    /// </summary>
    static void Main()
    {
        // Create HIBC LIC primary data codetext (Code128 variant)
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to create the barcode
            using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            barcodeStream.Position = 0;

            // Create BarCodeReader configured for HIBC Code128 LIC decoding
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.HIBCCode128LIC))
            {
                // Read all barcodes found in the stream
                var results = reader.ReadBarCodes();

                // Iterate through each detection result
                foreach (var result in results)
                {
                    // Simulate IsCodeTextValid by checking that CodeText is not null or empty
                    bool isCodeTextValid = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"IsCodeTextValid (simulated): {isCodeTextValid}");
                }
            }
        }
    }
}