// Title: Generate DataMatrix barcode with HIBCLIC secondary and additional data
// Description: Demonstrates how to build a HIBCLIC secondary and additional data codetext, set an expiration date, and render it as a DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of HIBCLICSecondaryAndAdditionalDataCodetext, EncodeTypes, and BarcodeGenerator classes. Typical use cases include encoding pharmaceutical product information such as lot numbers, serial numbers, and expiry dates into a DataMatrix symbol for labeling and tracking. Developers often need to construct custom codetext, configure barcode parameters, and export the result to common image formats.
// Prompt: Create a HIBCLICSecondaryAndAdditionalDataCodetext, set expiration date, and generate a DataMatrix barcode.
// Tags: data matrix, hibc lic, secondary data, expiration date, aspose.barcode, barcode generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a DataMatrix barcode using HIBCLIC secondary and additional data codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the codetext, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path (PNG) in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLIC_DataMatrix.png");

        // ------------------------------------------------------------
        // Create HIBC LIC secondary and additional data codetext
        // ------------------------------------------------------------
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Set the barcode type to HIBC QR LIC (required for this codetext)
            BarcodeType = EncodeTypes.HIBCQRLIC,
            // Initialize the data container for secondary and additional fields
            Data = new SecondaryAndAdditionalData()
        };

        // Populate secondary and additional data fields
        complexCodetext.Data.ExpiryDate = DateTime.Now;                     // Expiration date
        complexCodetext.Data.ExpiryDateFormat = HIBCLICDateFormat.MMDDYY; // Date format
        complexCodetext.Data.Quantity = 30;                                 // Quantity
        complexCodetext.Data.LotNumber = "LOT123";                          // Lot number
        complexCodetext.Data.SerialNumber = "SERIAL123";                    // Serial number
        complexCodetext.Data.DateOfManufacture = DateTime.Now;             // Manufacture date
        complexCodetext.LinkCharacter = '+';                               // Link character

        // Retrieve the fully constructed codetext string
        string codeText = complexCodetext.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Generate a DataMatrix barcode using the constructed codetext
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set visual parameters: module size and matrix version
            generator.Parameters.Barcode.XDimension.Pixels = 5f;
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}