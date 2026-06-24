using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC LIC barcode with secondary and additional data
/// using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a HIBC LIC barcode with secondary data and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare secondary data for HIBC LIC barcode
        // ------------------------------------------------------------
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Use Code128 variant of HIBC LIC
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            // Required link character between primary and secondary data
            LinkCharacter = '+',
            // Populate secondary data fields
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT12345",
                SerialNumber = "SN98765"
            }
        };

        // ------------------------------------------------------------
        // Generate the barcode with custom colors
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            // Set background color to light gray
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Save the barcode image to file
            generator.Save("hibc_lic.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC LIC barcode generated: hibc_lic.png");
    }
}