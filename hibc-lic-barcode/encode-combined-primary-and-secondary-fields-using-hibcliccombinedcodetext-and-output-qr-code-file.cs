// Title: Encode HIBC LIC Combined QR Code
// Description: Demonstrates encoding primary and secondary fields into a HIBC QR code using Aspose.BarCode and saving it as an image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes to create a HIBC QR (LIC) barcode. Developers often need to combine multiple data fields for healthcare and logistics labeling, and this snippet illustrates the typical workflow for generating such barcodes with customizable error correction levels.
// Prompt: Encode combined primary and secondary fields using HIBCLICCombinedCodetext and output a QR code file.
// Tags: hibc, qr, combined, barcode, generation, aspnet, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode; // for QRErrorLevel enum

/// <summary>
/// Example program that creates a HIBC QR (LIC) barcode with combined primary and secondary data fields
/// and saves the resulting image to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the combined codetext, configures the generator,
    /// and writes the QR code image to disk.
    /// </summary>
    static void Main()
    {
        // Build the combined codetext containing both primary and secondary data for a HIBC QR (LIC) barcode
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // Specify the HIBC QR LIC symbology
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product identifier
                LabelerIdentificationCode = "A999", // Labeler code
                UnitOfMeasureID = 1                 // Unit of measure identifier
            },
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now.AddMonths(6),          // Expiration date (6 months from now)
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,    // Date format for the expiry date
                Quantity = 30,                                   // Quantity of items
                LotNumber = "LOT123",                            // Lot number
                SerialNumber = "SERIAL123",                      // Serial number
                DateOfManufacture = DateTime.Now.AddMonths(-1)   // Manufacture date (1 month ago)
            }
        };

        // Initialize the complex barcode generator with the combined codetext
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Configure a high error correction level to improve scan reliability
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code as a PNG image file
            generator.Save("hibc_combined_qr.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC LIC combined QR code generated: hibc_combined_qr.png");
    }
}