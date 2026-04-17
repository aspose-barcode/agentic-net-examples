using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Define input CSV and output folder
        string inputCsv = "maxicode_data.csv";
        string outputFolder = "MaxiCodeOutput";

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If CSV does not exist, create a small sample file
        if (!File.Exists(inputCsv))
        {
            var sampleLines = new List<string>
            {
                // PostalCode,CountryCode,ServiceCategory,SecondMessage
                "524032140,056,999,Standard message 1",
                "123456789,001,100,Standard message 2",
                "987654321,840,200,Standard message 3"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all non‑empty lines from the CSV
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Skipping invalid line {index}: insufficient columns.");
                index++;
                continue;
            }

            // Parse fields
            string postalCode = parts[0].Trim();          // 9‑digit postal code for Mode 2
            if (!int.TryParse(parts[1].Trim(), out int countryCode))
            {
                Console.WriteLine($"Skipping line {index}: invalid CountryCode.");
                index++;
                continue;
            }
            if (!int.TryParse(parts[2].Trim(), out int serviceCategory))
            {
                Console.WriteLine($"Skipping line {index}: invalid ServiceCategory.");
                index++;
                continue;
            }
            string secondMessageText = parts[3].Trim();

            // Build MaxiCode codetext for Mode 2 with a standard second message
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
            };

            // Generate and save the barcode image
            string outputPath = Path.Combine(outputFolder, $"MaxiCode_{index:D3}.png");
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Optional: explicitly set the mode (Mode2) – not required because codetext defines it
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

                // Save as PNG
                generator.Save(outputPath);
            }

            Console.WriteLine($"Generated barcode {outputPath}");
            index++;
        }

        Console.WriteLine("Batch generation completed.");
    }
}