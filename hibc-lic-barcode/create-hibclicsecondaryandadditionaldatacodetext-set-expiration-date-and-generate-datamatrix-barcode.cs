// Title: Generate HIBC LIC Secondary Data DataMatrix Barcode
// Description: Creates a HIBCLICSecondaryAndAdditionalDataCodetext with an expiration date and encodes it as a DataMatrix barcode.
// Category-Description: This example demonstrates the use of Aspose.BarCode's ComplexBarcodeGenerator to produce HIBC (Health Industry Bar Code) LIC (Labeler Identification Code) barcodes with secondary and additional data. It showcases key API classes such as HIBCLICSecondaryAndAdditionalDataCodetext, SecondaryAndAdditionalData, and ComplexBarcodeGenerator, which are commonly used for healthcare labeling, inventory tracking, and regulatory compliance. Developers looking to embed lot numbers, serial numbers, and expiry dates into DataMatrix barcodes will find this pattern useful.
// Prompt: Create a HIBCLICSecondaryAndAdditionalDataCodetext, set expiration date, and generate a DataMatrix barcode.
// Tags: hibc, datamatrix, secondary data, expiration date, complexbarcode, generation, png

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to create a HIBCLICSecondaryAndAdditionalDataCodetext,
/// set an expiration date, and generate a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the secondary data object,
    /// configures the barcode generator, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Build secondary-and-additional data codetext for a HIBC LIC DataMatrix barcode
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC, // Specify DataMatrix symbology for HIBC LIC
            LinkCharacter = '+', // Required link character for HIBC format
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now.AddDays(30), // Set expiration date 30 days from now
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY, // Choose MMDDYY date format
                LotNumber = "LOT123", // Example lot number
                SerialNumber = "SERIAL123" // Example serial number
            }
        };

        // Initialize the complex barcode generator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            // Optionally define the output image dimensions (in points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the generated barcode as a PNG file
            generator.Save("hibc_secondary_datamatrix.png");
        }

        // Inform the user that the barcode has been created successfully
        Console.WriteLine("HIBC LIC secondary-data DataMatrix barcode generated successfully.");
    }
}