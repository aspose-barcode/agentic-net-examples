// Title: Batch generate Codabar barcodes from CSV with alternating start symbols
// Description: Demonstrates reading a CSV file, generating a Codabar barcode for each entry, and alternating the start/stop symbols to add visual variety.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing bulk barcode creation using the BarcodeGenerator class with EncodeTypes.Codabar. Typical use cases include preparing large sets of barcodes for inventory, shipping, or retail labeling where different start symbols are desired for visual distinction. Developers often need to read data sources (e.g., CSV files), configure barcode parameters, and save images in common formats like PNG.
// Prompt: Batch generate Codabar barcodes from a CSV file, applying alternating start symbols for visual variety.
// Tags: codabar, barcode, batch, csv, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes from a CSV file,
/// applying alternating start/stop symbols for visual variety.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads a CSV file, creates a Codabar barcode for each line,
    /// alternates the start/stop symbols, and saves the images to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file with codetext values
        string csvPath = Path.Combine(outputFolder, "data.csv");
        string[] sampleData = new string[]
        {
            "12345",
            "67890",
            "12-34",
            "56$78",
            "90/12"
        };
        File.WriteAllLines(csvPath, sampleData);

        // Verify that the CSV file exists before attempting to read it
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found.");
            return;
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);

        // Define the set of Codabar start/stop symbols to cycle through
        CodabarSymbol[] symbols = new CodabarSymbol[]
        {
            CodabarSymbol.A,
            CodabarSymbol.B,
            CodabarSymbol.C,
            CodabarSymbol.D
        };

        int index = 0;
        // Process each non‑empty line from the CSV
        foreach (string rawLine in lines)
        {
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Select the start/stop symbol based on the current index
            CodabarSymbol symbol = symbols[index % symbols.Length];
            string fileName = $"Codabar_{index + 1}_{symbol}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Generate the barcode with the selected symbol
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set module size (pixel width of the smallest bar)
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Apply the alternating start/stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = symbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = symbol;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated: {filePath}");
            index++;
        }

        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}