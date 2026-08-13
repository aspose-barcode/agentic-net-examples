// Title: Encode HIBC QR Code with Combined Primary and Secondary Fields
// Description: Demonstrates how to create a HIBC QR (LIC) barcode by combining primary and secondary data fields and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes. Developers often need to generate HIBC LIC QR codes for product labeling, requiring both primary product information and additional lot or date details. The snippet illustrates typical steps: configuring data, setting visual parameters, and exporting the barcode image.
// Prompt: Encode combined primary and secondary fields using HIBCLICCombinedCodetext and output a QR code file.
// Tags: qr code, hibc, combined codetext, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a HIBC QR (LIC) barcode that combines primary and secondary data fields,
/// then saves the resulting image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the combined codetext, configures visual parameters,
    /// and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Assemble combined primary and secondary data for a HIBC LIC QR code
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            // Specify the QR code symbology for HIBC LIC
            BarcodeType = EncodeTypes.HIBCQRLIC,

            // Populate primary product information
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            },

            // Populate secondary and additional information such as dates, lot, and serial numbers
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now,
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 30,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Now
            }
        };

        // Create the barcode generator using the combined codetext
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Optional: customize barcode and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated QR code as a PNG file
            generator.Save("hibc_qr.png");
        }

        Console.WriteLine("HIBC QR code generated: hibc_qr.png");
    }
}