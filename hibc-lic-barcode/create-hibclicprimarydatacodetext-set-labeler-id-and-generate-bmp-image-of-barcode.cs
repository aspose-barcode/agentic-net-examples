// Title: Generate HIBC QR LIC Barcode with Primary Data and Save as BMP
// Description: This example creates a HIBCLICPrimaryDataCodetext, sets the labeler identification code, and generates a BMP image of the resulting barcode.
// Category-Description: The sample belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use ComplexBarcodeGenerator with HIBC QR LIC symbology. It shows the use of HIBCLICPrimaryDataCodetext, PrimaryData, and EncodeTypes to encode healthcare product information. Developers often need such examples for creating compliant HIBC barcodes for labeling, tracking, and data exchange.
// Prompt: Create a HIBCLICPrimaryDataCodetext, set labeler ID, and generate a BMP image of the barcode.
// Tags: hibc, qr, lic, primary-data, barcode, generation, bmp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a HIBC QR LIC barcode with primary data and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the codetext, configures the generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.bmp");

        // Initialize the primary data codetext for HIBC QR LIC.
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext();
        codetext.BarcodeType = EncodeTypes.HIBCQRLIC;
        codetext.Data = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999", // labeler ID
            UnitOfMeasureID = 1
        };

        // Create the complex barcode generator with the prepared codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set the X-dimension (module size) to 10 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Save the generated barcode as a BMP image.
            generator.Save(outputFile);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}