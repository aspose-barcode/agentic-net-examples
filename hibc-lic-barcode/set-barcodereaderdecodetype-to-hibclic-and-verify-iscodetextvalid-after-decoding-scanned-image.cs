// Title: Decode HIBC Code128 LIC and Validate Code Text
// Description: Demonstrates setting BarCodeReader.DecodeType to HIBCLIC and checking IsCodeTextValid after decoding a generated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a HIBC Code128 LIC barcode and BarCodeReader to decode it. Developers commonly use these APIs to generate complex symbologies, read scanned images, and validate decoded data in healthcare and logistics applications.
// Prompt: Set BarCodeReader.DecodeType to HIBCLIC and verify IsCodeTextValid after decoding a scanned image.
// Tags: hibc, lic, decode, barcode, barcodereader, complexbarcodegenerator, hibclicprimarydatacodetext

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC Code128 LIC barcode, decodes it,
/// and validates the decoded text using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, and prints validation results.
    /// </summary>
    static void Main()
    {
        // Define primary HIBC LIC data (product number, labeler ID, unit of measure)
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
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Initialize BarCodeReader with the specific decode type for HIBC Code128 LIC
            using (var reader = new BarCodeReader(bitmap, DecodeType.HIBCCode128LIC))
            {
                // Decode all barcodes found in the image
                var results = reader.ReadBarCodes();

                // Iterate through each decoding result
                foreach (var result in results)
                {
                    // Determine if the decoded text is non‑empty (valid)
                    bool isValid = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Is Code Text Valid: {isValid}");
                }

                // Inform the user if no barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
            }
        }
    }
}