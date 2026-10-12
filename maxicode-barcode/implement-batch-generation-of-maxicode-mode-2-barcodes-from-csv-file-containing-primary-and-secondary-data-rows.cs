// Title: Batch Generation of MaxiCode Mode 2 Barcodes from CSV
// Description: Demonstrates how to read up to five rows from a CSV file and generate MaxiCode Mode 2 barcodes, saving each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode for .NET complex barcode generation category. It showcases the use of the ComplexBarcodeGenerator class together with MaxiCodeCodetextMode2 and MaxiCodeStructuredSecondMessage to create MaxiCode symbols, a common requirement for shipping and logistics applications. Developers often need to generate multiple barcodes in batch from data sources such as CSV files, customizing address lines and optional fields.
// Prompt: Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary and secondary data rows.
// Tags: maxicode, batch, png, complexbarcodegenerator, maxicodecodetextmode2, maxicodestructuredsecondmessage

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a batch of MaxiCode Mode 2 barcodes from a CSV file and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads CSV data, constructs MaxiCode payloads, and creates barcode images.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare input CSV file (create sample if it does not exist)
        // ------------------------------------------------------------
        string inputCsv = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_input.csv");
        if (!File.Exists(inputCsv))
        {
            var sampleLines = new List<string>
            {
                "524032140,56,999,634 ALPHA DRIVE,PITTSBURGH,PA,99",
                "524032141,56,998,123 BETA STREET,NEW YORK,NY,98",
                "524032142,56,997,456 GAMMA AVE,CHICAGO,IL,97",
                "524032143,56,996,789 DELTA RD,LOS ANGELES,CA,96",
                "524032144,56,995,321 EPSILON BLVD,SEATTLE,WA,95"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // ------------------------------------------------------------
        // Create a unique temporary output folder for generated barcodes
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Read CSV lines and limit processing to a maximum of 5 rows
        // ------------------------------------------------------------
        string[] lines = File.ReadAllLines(inputCsv);
        int maxRows = Math.Min(lines.Length, 5); // safety cap

        for (int i = 0; i < maxRows; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // skip empty lines

            // Split CSV line into columns
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Skipping line {i + 1}: insufficient columns.");
                continue;
            }

            // --------------------------------------------------------
            // Parse primary data: postal code, country code, service category
            // --------------------------------------------------------
            string postalCode = parts[0].Trim();
            if (!int.TryParse(parts[1].Trim(), out int countryCode))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid CountryCode.");
                continue;
            }
            if (!int.TryParse(parts[2].Trim(), out int serviceCategory))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid ServiceCategory.");
                continue;
            }

            // --------------------------------------------------------
            // Build structured second message (address lines and optional year)
            // --------------------------------------------------------
            var secondMessage = new MaxiCodeStructuredSecondMessage();

            // Add up to three address lines (columns 4‑6)
            for (int j = 3; j < Math.Min(parts.Length, 6); j++)
            {
                string msgLine = parts[j].Trim();
                if (!string.IsNullOrEmpty(msgLine))
                    secondMessage.Add(msgLine);
            }

            // Optional year (column 7 if numeric)
            if (parts.Length > 6 && int.TryParse(parts[6].Trim(), out int year))
            {
                secondMessage.Year = year;
            }

            // --------------------------------------------------------
            // Assemble MaxiCode Mode 2 codetext payload
            // --------------------------------------------------------
            var codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = secondMessage
            };

            // --------------------------------------------------------
            // Generate barcode image and save to output folder
            // --------------------------------------------------------
            string outputPath = Path.Combine(outputFolder, $"MaxiCode_Mode2_Row{i + 1}.png");
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Save(outputPath);
            }

            Console.WriteLine($"Generated barcode {i + 1} at: {outputPath}");
        }

        Console.WriteLine("Batch generation completed.");
    }
}