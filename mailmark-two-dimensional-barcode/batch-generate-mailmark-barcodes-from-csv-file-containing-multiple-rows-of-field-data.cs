// Title: Batch generate Mailmark barcodes from CSV data
// Description: Demonstrates reading a CSV file containing Mailmark fields and generating a PNG barcode for each row using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext classes to create Mailmark symbology barcodes in bulk. Typical scenarios include preparing mailing labels, logistics tracking, and bulk printing of postal barcodes. Developers often need to read structured data (e.g., CSV) and produce image files for each record, which this sample illustrates.
// Prompt: Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data.
// Tags: mailmark, barcode, csv, batch, generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Mailmark barcodes from CSV data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample CSV, reads each data row, and generates a PNG barcode image.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for the CSV file and generated barcode images
        string baseDir = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        string csvPath = Path.Combine(baseDir, "data.csv");
        string outputDir = Path.Combine(baseDir, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Write a sample CSV file (header + three data rows)
        var sampleLines = new List<string>
        {
            "Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
            "4,1,0,384224,16563762,EF61AH8T ",
            "4,1,0,384225,16563763,EF61AH8T ",
            "4,1,0,384226,16563764,EF61AH8T "
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Verify that the CSV file was created successfully
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found.");
            return;
        }

        // Read all lines from the CSV, skipping the header row
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Process each data row and generate a corresponding Mailmark barcode
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            string[] parts = line.Split(',');
            if (parts.Length != 6)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}.");
                continue;
            }

            try
            {
                // Parse individual fields from the CSV row
                int format = int.Parse(parts[0].Trim());
                int versionId = int.Parse(parts[1].Trim());
                string classValue = parts[2].Trim();
                int supplyChainId = int.Parse(parts[3].Trim());
                int itemId = int.Parse(parts[4].Trim());
                string destination = parts[5]; // Preserve trailing space if present

                // Populate the Mailmark codetext object with parsed values
                var mailmark = new MailmarkCodetext
                {
                    Format = format,
                    VersionID = versionId,
                    Class = classValue,
                    SupplychainID = supplyChainId,
                    ItemID = itemId,
                    DestinationPostCodePlusDPS = destination
                };

                // Define the output file path for the generated barcode image
                string outputPath = Path.Combine(outputDir, $"Mailmark_{i}.png");

                // Generate the barcode using ComplexBarcodeGenerator and save as PNG
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 4;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {i}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}