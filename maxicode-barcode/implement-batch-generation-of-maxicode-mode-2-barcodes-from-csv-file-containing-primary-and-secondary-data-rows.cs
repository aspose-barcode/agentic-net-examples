// Title: Batch Generation of MaxiCode Mode 2 Barcodes from CSV
// Description: Demonstrates how to read postal and address data from a CSV file and generate MaxiCode Mode 2 barcodes in batch using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and MaxiCodeStructuredSecondMessage classes to create Mode 2 barcodes, a common requirement for shipping and logistics applications. Developers often need to automate bulk barcode creation from data sources like CSV files, and this snippet provides a clear pattern for doing so.
// Prompt: Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary and secondary data rows.
// Tags: maxicode, mode2, batch, csv, aspose.barcode, barcode-generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a CSV file and generates MaxiCode Mode 2 barcodes in batch.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, writes a sample CSV,
    /// parses each row, validates data, builds the MaxiCode payload, and saves the generated images.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Path to the sample CSV file
        string csvPath = Path.Combine(batchFolder, "data.csv");

        // Create a small sample CSV (PostalCode,CountryCode,ServiceCategory,Line1,Line2,Line3,Year)
        var sampleLines = new List<string>
        {
            "524032140,56,999,634 ALPHA DRIVE,PITTSBURGH,PA,99",
            "524032141,56,998,123 BETA STREET,NEW YORK,NY,20",
            "524032142,56,997,789 GAMMA AVE,LOS ANGELES,CA,15"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Read and process the CSV
        var rows = new List<string[]>();
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines
            var parts = line.Split(',');
            rows.Add(parts);
        }

        // Limit batch size to a safe number (max 5)
        int maxItems = Math.Min(rows.Count, 5);
        for (int i = 0; i < maxItems; i++)
        {
            var cols = rows[i];
            if (cols.Length < 7)
            {
                Console.WriteLine($"Row {i + 1} does not contain enough columns. Skipping.");
                continue;
            }

            // Validate postal code (must be 9 digits for Mode 2)
            string postalCode = cols[0].Trim();
            if (postalCode.Length != 9 || !int.TryParse(postalCode, out _))
            {
                Console.WriteLine($"Row {i + 1} has invalid PostalCode for Mode 2. Skipping.");
                continue;
            }

            // Parse numeric fields: country code, service category, and year
            if (!int.TryParse(cols[1].Trim(), out int countryCode) ||
                !int.TryParse(cols[2].Trim(), out int serviceCategory) ||
                !int.TryParse(cols[6].Trim(), out int year))
            {
                Console.WriteLine($"Row {i + 1} has invalid numeric fields. Skipping.");
                continue;
            }

            // Build the structured second message (address lines and year)
            var secondMessage = new MaxiCodeStructuredSecondMessage();
            secondMessage.Add(cols[3].Trim()); // Line1
            secondMessage.Add(cols[4].Trim()); // Line2
            secondMessage.Add(cols[5].Trim()); // Line3
            secondMessage.Year = year;

            // Assemble the MaxiCode codetext for Mode 2
            var codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = secondMessage
            };

            // Generate and save the barcode image
            string outputPath = Path.Combine(batchFolder, $"MaxiCode_Mode2_Row{i + 1}.png");
            try
            {
                using (var generator = new ComplexBarcodeGenerator(codetext))
                {
                    generator.Save(outputPath);
                }
                Console.WriteLine($"Generated barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for row {i + 1}: {ex.Message}");
            }
        }

        // Cleanup note (optional): the temporary folder and files remain for inspection
        Console.WriteLine($"Batch processing completed. Files are located in: {batchFolder}");
    }
}