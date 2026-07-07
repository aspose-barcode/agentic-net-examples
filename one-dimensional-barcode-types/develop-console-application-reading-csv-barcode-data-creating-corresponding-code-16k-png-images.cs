// Title: Generate Code 16K Barcodes from CSV
// Description: Reads a CSV file with filename and Code 16K text pairs and creates PNG barcode images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Code 16K symbology. Shows how to configure generator parameters, handle CSV input, and save images in PNG format. Ideal for developers needing batch barcode creation in console applications, covering key classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Develop console application reading CSV barcode data, creating corresponding Code 16K PNG images.
// Tags: code16k, barcode generation, png output, aspose.barcode, console app

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console application that reads a CSV file containing barcode data and generates
/// Code 16K PNG images using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the CSV file and creates barcode images.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path to the CSV file containing barcode data.
        string csvPath = "barcodes.csv";

        // If the CSV does not exist, create a small sample file.
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("sample1.png,HELLO123");
                writer.WriteLine("sample2.png,WORLD456");
            }
            Console.WriteLine($"Sample CSV created at '{csvPath}'.");
        }

        // Open the CSV for reading line by line.
        using (var reader = new StreamReader(csvPath))
        {
            string line;
            int lineNumber = 0;

            // Process each non‑empty line.
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                // Skip blank lines.
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split the line into filename and barcode text.
                string[] parts = line.Split(',');
                if (parts.Length < 2)
                {
                    Console.WriteLine($"Invalid format at line {lineNumber}: '{line}'. Expected 'filename,codeText'.");
                    continue;
                }

                string fileName = parts[0].Trim();
                string codeText = parts[1].Trim();

                // Validate that barcode text is present.
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Empty CodeText at line {lineNumber}.");
                    continue;
                }

                // Generate the barcode using Aspose.BarCode.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Enable automatic sizing based on content.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Set Code16K‑specific aspect ratio (default is 1.0).
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                    // Optional visual settings: black bars on white background.
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Build the full output path for the PNG file.
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                    // Save the generated barcode as a PNG image.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated '{outputPath}'.");
                }
            }
        }
    }
}