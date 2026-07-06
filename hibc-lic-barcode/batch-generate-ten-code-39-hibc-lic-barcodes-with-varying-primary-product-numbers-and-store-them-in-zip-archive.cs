// Title: Batch Generation of Code 39 HIBC LIC Barcodes and Zipping
// Description: Generates ten Code 39 HIBC LIC barcodes with unique primary product numbers, saves them as PNG images, and packages them into a zip archive.
// Category-Description: This example demonstrates Aspose.BarCode's complex barcode generation for HIBC Code 39 LIC symbology using the ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext classes. It shows how to create multiple barcodes in a batch, customize primary data fields, and archive the results. Developers working with healthcare or logistics labeling often need to produce HIBC‑compliant barcodes programmatically and bundle them for distribution.
// Prompt: Batch generate ten Code 39 HIBC LIC barcodes with varying primary product numbers and store them in a zip archive.
// Tags: barcode symbology, batch generation, png, zip, complexbarcode, hibc, code39

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of Code 39 HIBC LIC barcodes and archiving them into a zip file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them as PNG files, and creates a zip archive.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Loop to generate 10 distinct Code 39 HIBC LIC barcodes.
        for (int i = 1; i <= 10; i++)
        {
            // Prepare primary data with a unique product number for each barcode.
            var primaryData = new PrimaryData
            {
                ProductOrCatalogNumber = $"P{i:D4}", // e.g., P0001, P0002, ...
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            };

            // Build the complex codetext specifying the HIBC Code 39 LIC symbology.
            var codetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode39LIC,
                Data = primaryData
            };

            // Determine the file path for the current barcode image.
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");

            // Generate the barcode image and save it to the file system.
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Save(filePath);
            }

            Console.WriteLine($"Generated barcode {i}: {filePath}");
        }

        // Path for the resulting zip archive containing all barcode images.
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes.zip");

        // Create the zip archive and add each PNG file from the output directory.
        using (var zipStream = new FileStream(zipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string file in Directory.GetFiles(outputDir, "*.png"))
            {
                string entryName = Path.GetFileName(file);
                archive.CreateEntryFromFile(file, entryName);
                Console.WriteLine($"Added {entryName} to zip archive.");
            }
        }

        Console.WriteLine($"All barcodes have been zipped to: {zipPath}");
    }
}