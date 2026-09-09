// Title: Generate HIBCLIC Secondary DataMatrix Barcode with Expiration Date
// Description: Demonstrates how to create a HIBCLIC secondary and additional data codetext, set an expiration date, and generate a DataMatrix barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of HIBCLICDataMatrixLIC symbology via ComplexBarcodeGenerator. Developers learn to configure secondary data fields such as expiry date and lot number, adjust barcode dimensions, and save the result as PNG. Ideal for healthcare and logistics applications that require HIBC-compliant barcodes with supplemental information.
// Prompt: Create a HIBCLICSecondaryAndAdditionalDataCodetext, set expiration date, and generate a DataMatrix barcode.
// Tags: hibc, datamatrix, secondary-data, expiration-date, complexbarcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBCLIC secondary and additional data DataMatrix barcode with an expiration date.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, configures barcode data, generates the barcode image, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine and create the output directory if it does not exist
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the complex codetext with secondary data (expiry date, lot number) for HIBC DataMatrix LIC
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now,
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                LotNumber = "LOT123"
            }
        };

        // Define the full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "HIBCLIC_Secondary_DataMatrix.png");

        // Generate the barcode using ComplexBarcodeGenerator and save it as PNG
        using (var gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 10f; // Set module size
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}