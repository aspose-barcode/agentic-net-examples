using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure input folder exists; create a sample file if empty
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed a sample file if the folder has no files
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.txt");
            File.WriteAllText(samplePath, "ABC123");
            existingFiles = new[] { samplePath };
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each file in the input folder
        foreach (string filePath in existingFiles)
        {
            // Use the file name (without extension) as the barcode text
            string codeText = Path.GetFileNameWithoutExtension(filePath);

            // Create a Code 39 barcode generator with checksum enabled
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                // Save as SVG
                string outputFile = Path.Combine(outputFolder, $"{codeText}.svg");
                generator.Save(outputFile, BarCodeImageFormat.Svg);
                Console.WriteLine($"Generated barcode for '{codeText}' -> {outputFile}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}