// Title: Batch generate Codabar barcodes from CSV with alternating start symbols
// Description: Demonstrates reading a CSV file, creating a Codabar barcode for each entry, and saving the images as PNG files while alternating start/stop symbols for visual variety.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases the use of BarcodeGenerator, EncodeTypes, and Codabar-specific parameters to produce multiple barcodes in a single run. Typical scenarios include bulk label creation, inventory tagging, and automated report generation where developers need to process lists of data and output consistent barcode images.
// Prompt: Batch generate Codabar barcodes from a CSV file, applying alternating start symbols for visual variety.
// Tags: codabar, barcode, batch, csv, generation, png, aspose.barcode, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a batch of Codabar barcodes from a CSV file, alternating start/stop symbols for each entry.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, writes sample CSV data,
    /// reads each line, generates a Codabar barcode with an alternating start/stop symbol,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a sample CSV file with data strings (Codabar allows digits and -$./:+)
        string csvPath = Path.Combine(batchFolder, "data.csv");
        string[] sampleData = new string[] { "12345", "67890", "112233", "445566", "778899" };
        File.WriteAllLines(csvPath, sampleData);

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);

        // Define the set of start/stop symbols to rotate through
        CodabarSymbol[] symbols = new CodabarSymbol[] { CodabarSymbol.A, CodabarSymbol.B, CodabarSymbol.C, CodabarSymbol.D };

        // Process each line and generate a barcode
        for (int i = 0; i < lines.Length; i++)
        {
            string data = lines[i].Trim();

            // Skip empty lines
            if (string.IsNullOrEmpty(data))
                continue;

            // Select the start/stop symbol based on the current index
            CodabarSymbol startStopSymbol = symbols[i % symbols.Length];

            // Build the full Codabar text including start/stop symbols
            string codeText = $"{startStopSymbol}{data}{startStopSymbol}";

            // Determine the output file path
            string outputPath = Path.Combine(batchFolder, $"barcode_{i + 1}_{startStopSymbol}.png");

            try
            {
                // Initialize the barcode generator with Codabar encoding
                using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
                {
                    // Optional visual settings
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.Codabar.StartSymbol = startStopSymbol;
                    generator.Parameters.Barcode.Codabar.StopSymbol = startStopSymbol;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}