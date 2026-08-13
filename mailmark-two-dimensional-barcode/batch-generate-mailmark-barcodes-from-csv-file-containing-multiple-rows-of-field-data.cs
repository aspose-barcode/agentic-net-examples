// Title: Batch generate Mailmark barcodes from CSV data
// Description: Demonstrates reading a CSV file with Mailmark field values and creating a PNG barcode for each record using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to work with the Mailmark symbology by populating a MailmarkCodetext object and using ComplexBarcodeGenerator to produce barcodes. Typical scenarios include bulk creation of Mailmark barcodes for mailing, logistics, or inventory systems. Developers often need to parse input data, validate fields, and save barcodes in common image formats such as PNG.
// Prompt: Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data.
// Tags: mailmark, barcode, csv, batch, generation, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Mailmark barcodes in batch from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the CSV, validates each row, creates a MailmarkCodetext object,
    /// generates a PNG barcode, and saves it to the output folder.
    /// </summary>
    static void Main()
    {
        // Define input CSV and output folder paths
        string csvPath = "mailmark_input.csv";
        string outputFolder = "Output";

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV does not exist, create a sample file with a few records
        if (!File.Exists(csvPath))
        {
            var sampleLines = new[]
            {
                "Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
                "4,1,0,384224,16563760,EF61AH8T ",
                "4,1,0,384224,16563761,EF61AH8T ",
                "4,1,0,384224,16563762,EF61AH8T "
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV, skipping the header row
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Process each data row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split by comma (simple CSV, no quoted commas)
            string[] parts = line.Split(',');

            if (parts.Length != 6)
            {
                Console.WriteLine($"Line {i + 1}: Incorrect number of fields. Skipping.");
                continue;
            }

            // Parse numeric fields; if parsing fails, skip the row
            if (!int.TryParse(parts[0].Trim(), out int format) ||
                !int.TryParse(parts[1].Trim(), out int versionId) ||
                !int.TryParse(parts[3].Trim(), out int supplyChainId) ||
                !int.TryParse(parts[4].Trim(), out int itemId))
            {
                Console.WriteLine($"Line {i + 1}: Invalid numeric values. Skipping.");
                continue;
            }

            // Remaining fields are strings; preserve trailing space for destination as required
            string classValue = parts[2].Trim();
            string destination = parts[5];

            // Validate that the format corresponds to 4‑state Mailmark (value 4)
            if (format != 4)
            {
                Console.WriteLine($"Line {i + 1}: Unsupported Format {format}. Only 4‑state (value 4) is supported. Skipping.");
                continue;
            }

            // Build the MailmarkCodetext object with the parsed values
            var mailmark = new MailmarkCodetext
            {
                Format = format,
                VersionID = versionId,
                Class = classValue,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destination // preserve trailing space
            };

            // Generate the barcode and save it as a PNG file
            string outputPath = Path.Combine(outputFolder, $"Mailmark_{i}.png");
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode for line {i + 1} -> {outputPath}");
        }

        Console.WriteLine("Batch generation completed.");
    }
}