using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC DataMatrix barcode that includes secondary and additional data,
/// saved at a resolution of 600 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Prepares barcode data, configures the generator, saves the image, and outputs a confirmation message.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare secondary‑and‑additional data for the HIBC LIC DataMatrix barcode
        // ------------------------------------------------------------
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",                                 // Lot identifier
            SerialNumber = "SN001",                               // Serial number
            ExpiryDate = DateTime.Today.AddMonths(6),             // Expiration date (6 months from today)
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,         // Date format for the expiry date
            Quantity = 10,                                        // Quantity of items
            DateOfManufacture = DateTime.Today                    // Manufacturing date
        };

        // ------------------------------------------------------------
        // 2. Create the complex codetext object that combines the barcode type,
        //    link character, and the secondary data defined above
        // ------------------------------------------------------------
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC, // Use HIBC DataMatrix LIC symbology
            LinkCharacter = '+',                         // Required link character for HIBC
            Data = secondaryData                         // Attach the secondary data
        };

        // ------------------------------------------------------------
        // 3. Generate the barcode with a resolution of 600 DPI and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set both horizontal and vertical resolution to 600 DPI
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode image; PNG is the default format
            generator.Save("HIBC_DataMatrix_600DPI.png");
        }

        // ------------------------------------------------------------
        // 4. Inform the user that the barcode has been generated
        // ------------------------------------------------------------
        Console.WriteLine("Barcode generated with 600 DPI resolution: HIBC_DataMatrix_600DPI.png");
    }
}