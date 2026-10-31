// Title: Generate Code 39 Barcodes from CSV and Save as BMP
// Description: This example reads a CSV file containing barcode text and output filenames, creates Code 39 barcodes with visible checksums, and saves them as BMP images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for batch processing scenarios. It uses BarcodeGenerator with EncodeTypes.Code39FullASCII, configures checksum visibility, and outputs BMP files. Typical use cases include creating printable labels from data sources such as CSV, Excel, or databases. Developers working with bulk barcode creation often need to control checksum display, text placement, and image format, which this example illustrates.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: code39, barcode, generation, csv, bmp, checksum, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads barcode data from a CSV file, generates Code 39 barcodes with visible checksums,
/// and saves each barcode as a BMP image in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the CSV reading, barcode generation, and file saving workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the path for a sample CSV file and write example data.
        string csvPath = Path.Combine(outputFolder, "data.csv");
        // Sample CSV content: CodeText,FileName
        string[] sampleLines = new[]
        {
            "ABC123,ABC123.bmp",
            "XYZ789,XYZ789.bmp",
            "HELLO,WORLD.bmp"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Verify that the CSV file exists before proceeding.
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        // Read all lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            // Skip empty or whitespace-only lines.
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split each line into barcode text and output file name.
            string[] parts = line.Split(',');
            if (parts.Length < 2)
                continue;

            string codeText = parts[0].Trim();
            string fileName = parts[1].Trim();

            // Ensure both fields contain valid data.
            if (string.IsNullOrEmpty(codeText) || string.IsNullOrEmpty(fileName))
                continue;

            // Build the full output path for the BMP file.
            string outPath = Path.Combine(outputFolder, fileName);

            // Generate the barcode with visible checksum and save as BMP.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                generator.Save(outPath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' -> {outPath}");
        }

        Console.WriteLine("All barcodes generated in: " + outputFolder);
    }
}