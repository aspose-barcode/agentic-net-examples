using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLICBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        List<string> generatedFiles = new List<string>();

        // Generate 10 HIBC Code 39 LIC barcodes with varying product numbers
        for (int i = 1; i <= 10; i++)
        {
            // Prepare primary data
            HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext();
            complexCodetext.BarcodeType = EncodeTypes.HIBCCode39LIC;
            complexCodetext.Data = new PrimaryData
            {
                ProductOrCatalogNumber = $"P{i:D4}",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            };

            string filePath = Path.Combine(tempFolder, $"HIBCLIC_Code39_{i}.png");

            // Generate and save barcode
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 10;
                generator.Save(filePath);
            }

            generatedFiles.Add(filePath);
        }

        // Create zip archive containing the generated barcodes
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLIC_Code39_Batch.zip");
        using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (string file in generatedFiles)
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }

        Console.WriteLine($"Generated {generatedFiles.Count} barcodes in folder: {tempFolder}");
        Console.WriteLine($"Zip archive created at: {zipPath}");
    }
}