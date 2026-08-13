// Title: Batch Generation of MaxiCode Mode 2 Barcodes from CSV
// Description: Demonstrates how to read a CSV file containing postal, country, service, and secondary message data and generate a series of MaxiCode Mode 2 barcode images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related classes to create image files. Developers often need to automate bulk barcode creation for shipping, logistics, or inventory systems, and this snippet provides a template for reading input data and producing PNG outputs.
// Prompt: Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary and secondary data rows.
// Tags: maxicode, batch, csv, barcode generation, image, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates MaxiCode Mode 2 barcodes in batch from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads CSV, creates output folder, and generates barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file (relative to the executable)
        string csvPath = "maxicode_input.csv";

        // Folder where generated PNG images will be saved
        string outputFolder = "Output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file is missing, create a small sample file for demonstration
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                "PostalCode,CountryCode,ServiceCategory,SecondMessage",
                "524032140,056,999,Sample message 1",
                "123456789,840,100,Sample message 2",
                "987654321,124,200,Sample message 3"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at '{csvPath}'.");
        }

        // Read all lines from the CSV, preserving empty lines for later filtering
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV file does not contain data rows.");
            return;
        }

        // Process each data row, skipping the header line (index 0)
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i].Trim();
            if (string.IsNullOrEmpty(line))
            {
                // Skip blank lines
                continue;
            }

            // Split the CSV line into its constituent fields
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}: '{line}'");
                continue;
            }

            // Extract and validate individual fields
            string postalCode = parts[0].Trim();

            if (!int.TryParse(parts[1].Trim(), out int countryCode))
            {
                Console.WriteLine($"Invalid CountryCode on line {i + 1}");
                continue;
            }

            if (!int.TryParse(parts[2].Trim(), out int serviceCategory))
            {
                Console.WriteLine($"Invalid ServiceCategory on line {i + 1}");
                continue;
            }

            string secondMessageText = parts[3].Trim();

            // Build the MaxiCode codetext (Mode 2) with a standard second message
            var maxiCodeData = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
            };

            // Generate the barcode image using the complex barcode generator
            using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
            {
                using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
                {
                    // Construct a unique file name for each barcode
                    string fileName = $"MaxiCode_{i:D3}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the image as PNG
                    image.Save(outputPath);
                    Console.WriteLine($"Saved barcode to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}