using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a HIBC QR LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares barcode data, generates the QR code,
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare combined primary and secondary data for HIBC QR LIC barcode
        // ------------------------------------------------------------
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            PrimaryData = new PrimaryData
            {
                // Product or catalog number (example value)
                ProductOrCatalogNumber = "12345",
                // Labeler identification code (example value)
                LabelerIdentificationCode = "A999",
                // Unit of measure identifier
                UnitOfMeasureID = 1
            },
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                // Expiry date set to 30 days from now
                ExpiryDate = DateTime.Now.AddDays(30),
                // Expiry date format (MMDDYY)
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                // Quantity of items
                Quantity = 30,
                // Lot number (example value)
                LotNumber = "LOT123",
                // Serial number (example value)
                SerialNumber = "SERIAL123",
                // Date of manufacture set to 10 days ago
                DateOfManufacture = DateTime.Now.AddDays(-10)
            }
        };

        // ------------------------------------------------------------
        // 2. Generate the QR code image using ComplexBarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Optional: set a high error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // --------------------------------------------------------
            // 3. Save the barcode to a PNG file
            // --------------------------------------------------------
            string outputFile = "hibc_combined_qr.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode saved to {outputFile}");
        }
    }
}