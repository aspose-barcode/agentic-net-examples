using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define CSV file path
        string csvPath = "data.csv";

        // If CSV does not exist, create a sample file with a few rows
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                // Format: CodeText,XDimension(Point),ImageWidth(Point),ImageHeight(Point)
                writer.WriteLine("ABC123,2.5,300,150");
                writer.WriteLine("XYZ789,3.0,250,120");
                writer.WriteLine("123456,1.8,200,100");
            }
        }

        // Read all lines from CSV
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split CSV fields
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Skipping invalid line {index}: {line}");
                index++;
                continue;
            }

            string codeText = parts[0].Trim();

            // Parse numeric values using invariant culture
            if (!float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float xDim) ||
                !float.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float imgWidth) ||
                !float.TryParse(parts[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float imgHeight))
            {
                Console.WriteLine($"Skipping line with invalid numbers {index}: {line}");
                index++;
                continue;
            }

            // Create barcode generator for Code128 (as a common 1D symbology)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Assign size units
                generator.Parameters.Barcode.XDimension.Point = xDim;          // smallest bar width
                generator.Parameters.ImageWidth.Point = imgWidth;            // overall image width
                generator.Parameters.ImageHeight.Point = imgHeight;          // overall image height

                // Optional: set colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build output file name
                string outputFile = $"barcode_{index}.png";

                // Save as PNG
                generator.Save(outputFile, BarCodeImageFormat.Png);

                Console.WriteLine($"Generated {outputFile} for code '{codeText}'");
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}