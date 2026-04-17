using System;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input CSV and output folder paths
        string inputCsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.csv");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If CSV does not exist, create a sample file with a few rows
        if (!File.Exists(inputCsv))
        {
            string[] sampleLines =
            {
                "Hello World,12",
                "Aspose.BarCode,14",
                "QR Code Sample,16",
                "Custom Text,18",
                "Final Row,20"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all lines from the CSV
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split line into text and font size
            string[] parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
            {
                Console.WriteLine($"Skipping invalid line: {line}");
                continue;
            }

            string text = parts[0].Trim();
            if (!float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float fontSize))
            {
                Console.WriteLine($"Invalid font size on line: {line}");
                continue;
            }

            // Generate QR code with specified text and font size
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set QR error correction level (optional)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Show human‑readable text below the barcode
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Apply custom font size
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Save the barcode image
                string outputPath = Path.Combine(outputFolder, $"qr_{index}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code {index}: \"{text}\" with font size {fontSize}pt");
            index++;
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}