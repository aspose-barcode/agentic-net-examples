// Title: Generate Code 39 Barcodes from CSV and Save BMP Images
// Description: This example reads a CSV file containing text values, creates Code 39 barcodes with visible checksums, and saves each barcode as a BMP image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Code 39 symbology, covering CSV data ingestion, checksum configuration, and BMP image output. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for batch barcode creation tasks in inventory, shipping, or labeling applications.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: code39,barcode generation,checksum,csv,output bmp,aspose.barcode,aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Reads a CSV file, generates Code 39 barcodes with visible checksums,
/// and saves each barcode as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the CSV‑to‑barcode workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary folder and write a sample CSV file.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeCsvDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string csvPath = Path.Combine(tempFolder, "data.csv");
        File.WriteAllLines(csvPath, new[]
        {
            "ABC123",
            "XYZ789",
            "CODE39",
            "HELLO WORLD"
        }, Encoding.UTF8);

        // --------------------------------------------------------------------
        // 2. Verify that the CSV file exists before proceeding.
        // --------------------------------------------------------------------
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 3. Create an output folder for the generated BMP images.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(tempFolder, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // 4. Read each line from the CSV and generate a corresponding barcode.
        // --------------------------------------------------------------------
        string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);
        foreach (string rawLine in lines)
        {
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty lines.

            // Create a barcode generator for Code39 with full ASCII support.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum calculation and make it visible in the human‑readable text.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Show the code text below the barcode (explicitly set location).
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Build a safe file name for the BMP image.
                string safeFileName = GetSafeFileName(codeText) + ".bmp";
                string outputPath = Path.Combine(outputFolder, safeFileName);

                // Save the barcode as a BMP file.
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved barcode for \"{codeText}\" to {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // ------------------------------------------------------------------------
    // Helper method: creates a file‑system safe file name from arbitrary text.
    // ------------------------------------------------------------------------
    private static string GetSafeFileName(string input)
    {
        var sb = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c) || c == '_' || c == '-')
                sb.Append(c);
            else
                sb.Append('_');
        }
        return sb.ToString();
    }
}