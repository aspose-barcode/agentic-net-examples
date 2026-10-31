// Title: Batch generate HIBC Code 39 LIC barcodes and zip them
// Description: Demonstrates how to create ten HIBC Code 39 LIC barcodes with different product numbers using Aspose.BarCode and store them in a zip archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation with HIBCLICPrimaryDataCodetext and encoding types. It shows typical use cases such as generating multiple barcodes programmatically, customizing barcode parameters, and packaging the results for distribution. Developers working with healthcare or logistics labeling often need to produce batches of HIBC‑compliant barcodes and archive them, making this pattern a common reference.
// Prompt: Batch generate ten Code 39 HIBC LIC barcodes with varying primary product numbers and store them in a zip archive.
// Tags: barcode, code39, hibc, lic, batch, zip, aspose.barcode, generation, complexbarcode

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a batch of HIBC Code 39 LIC barcodes with unique product numbers,
/// saves each as a PNG file, and packages them into a zip archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary directories, generates barcodes,
    /// and creates a zip file containing all generated images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "HIBCLICBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        List<string> files = new List<string>();

        // Generate ten barcodes with sequential product numbers.
        for (int i = 1; i <= 10; i++)
        {
            // Build a product number like PN0001, PN0002, etc.
            string productNumber = $"PN{i:D4}";

            // Configure primary HIBC LIC data.
            var primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode39LIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = productNumber,
                    LabelerIdentificationCode = "A999",
                    UnitOfMeasureID = 1
                }
            };

            // Generate the barcode image.
            using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                // Set the X-dimension (module width) to 5 pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 5f;

                // Define the output file path.
                string filePath = Path.Combine(tempDir, $"barcode_{i}.png");

                // Save the barcode as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
                files.Add(filePath);
            }
        }

        // Define the zip archive path in the temp folder.
        string zipPath = Path.Combine(Path.GetTempPath(), "HIBCLICBatch.zip");

        // Remove any existing zip file with the same name.
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a zip archive and add all generated PNG files.
        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (var file in files)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }

        // Inform the user about the successful operation.
        Console.WriteLine($"Generated {files.Count} HIBC Code 39 LIC barcodes and saved to zip: {zipPath}");
    }
}