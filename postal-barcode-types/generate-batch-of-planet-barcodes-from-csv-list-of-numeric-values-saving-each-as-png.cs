using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input CSV file and output folder
        string csvPath = "values.csv";
        string outputFolder = "Barcodes";

        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If CSV does not exist, create a sample file with a few numeric values
        if (!File.Exists(csvPath))
        {
            var sampleValues = new[] { "12345", "67890", "112233", "445566", "778899" };
            File.WriteAllText(csvPath, string.Join(",", sampleValues));
        }

        // Read all content from CSV
        string csvContent = File.ReadAllText(csvPath);
        // Split by commas and trim whitespace
        string[] rawValues = csvContent.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        var values = new List<string>();
        foreach (var raw in rawValues)
        {
            string trimmed = raw.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                values.Add(trimmed);
            }
        }

        // Process each numeric value and generate a Planet barcode
        foreach (var value in values)
        {
            // Build output file name
            string fileName = $"Planet_{value}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, value))
            {
                // Optional: customize appearance (e.g., bar color)
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode image as PNG
                generator.Save(filePath);
            }

            Console.WriteLine($"Generated barcode for value {value} -> {filePath}");
        }

        Console.WriteLine("All barcodes have been generated.");
    }
}