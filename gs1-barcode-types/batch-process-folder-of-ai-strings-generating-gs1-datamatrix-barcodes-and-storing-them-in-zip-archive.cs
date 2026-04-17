using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define input and output locations
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputAI");
        string outputZipPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes.zip");

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed sample AI strings if folder is empty (up to 5 samples)
        string[] sampleAi = new[]
        {
            "(01)12345678901231",
            "(10)ABC123",
            "(21)9876543210",
            "(01)95012345678903(10)BATCH01",
            "(01)95012345678903(21)LOT12345"
        };

        var txtFiles = Directory.GetFiles(inputFolder, "*.txt");
        if (txtFiles.Length == 0)
        {
            for (int i = 0; i < sampleAi.Length; i++)
            {
                string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
                File.WriteAllText(filePath, sampleAi[i]);
            }
            txtFiles = Directory.GetFiles(inputFolder, "*.txt");
        }

        // Create ZIP archive for output
        using (var zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: false))
        {
            foreach (string file in txtFiles)
            {
                // Read AI string from file
                string codeText = File.ReadAllText(file).Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    continue; // skip empty files
                }

                // Generate GS1 DataMatrix barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                {
                    // Optional: set XDimension for better size control
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Save barcode to memory stream as PNG
                    using (var imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // reset for reading

                        // Add image to ZIP archive
                        string entryName = Path.GetFileNameWithoutExtension(file) + ".png";
                        var zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);
                        using (var entryStream = zipEntry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Barcode images have been saved to '{outputZipPath}'.");
    }
}