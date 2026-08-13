// Title: Batch conversion of CSV rows to Mailmark barcode images using Aspose.BarCode
// Description: Demonstrates reading a CSV file and generating a separate Mailmark barcode image for each data row.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use ComplexBarcodeGenerator and MailmarkCodetext to create Mailmark barcodes in bulk. Typical use cases include converting data sources such as CSV files into individual barcode images for mailing, logistics, or inventory tracking. Developers often need to automate barcode creation for large datasets, and this pattern illustrates the essential API classes and workflow.
// Prompt: Create a PowerShell script invoking the .NET library to batch convert CSV rows into individual Mailmark barcode images.
// Tags: mailmark, barcode, generation, csv, batch, aspose.barcode, png, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch conversion of CSV data rows into individual Mailmark barcode PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that reads CSV, creates output folder, and generates barcode images.
    /// </summary>
    static void Main()
    {
        // Define the input CSV file path; fall back to a sample file if it does not exist.
        string csvPath = "mailmark_data.csv";
        if (!File.Exists(csvPath))
        {
            // Create a small sample CSV to ensure the example runs without external data.
            File.WriteAllText(csvPath,
                "Class,DestinationPostCodePlusDPS,Format,VersionID,ItemID,SupplychainID\n" +
                "\"0\",\"EF61AH8T \",1,1,16563762,384224\n" +
                "\"1\",\"EF61AH8T \",2,1,16563763,384224");
            Console.WriteLine($"Sample CSV created at '{Path.GetFullPath(csvPath)}'.");
        }

        // Prepare the output folder where generated barcode images will be saved.
        string outputFolder = "MailmarkBarcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Read all lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV contains no data rows.");
            return;
        }

        // Process each data row (skip header line at index 0).
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            // Simple CSV split (does not handle escaped commas).
            string[] parts = line.Split(',');

            if (parts.Length < 6)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}: insufficient columns.");
                continue;
            }

            try
            {
                // Extract and clean individual column values.
                string classValue = parts[0].Trim().Trim('\"');
                string destination = parts[1].Trim().Trim('\"'); // Keep trailing space as required.
                int format = int.Parse(parts[2].Trim());
                int versionId = int.Parse(parts[3].Trim());
                int itemId = int.Parse(parts[4].Trim());
                int supplyChainId = int.Parse(parts[5].Trim());

                // Build the Mailmark codetext object with the extracted values.
                var mailmark = new MailmarkCodetext
                {
                    Class = classValue,
                    DestinationPostCodePlusDPS = destination,
                    Format = format,
                    VersionID = versionId,
                    ItemID = itemId,
                    SupplychainID = supplyChainId
                };

                // Generate the barcode image using ComplexBarcodeGenerator.
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    string fileName = $"Mailmark_{i:D4}_{itemId}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}