using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
class Program
{
    static void Main()
    {
        const string csvPath = "input.csv";

        if (!File.Exists(csvPath))
        {
            throw new FileNotFoundException($"CSV file not found: {csvPath}");
        }

        // Read all non‑empty lines from the CSV file.
        List<string> values = new List<string>();
        using (var reader = new StreamReader(csvPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {
                    // Assuming each line contains a single value to encode.
                    values.Add(line);
                }
            }
        }

        // Create a barcode for each value.
        int index = 1;
        foreach (string value in values)
        {
            // Initialize generator for Code39.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
            {
                // Set the text to encode.
                generator.CodeText = value;

                // Enable checksum generation and make it visible.
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save as BMP; file name includes index to avoid illegal characters.
                string safeFileName = $"barcode_{index}.bmp";
                generator.Save(safeFileName);
                Console.WriteLine($"Saved barcode for \"{value}\" as {safeFileName}");
            }

            index++;
        }
    }
}