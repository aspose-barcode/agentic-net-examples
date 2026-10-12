// Title: Encode HIBC LIC Combined Data into QR Code
// Description: Demonstrates how to create a HIBC QR LIC barcode that includes both primary and secondary data fields and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes to build a combined HIBC QR LIC symbology. Typical use cases include encoding product information, lot numbers, and expiration dates for healthcare and logistics applications. Developers often need to generate such barcodes for labeling and tracking purposes, and this snippet illustrates the essential API calls.
/// Prompt: Encode combined primary and secondary fields using HIBCLICCombinedCodetext and output a QR code file.
/// Tags: hibc, lic, combined, qr, barcode, generation, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC QR LIC barcode with combined primary and secondary data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it to the output folder.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create combined HIBC LIC codetext object
        HIBCLICCombinedCodetext combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC
        };

        // Populate primary data fields
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Populate secondary and additional data fields
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Now,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            Quantity = 30,
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            DateOfManufacture = DateTime.Now
        };

        // Define output file path
        string outputPath = Path.Combine(outputDir, "HIBCLICCombined.png");

        // Generate the barcode and save as PNG
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"HIBC LIC combined QR code saved to: {outputPath}");
    }
}