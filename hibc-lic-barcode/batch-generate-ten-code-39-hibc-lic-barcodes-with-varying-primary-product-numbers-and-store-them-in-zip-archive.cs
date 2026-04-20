using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        string zipPath = "HIBC_LIC_Code39.zip";

        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        using (FileStream zipFile = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
        {
            for (int i = 1; i <= 10; i++)
            {
                var primary = new PrimaryData();
                primary.ProductOrCatalogNumber = $"P{i:D4}";
                primary.LabelerIdentificationCode = "A999";
                primary.UnitOfMeasureID = 1;

                var codetext = new HIBCLICPrimaryDataCodetext();
                codetext.Data = primary;
                codetext.BarcodeType = EncodeTypes.HIBCCode39LIC;

                using (var generator = new ComplexBarcodeGenerator(codetext))
                {
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        var entry = archive.CreateEntry($"barcode_{i:D2}.png");
                        using (var entryStream = entry.Open())
                        {
                            ms.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Generated zip archive: {zipPath}");
    }
}