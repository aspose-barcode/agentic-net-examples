// Title: Generate Code 39 HIBC LIC barcode with primary data
// Description: Demonstrates creating a HIBC Code 39 LIC barcode using primary data and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC Code 39 LIC. It showcases the use of ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext classes to encode product information. Developers often need to generate HIBC-compliant barcodes for healthcare labeling and inventory tracking.
// Prompt: Generate a Code 39 HIBC LIC barcode with primary data and save it as a PNG image.
// Tags: code39, hibc, lic, barcode generation, png, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generation of a HIBC Code 39 LIC barcode with primary data and saving it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Creates primary data, generates the barcode, and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Define the primary data required for a HIBC Code 39 LIC barcode
            var primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode39LIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = "12345",   // Product or catalog identifier
                    LabelerIdentificationCode = "A999", // Labeler ID assigned by HIBC
                    UnitOfMeasureID = 1                 // Unit of measure (e.g., each)
                }
            };

            // Initialize the complex barcode generator with the primary data
            using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                // Save the generated barcode; the file extension determines the PNG format
                generator.Save("hibc_primary.png");
            }

            // Inform the user that the barcode image has been created
            Console.WriteLine("HIBC Code39 LIC barcode generated: hibc_primary.png");
        }
    }
}