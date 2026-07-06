// Title: Generate HIBC Aztec barcode with primary data
// Description: Demonstrates how to create a HIBC Aztec (HIBCAztecLIC) barcode by setting the barcode type before populating primary data fields, then generating and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext to produce HIBC-compliant barcodes. Developers often need to generate industry-specific barcodes (e.g., HIBC) with custom data for labeling and tracking, using EncodeTypes and image output classes. The snippet illustrates typical steps: configure barcode type, fill required data, generate image, and save to file.
// Prompt: Set the BarcodeType property to Aztec before assigning a HIBCLICPrimaryDataCodetext for generation.
// Tags: aztec, hibc, barcode generation, png, complexbarcodegenerator, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC Aztec barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates primary data, sets barcode type, generates the barcode image, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a HIBCLICPrimaryDataCodetext instance to hold barcode configuration and data
        var primaryCodetext = new HIBCLICPrimaryDataCodetext();

        // Set the barcode type to Aztec (HIBCAztecLIC) BEFORE assigning primary data
        primaryCodetext.BarcodeType = EncodeTypes.HIBCAztecLIC;

        // Populate the required primary data fields for the HIBC barcode
        primaryCodetext.Data = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",   // Example product/catalog number
            LabelerIdentificationCode = "A999", // Example labeler ID
            UnitOfMeasureID = 1                 // Example unit of measure identifier
        };

        // Initialize the ComplexBarcodeGenerator with the configured primary codetext
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated image to a PNG file
                bitmap.Save("hibc_aztec.png", ImageFormat.Png);
            }
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC Aztec barcode generated: hibc_aztec.png");
    }
}