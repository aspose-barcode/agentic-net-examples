using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string inputFile = "zipcodes.csv";
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input file does not exist, create a sample file with a few zip codes
        if (!File.Exists(inputFile))
        {
            var sampleCodes = new List<string>
            {
                "12345",
                "67890",
                "123456789",
                "98765",
                "54321"
            };
            File.WriteAllLines(inputFile, sampleCodes);
        }

        // Read zip codes from the file (one code per line)
        var codes = new List<string>();
        foreach (var line in File.ReadAllLines(inputFile))
        {
            var trimmed = line.Trim();
            if (!string.IsNullOrEmpty(trimmed))
            {
                codes.Add(trimmed);
            }
        }

        // Limit processing to a safe number of items
        int maxCount = Math.Min(codes.Count, 10);
        for (int i = 0; i < maxCount; i++)
        {
            string code = codes[i];

            // Postnet requires 5 or 9 numeric digits; skip invalid entries
            if (code.Length != 5 && code.Length != 9)
            {
                Console.WriteLine($"Skipping invalid Postnet code '{code}'. Must be 5 or 9 digits.");
                continue;
            }

            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
            {
                // Optional: set image dimensions
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image as PNG
                string filePath = Path.Combine(outputFolder, $"Postnet_{code}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved barcode for {code} to {filePath}");
            }
        }

        // Real database implementation placeholder:
        // In a production scenario replace the CSV reading with a database query, e.g.:
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT ZipCode FROM Addresses", connection))
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             string dbCode = reader.GetString(0);
        //             // Generate barcode as shown above
        //         }
        //     }
        // }
    }
}