// Title: Generate Code 39 barcodes with checksum and save as SVG
// Description: This example reads text files from a folder, creates Code 39 barcodes with checksum enabled, and writes the barcodes as SVG images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for batch processing scenarios. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create Code 39 barcodes with checksum, a common requirement for inventory and tracking systems. Developers can adapt this pattern for bulk barcode creation from file data.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, checksum, svg, barcode generation, batch processing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Batch processes text files to generate Code 39 barcodes with checksum enabled and saves them as SVG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans the Input folder, creates barcodes, and writes them to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define input and output folders relative to the executable directory
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Output");

        // Ensure the input and output folders exist
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Seed sample files if the input folder is empty (self‑contained example)
        string[] sampleFiles = Directory.GetFiles(inputFolder);
        if (sampleFiles.Length == 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"Sample{i}.txt");
                File.WriteAllText(samplePath, $"Sample content {i}");
            }
        }

        // Process each .txt file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.txt"))
        {
            // Use the file name (without extension) as the barcode text
            string codeText = Path.GetFileNameWithoutExtension(filePath);

            // Build the output SVG file path
            string outputPath = Path.Combine(outputFolder, $"{codeText}.svg");

            // Generate a Code39 barcode with checksum enabled and save it as SVG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }
        }

        // Indicate completion
        Console.WriteLine("Barcode generation completed.");
    }
}