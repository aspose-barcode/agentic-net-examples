// Title: Configure Image Size for HIBC DataMatrix LIC Barcode
// Description: Demonstrates setting the barcode image dimensions to 300 × 150 pixels before generating a DataMatrix HIBC LIC barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and SecondaryAndAdditionalData classes to create HIBC‑LIC DataMatrix barcodes. Developers often need to control image size, format, and secondary data when integrating barcodes into packaging, labeling, or inventory systems.
// Prompt: Configure barcode image size to 300 × 150 pixels before rendering a DataMatrix HIBC LIC barcode.
// Tags: datamatrix, hibc, lic, image-size, generation, png, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC DataMatrix LIC barcode with a custom image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares secondary data, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data for the HIBC LIC DataMatrix barcode (e.g., lot number).
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };

        // Create complex codetext that combines barcode type, link character, and secondary data.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the desired image dimensions (300 × 150 pixels).
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode as a PNG file.
            generator.Save("HIBC_DataMatrix_LIC.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated and saved as HIBC_DataMatrix_LIC.png");
    }
}