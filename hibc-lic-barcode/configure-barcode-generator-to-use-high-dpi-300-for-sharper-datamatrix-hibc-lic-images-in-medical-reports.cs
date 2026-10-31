// Title: Generate high‑resolution DataMatrix HIBC LIC barcode
// Description: Demonstrates configuring Aspose.BarCode to produce a 300 DPI DataMatrix HIBC LIC image, suitable for clear printing in medical reports.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, set resolution and module size, and save the image. Developers creating medical labeling or inventory systems often need high‑resolution DataMatrix barcodes for compliance and readability.
// Prompt: Configure the barcode generator to use high DPI (300) for sharper DataMatrix HIBC LIC images in medical reports.
// Tags: datamatrix, hibc, lic, highdpi, barcode, generation, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a high‑resolution DataMatrix HIBC LIC barcode
/// using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a temporary folder,
    /// and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLIC_Demo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "HIBCLIC_DataMatrix.png");

        // Prepare primary data required for a HIBC LIC barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC, // Specify HIBC DataMatrix LIC symbology
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Example product number
                LabelerIdentificationCode = "A999", // Example labeler ID
                UnitOfMeasureID = 1                 // Example unit of measure
            }
        };

        // Generate the barcode with a high resolution (300 DPI) for sharper output
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.Resolution = 300f;               // Set DPI
            gen.Parameters.Barcode.XDimension.Pixels = 10f; // Optional: define module size in pixels
            gen.Save(outputPath);                            // Save image to file
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"HIBC LIC DataMatrix barcode saved to: {outputPath}");
    }
}