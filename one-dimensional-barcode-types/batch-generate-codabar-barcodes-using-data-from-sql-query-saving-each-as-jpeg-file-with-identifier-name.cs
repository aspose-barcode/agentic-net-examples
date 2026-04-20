using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        var items = new List<(string Id, string Code)>
        {
            ("Sample1", "A12345B"),
            ("Sample2", "C67890D"),
            ("Sample3", "E11111F")
        };

        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (var item in items)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, item.Code))
            {
                string filePath = Path.Combine(outputFolder, $"{item.Id}.jpeg");
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Saved barcode for {item.Id} to {filePath}");
            }
        }
    }
}