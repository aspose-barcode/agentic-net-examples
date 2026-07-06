// Title: Generate high-resolution HIBC DataMatrix LIC barcode
// Description: Demonstrates configuring Aspose.BarCode to produce a 300 DPI DataMatrix HIBC LIC barcode, useful for clear printing in medical reports.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on HIBC (Health Industry Bar Code) symbology. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and SecondaryAndAdditionalData classes to embed lot and serial numbers. Developers creating medical or pharmaceutical labels often need high-resolution barcodes for accurate scanning and regulatory compliance.
// Prompt: Configure the barcode generator to use high DPI (300) for sharper DataMatrix HIBC LIC images in medical reports.
// Tags: datamatrix, hibc, lic, highdpi, barcode, generation, aspnet, aspnetcore

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑resolution HIBC DataMatrix LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates secondary data, builds complex codetext, sets DPI to 300, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data for HIBC LIC DataMatrix barcode
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN001"
        };

        // Configure complex codetext with required link character
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode with high DPI (300)
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the resolution (dots per inch) to 300 for sharper output
            generator.Parameters.Resolution = 300f;
            // Save the generated barcode image as PNG
            generator.Save("hibc_datamatrix_lic.png");
        }

        // Inform the user that generation succeeded
        Console.WriteLine("HIBC DataMatrix LIC barcode generated with 300 DPI.");
    }
}