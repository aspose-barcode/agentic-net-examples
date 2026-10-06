// Title: Generate HIBC Aztec LIC barcode using Aspose.BarCode
// Description: Demonstrates how to set the barcode type to Aztec and generate a HIBC Aztec LIC barcode with primary data, saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of HIBCLICPrimaryDataCodetext, ComplexBarcodeGenerator, and EncodeTypes to create HIBC Aztec LIC barcodes. Developers commonly need to generate HIBC-compliant barcodes for healthcare labeling, requiring specific symbology and data fields. The snippet shows typical steps: configuring barcode type, populating primary data, and saving the image.
// Prompt: Set the BarcodeType property to Aztec before assigning a HIBCLICPrimaryDataCodetext for generation.
// Tags: aztec, hibc, lic, barcode, generation, png, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC Aztec LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates primary data, sets barcode type to Aztec, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize primary data codetext for HIBC Aztec LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext();

        // Set the barcode symbology to Aztec (HIBC Aztec LIC)
        primaryCodetext.BarcodeType = EncodeTypes.HIBCAztecLIC;

        // Populate the required primary data fields
        primaryCodetext.Data = new PrimaryData
        {
            LabelerIdentificationCode = "A999",   // Identifier of the labeler
            ProductOrCatalogNumber = "12345",     // Product or catalog number
            UnitOfMeasureID = 1                   // Unit of measure identifier
        };

        // Generate the barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Save the generated barcode as a PNG file
            generator.Save("hibc_aztec.png", BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("HIBC Aztec barcode generated: hibc_aztec.png");
    }
}