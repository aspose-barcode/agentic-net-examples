// Title: Batch generate HIBC Code 39 LIC barcodes and archive them in a ZIP file
// Description: Demonstrates generating ten HIBC Code 39 LIC barcodes with unique product numbers, saving each as a PNG image, and packaging the images into a zip archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator class. It showcases typical use cases such as batch barcode production for inventory labeling, where developers need to programmatically create multiple barcodes with varying data and bundle the results for distribution or storage.
// Prompt: Batch generate ten Code 39 HIBC LIC barcodes with varying primary product numbers and store them in a zip archive.
// Tags: barcode symbology, generation, zip, code39, hibc, lic, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates batch generation of HIBC Code 39 LIC barcodes and archiving them.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcode images, stores them, and zips the collection.
    /// </summary>
    static void Main()
    {
        // Directory to store individual barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate 10 HIBC Code 39 LIC barcodes with different primary product numbers
        for (int i = 1; i <= 10; i++)
        {
            // Example primary product number (e.g., "P00001", "P00002", ...)
            string productNumber = $"P{i:D5}";

            // Build the complex codetext for HIBC Code 39 LIC
            var complexCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode39LIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = productNumber,
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Generate the barcode image and save it as PNG
            string imagePath = Path.Combine(outputDir, $"barcode{i}.png");
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                generator.Save(imagePath);
            }
        }

        // Create a ZIP archive containing all generated barcode images
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes.zip");
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        using (var zipStream = new FileStream(zipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string filePath in Directory.GetFiles(outputDir, "*.png"))
            {
                string entryName = Path.GetFileName(filePath);
                archive.CreateEntryFromFile(filePath, entryName);
            }
        }

        // Optional: clean up the temporary image files
        // foreach (string filePath in Directory.GetFiles(outputDir, "*.png"))
        // {
        //     File.Delete(filePath);
        // }
        // Directory.Delete(outputDir);
    }
}