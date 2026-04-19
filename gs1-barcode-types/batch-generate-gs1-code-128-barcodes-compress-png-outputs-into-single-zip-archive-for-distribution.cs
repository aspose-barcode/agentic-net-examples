using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BatchGs1Code128Generator
{
    class Program
    {
        static void Main()
        {
            // Prepare sample GS1 Code 128 data (must contain parentheses for AI)
            List<string> codeTexts = new List<string>
            {
                "(01)12345678901231",
                "(01)98765432109876(10)ABC123",
                "(01)55555555555555(21)XYZ789",
                "(01)11111111111111(17)210101",
                "(01)22222222222222(10)LOT001"
            };

            // Output folder for PNG images
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // List to keep generated file paths
            List<string> generatedFiles = new List<string>();

            // Generate PNG barcodes
            for (int i = 0; i < codeTexts.Count; i++)
            {
                string codeText = codeTexts[i];
                string fileName = $"barcode_{i + 1}.png";
                string filePath = Path.Combine(outputFolder, fileName);

                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Optional: set image resolution or dimensions if needed
                    // generator.Parameters.Resolution = 300;

                    // Save as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                generatedFiles.Add(filePath);
            }

            // Create ZIP archive containing all PNG files
            string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "Gs1Code128Barcodes.zip");
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (string file in generatedFiles)
                {
                    archive.CreateEntryFromFile(file, Path.GetFileName(file));
                }
            }

            // Optionally clean up individual PNG files
            foreach (string file in generatedFiles)
            {
                File.Delete(file);
            }

            // Indicate completion
            Console.WriteLine($"Generated {generatedFiles.Count} GS1 Code 128 barcodes and compressed into '{zipPath}'.");
        }
    }
}