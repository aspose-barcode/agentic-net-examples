using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Directory to store individual barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate 10 HIBC Code39 barcodes with varying primary product numbers
        for (int i = 1; i <= 10; i++)
        {
            // Prepare primary data
            var primaryCodetext = new HIBCLICPrimaryDataCodetext
            {
                BarcodeType = EncodeTypes.HIBCCode39LIC,
                Data = new PrimaryData
                {
                    ProductOrCatalogNumber = $"PN{i:D4}",          // Varying product number
                    LabelerIdentificationCode = "A999",           // Example labeler ID
                    UnitOfMeasureID = 1                           // Example UOM
                }
            };

            // Generate barcode image and save to file
            string filePath = Path.Combine(outputDir, $"HIBC_Code39_{i:D2}.png");
            using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
            {
                generator.Save(filePath);
            }

            Console.WriteLine($"Generated barcode {i} -> {filePath}");
        }

        // Create zip archive containing all generated images
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBC_Code39_Barcodes.zip");
        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (string file in Directory.GetFiles(outputDir, "*.png"))
            {
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }

        Console.WriteLine($"All barcodes zipped to: {zipPath}");
    }
}