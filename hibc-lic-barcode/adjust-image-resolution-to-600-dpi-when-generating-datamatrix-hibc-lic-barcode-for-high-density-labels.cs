// Title: Generate HIBC DataMatrix LIC Barcode at 600 DPI
// Description: Creates a HIBC DataMatrix LIC barcode with secondary data and saves it as a 600 DPI PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator together with HIBCLICSecondaryAndAdditionalDataCodetext to produce high‑density HIBC LIC DataMatrix barcodes. Typical use cases include pharmaceutical labeling and inventory tracking where secondary information (lot, serial, dates) must be encoded at high resolution for small‑format labels.
// Prompt: Adjust the image resolution to 600 DPI when generating a DataMatrix HIBC LIC barcode for high‑density labels.
// Tags: datamatrix, hibc, lic, generation, resolution, png, complexbarcodegenerator, hibclicsecondaryandadditionaldatacodetext

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a HIBC DataMatrix LIC barcode with secondary data
/// and saves the image at 600 DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares secondary data, configures the generator,
    /// and saves the high‑resolution barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary and additional data required for a HIBC LIC DataMatrix barcode
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SN456",
                ExpiryDate = DateTime.Today.AddMonths(6),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                DateOfManufacture = DateTime.Today
            }
        };

        // Generate the barcode with a high image resolution (600 DPI)
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            // Set the resolution property to 600 DPI
            generator.Parameters.Resolution = 600;

            // Save the generated barcode as a PNG file
            generator.Save("HIBC_DataMatrix_600dpi.png");
        }
    }
}