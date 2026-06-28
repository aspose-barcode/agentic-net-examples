using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC DataMatrix LIC barcode with secondary and additional data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a HIBC DataMatrix LIC barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "hibc_datamatrix.png";

        // Prepare secondary and additional data required for the HIBC LIC DataMatrix barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",                     // Lot number of the product
            SerialNumber = "SN001",                   // Serial number of the product
            ExpiryDate = DateTime.Today.AddMonths(6), // Expiration date (6 months from today)
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY, // Date format for the expiry date
            Quantity = 10,                            // Quantity of items
            DateOfManufacture = DateTime.Today        // Manufacturing date (today)
        };

        // Configure the complex codetext that includes barcode type, link character, and the secondary data.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC, // Specify HIBC DataMatrix LIC barcode type
            LinkCharacter = '+',                         // Link character used in the codetext
            Data = secondaryData                         // Attach the prepared secondary data
        };

        // Create a ComplexBarcodeGenerator with the configured codetext.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified file (PNG format by default).
            generator.Save(outputPath);
        }

        // Output the full path of the saved barcode image to the console.
        Console.WriteLine($"HIBC DataMatrix LIC barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}