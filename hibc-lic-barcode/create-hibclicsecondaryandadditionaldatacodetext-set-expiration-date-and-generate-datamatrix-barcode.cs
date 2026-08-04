// Title: Generate HIBCLIC DataMatrix Barcode with Expiration Date
// Description: Demonstrates how to create a HIBCLIC secondary and additional data codetext, set an expiration date, and generate a DataMatrix barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICSecondaryAndAdditionalDataCodetext to produce HIBC‑compliant DataMatrix barcodes. Typical use cases include labeling pharmaceutical or medical devices where secondary data such as expiry date, lot number, and serial number must be encoded. Developers often need to combine HIBC standards with Aspose.BarCode's EncodeTypes and complex data structures to meet regulatory labeling requirements.
// Prompt: Create a HIBCLICSecondaryAndAdditionalDataCodetext, set expiration date, and generate a DataMatrix barcode.
// Tags: hibc, datamatrix, barcode, generation, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a HIBCLIC secondary and additional data codetext,
/// sets an expiration date, and generates a DataMatrix barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the complex codetext, generates the barcode,
    /// and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize secondary and additional data codetext for HIBC LIC
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Specify the barcode symbology (DataMatrix) for HIBC LIC
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            // The link character is mandatory; '+' is the default value
            LinkCharacter = '+',
            // Populate secondary data such as expiry date, quantity, lot number, and serial number
            Data = new SecondaryAndAdditionalData
            {
                // Set expiration date to 30 days from now
                ExpiryDate = DateTime.Now.AddDays(30),
                // Define the date format (MMDDYY) required by HIBC
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                // Example quantity value
                Quantity = 10,
                // Example lot number
                LotNumber = "LOT123",
                // Example serial number
                SerialNumber = "SN001"
            }
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Save the barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Write the PNG bytes to a file on disk
                File.WriteAllBytes("hibc_datamatrix.png", ms.ToArray());
            }
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC LIC DataMatrix barcode generated: hibc_datamatrix.png");
    }
}