// Title: Add Quiet Zone to HIBC DataMatrix LIC Barcode
// Description: Demonstrates how to generate a HIBC DataMatrix LIC barcode with a ten‑module quiet zone using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to work with the ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and SecondaryAndAdditionalData classes to create HIBC‑compliant DataMatrix barcodes. Developers often need to adjust quiet zones, module size, and padding to satisfy printing standards and regulatory requirements.
// Prompt: Add a quiet zone of ten modules around a DataMatrix HIBC LIC barcode to meet printing standards.
// Tags: datamatrix, hibc, quietzone, png, generation, complexbarcode, aspose.barcodes, secondarydata

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a HIBC DataMatrix LIC barcode with a ten‑module quiet zone and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares secondary data, configures barcode parameters, adds a quiet zone,
    /// and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data for the HIBC LIC DataMatrix barcode (lot and serial numbers).
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN001"
        };

        // Create the complex codetext object that combines the barcode type, link character, and secondary data.
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Initialize the complex barcode generator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Define the module size (XDimension) – 2 points per module.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate quiet zone size: ten modules on each side.
            float quietZone = generator.Parameters.Barcode.XDimension.Point * 10f;

            // Apply the quiet zone to all four padding sides.
            generator.Parameters.Barcode.Padding.Left.Point   = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point    = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point  = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Enable automatic sizing using interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode as a PNG file.
            generator.Save("hibc_datamatrix.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("HIBC DataMatrix LIC barcode generated with a 10‑module quiet zone.");
    }
}