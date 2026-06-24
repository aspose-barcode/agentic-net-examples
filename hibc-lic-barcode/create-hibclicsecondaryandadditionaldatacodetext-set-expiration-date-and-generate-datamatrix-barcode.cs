using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a HIBC LIC DataMatrix barcode with secondary and additional data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode with secondary data and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Initialize secondary and additional data codetext for a HIBC LIC DataMatrix barcode
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Set the barcode symbology to HIBC DataMatrix LIC
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            // Define the mandatory link character; default is '+'
            LinkCharacter = '+',
            // Populate the secondary data fields
            Data = new SecondaryAndAdditionalData
            {
                // Set the expiration date (example: 30 days from the current date)
                ExpiryDate = DateTime.Now.AddDays(30),
                // Choose the date format for the expiration date (e.g., MMDDYY)
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                // Optional: specify lot number
                LotNumber = "LOT123",
                // Optional: specify serial number
                SerialNumber = "SN001"
            }
        };

        // Create a barcode generator using the configured codetext
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Save the generated barcode as a PNG image file
            generator.Save("hibc_secondary_datamatrix.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC LIC DataMatrix barcode generated: hibc_secondary_datamatrix.png");
    }
}