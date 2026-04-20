using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace SwissPostParcelBatch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Output folder for barcode images
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // CSV index file path
            string csvPath = Path.Combine(outputFolder, "index.csv");

            // Prepare a list of identifiers and corresponding code texts
            List<(string Identifier, string CodeText)> items = new List<(string, string)>();
            for (int i = 1; i <= 5; i++)
            {
                string identifier = $"ID{i:D3}";
                // Sample code text for Swiss Post Parcel Additional Service (placeholder format)
                string codeText = $"SP{i:D5}";
                items.Add((identifier, codeText));
            }

            // Write CSV header and generate barcodes
            using (var writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("Identifier,CodeText,FileName");

                foreach (var item in items)
                {
                    string fileName = $"{item.Identifier}.png";
                    string filePath = Path.Combine(outputFolder, fileName);

                    // Generate Swiss Post Parcel barcode
                    using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, item.CodeText))
                    {
                        // Save the barcode image
                        generator.Save(filePath);
                    }

                    // Write CSV entry
                    writer.WriteLine($"{item.Identifier},{item.CodeText},{fileName}");
                }
            }

            Console.WriteLine($"Generated {items.Count} barcodes in folder: {outputFolder}");
            Console.WriteLine($"CSV index created at: {csvPath}");
        }
    }
}