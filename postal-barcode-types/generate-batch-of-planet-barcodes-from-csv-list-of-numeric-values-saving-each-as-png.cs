// Title: Generate Planet barcodes from CSV values
// Description: This example reads numeric values from a CSV file and creates a Planet barcode PNG for each value.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. It showcases the BarcodeGenerator class with EncodeTypes.Planet, file I/O for CSV input, and saving images in PNG format. Useful for developers needing to automate barcode creation from data sources such as spreadsheets or databases.
// Prompt: Generate a batch of Planet barcodes from a CSV list of numeric values, saving each as PNG.
// Tags: planet, barcode, generation, csv, png, aspose.barcode, batch-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Planet barcodes from a CSV list of numeric values and saves each as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads values from a CSV file, validates them, and creates corresponding barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file containing comma‑separated numeric values.
        string csvPath = "values.csv";

        // If the CSV file does not exist, create a small sample file with example values.
        if (!File.Exists(csvPath))
        {
            string sampleData = "123456,789012,345678,901234,567890";
            File.WriteAllText(csvPath, sampleData);
        }

        // Directory where generated PNG barcode images will be stored.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read the entire CSV content and split it into individual values.
        string csvContent = File.ReadAllText(csvPath);
        string[] values = csvContent.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Process each numeric value from the CSV.
        foreach (string rawValue in values)
        {
            string value = rawValue.Trim();

            // Validate that the value consists only of digits; skip if invalid.
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    Console.WriteLine($"Skipping invalid value: {value}");
                    goto ContinueLoop;
                }
            }

            // Build the output file name, e.g., "Planet_123456.png".
            string outputPath = Path.Combine(outputDir, $"Planet_{value}.png");

            // Generate a Planet barcode for the validated value and save it as PNG.
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, value))
            {
                // Optional: set a white background and black bars (default colors are fine).
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the barcode image in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

        ContinueLoop:
            continue;
        }

        Console.WriteLine("Barcode generation completed.");
    }
}