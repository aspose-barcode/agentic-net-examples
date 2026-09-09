// Title: Encode HIBC QR LIC with combined primary and secondary fields
// Description: Demonstrates creating a HIBC QR LIC barcode that includes both primary and secondary data elements and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator together with HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes. Typical scenarios include healthcare, pharmaceutical, and logistics labeling where HIBC QR LIC barcodes carry detailed product information. Developers often need to combine multiple data sections into a single barcode and control image parameters such as X‑dimension.
// Prompt: Encode combined primary and secondary fields using HIBCLICCombinedCodetext and output a QR code file.
// Tags: hibc, qr, lic, combined, primary, secondary, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates encoding a HIBC QR LIC barcode with combined primary and secondary data fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare combined HIBC QR LIC codetext with primary and secondary data
        HIBCLICCombinedCodetext combinedCodetext = new HIBCLICCombinedCodetext();
        combinedCodetext.BarcodeType = EncodeTypes.HIBCQRLIC;

        // Set primary data fields
        combinedCodetext.PrimaryData = new PrimaryData();
        combinedCodetext.PrimaryData.ProductOrCatalogNumber = "12345";
        combinedCodetext.PrimaryData.LabelerIdentificationCode = "A999";
        combinedCodetext.PrimaryData.UnitOfMeasureID = 1;

        // Set secondary and additional data fields
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData();
        combinedCodetext.SecondaryAndAdditionalData.ExpiryDate = DateTime.Now;
        combinedCodetext.SecondaryAndAdditionalData.ExpiryDateFormat = HIBCLICDateFormat.MMDDYY;
        combinedCodetext.SecondaryAndAdditionalData.Quantity = 30;
        combinedCodetext.SecondaryAndAdditionalData.LotNumber = "LOT123";
        combinedCodetext.SecondaryAndAdditionalData.SerialNumber = "SERIAL123";
        combinedCodetext.SecondaryAndAdditionalData.DateOfManufacture = DateTime.Now;

        // Define output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICCombined.png");

        // Generate QR code image using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Adjust X-dimension for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"HIBC LIC combined QR code saved to: {outputPath}");
    }
}