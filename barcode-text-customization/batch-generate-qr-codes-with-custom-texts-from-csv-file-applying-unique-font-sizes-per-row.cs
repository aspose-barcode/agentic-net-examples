using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output directories
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        string csvPath = Path.Combine(inputDir, "data.csv");

        // Ensure input directory exists
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If CSV file is missing, create a sample one
        if (!File.Exists(csvPath))
        {
            string[] sampleLines = {
                "Hello World,12",
                "Aspose.BarCode,16",
                "Sample QR,14"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expected format: text,fontSize
            string[] parts = line.Split(',');
            if (parts.Length != 2)
                continue; // Skip malformed lines

            string text = parts[0].Trim();
            if (!float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float fontSize))
                continue; // Skip if font size is invalid

            // Generate QR code with custom text and font size
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the encoded value
                generator.CodeText = text;

                // Set visible text (optional, shows the same text under the QR)
                generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = text;

                // Apply unique font size for this row
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Save the QR code image
                string outputPath = Path.Combine(outputDir, $"qr_{index}.png");
                generator.Save(outputPath);
            }

            index++;
        }
    }
}