// Title: Batch generate Codabar barcodes from CSV with alternating start symbols
// Description: Demonstrates how to read a list of data values from a CSV file and generate Codabar barcode images, alternating the start/stop symbols to add visual variety.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodabarSymbol classes. Typical use cases include bulk barcode creation for inventory, shipping, or ticketing systems where different start/stop symbols are required. Developers often need to read input data from files, configure barcode parameters, and save images in common formats.
// Prompt: Batch generate Codabar barcodes from a CSV file, applying alternating start symbols for visual variety.
// Tags: codabar,barcode,generation,csv,batch,aspose.barcode,images,alternating symbols

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Codabar barcodes in batch from a CSV file, alternating start/stop symbols for each entry.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads barcode data from a CSV file (or creates a sample), generates images, and saves them to a temporary folder.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the path to the CSV file.</param>
    static void Main(string[] args)
    {
        // Determine CSV path: use argument if provided and valid, otherwise create a sample CSV.
        string csvPath;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            csvPath = args[0];
        }
        else
        {
            // Create a temporary working folder for the sample CSV.
            string workFolder = Path.Combine(Path.GetTempPath(), "BatchCodabar_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workFolder);

            // Define sample data lines.
            csvPath = Path.Combine(workFolder, "data.csv");
            var sampleLines = new List<string>
            {
                "12345",
                "67890",
                "112233",
                "445566",
                "778899"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Verify that the CSV file exists before proceeding.
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Create an output folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the set of start/stop symbols to rotate through.
        CodabarSymbol[] symbols = new CodabarSymbol[]
        {
            CodabarSymbol.A,
            CodabarSymbol.B,
            CodabarSymbol.C,
            CodabarSymbol.D
        };

        int index = 0; // Tracks the current line number for naming and symbol selection.

        // Read the CSV file line by line.
        using (var reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // Skip empty lines.

                string codeText = line.Trim(); // Clean up the barcode text.

                // Select the start/stop symbol based on the current index (alternating).
                CodabarSymbol symbol = symbols[index % symbols.Length];
                index++;

                try
                {
                    // Initialize the barcode generator for Codabar with the current text.
                    using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
                    {
                        // Optional visual setting: set X‑dimension (module width) in pixels.
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;

                        // Apply the alternating start and stop symbols.
                        generator.Parameters.Barcode.Codabar.StartSymbol = symbol;
                        generator.Parameters.Barcode.Codabar.StopSymbol = symbol;

                        // Build the output file name and path.
                        string fileName = $"Codabar_{index}_{symbol}.png";
                        string outPath = Path.Combine(outputFolder, fileName);

                        // Save the barcode image as PNG.
                        generator.Save(outPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Generated: {outPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during barcode generation.
                    Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
                }
            }
        }

        // Inform the user where all generated barcodes are stored.
        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}