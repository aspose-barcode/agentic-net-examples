// Title: Batch generation of MaxiCode Mode 2 barcodes from CSV
// Description: Demonstrates how to read postal and address data from a CSV file and generate a series of MaxiCode Mode 2 barcodes, saving each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and MaxiCodeStructuredSecondMessage classes to create postal barcodes for bulk processing. Developers often need to automate barcode creation from data sources like CSV files for shipping, logistics, and inventory systems.
// Prompt: Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary and secondary data rows.
// Tags: maxicode, barcode, batch, csv, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Provides an example of batch generating MaxiCode Mode 2 barcodes from CSV data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, writes sample CSV data, reads each row,
    /// builds the MaxiCode codetext, and generates PNG barcode images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a sample CSV file (PostalCode,CountryCode,ServiceCategory,Line1,Line2,Line3,Year)
        string csvPath = Path.Combine(batchFolder, "data.csv");
        string[] sampleLines = new string[]
        {
            "PostalCode,CountryCode,ServiceCategory,Line1,Line2,Line3,Year",
            "524032140,56,999,634 ALPHA DRIVE,PITTSBURGH,PA,99",
            "123456789,56,999,123 MAIN ST,NEW YORK,NY,20",
            "987654321,56,999,456 OAK AVE,LOS ANGELES,CA,21"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Read all CSV lines
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Process each data row (skip header)
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the CSV line into individual fields
            string[] parts = line.Split(',');
            if (parts.Length < 7)
            {
                Console.WriteLine($"Row {i} is malformed and will be skipped.");
                continue;
            }

            // Validate and extract primary fields
            string postalCode = parts[0].Trim();
            if (postalCode.Length != 9 || !long.TryParse(postalCode, out _))
            {
                Console.WriteLine($"Row {i}: PostalCode must be exactly 9 digits.");
                continue;
            }

            if (!int.TryParse(parts[1].Trim(), out int countryCode))
            {
                Console.WriteLine($"Row {i}: invalid CountryCode.");
                continue;
            }

            if (!int.TryParse(parts[2].Trim(), out int serviceCategory))
            {
                Console.WriteLine($"Row {i}: invalid ServiceCategory.");
                continue;
            }

            // Extract secondary message lines
            string line1 = parts[3].Trim();
            string line2 = parts[4].Trim();
            string line3 = parts[5].Trim();

            if (!int.TryParse(parts[6].Trim(), out int year))
            {
                Console.WriteLine($"Row {i}: invalid Year.");
                continue;
            }

            // Build MaxiCode codetext for Mode 2
            var codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory
            };

            // Populate the structured second message
            var secondMessage = new MaxiCodeStructuredSecondMessage();
            secondMessage.Add(line1);
            secondMessage.Add(line2);
            secondMessage.Add(line3);
            secondMessage.Year = year;

            codetext.SecondMessage = secondMessage;

            // Generate barcode image and save as PNG
            string outputPath = Path.Combine(batchFolder, $"MaxiCode_{i}.png");
            try
            {
                using (var generator = new ComplexBarcodeGenerator(codetext))
                {
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated barcode {i}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for row {i}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}