// Title: Generate HIBC Code 128 LIC barcode with primary data and save as BMP
// Description: Demonstrates creating a HIBCLICPrimaryDataCodetext, setting the labeler ID, and exporting the barcode to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with HIBC Code 128 LIC symbology, illustrating how to populate primary data fields such as product number and labeler identification. Developers working with healthcare or logistics barcodes can reference this pattern for creating compliant HIBC barcodes and saving them in various image formats.
// Prompt: Create a HIBCLICPrimaryDataCodetext, set labeler ID, and generate a BMP image of the barcode.
// Tags: hibc, code128lic, complexbarcode, barcode generation, bmp, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

namespace BarcodeSample
{
    /// <summary>
    /// Entry point for the barcode generation sample.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Creates a HIBCLICPrimaryDataCodetext, generates a barcode, and saves it as a BMP file.
        /// </summary>
        static void Main()
        {
            // Initialize primary data codetext for HIBC Code 128 LIC barcode
            var primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                // Select the HIBC Code 128 LIC symbology
                BarcodeType = EncodeTypes.HIBCCode128LIC,
                // Populate the required primary data fields
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = "12345",
                    LabelerIdentificationCode = "A999", // labeler ID
                    UnitOfMeasureID = 1 // optional, example value
                }
            };

            // Use ComplexBarcodeGenerator to create the barcode image
            using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                // Define output file path
                string outputPath = "hibc_primary.bmp";

                // Save the generated barcode as a BMP image
                generator.Save(outputPath, BarCodeImageFormat.Bmp);

                // Inform the user where the file was saved
                Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}