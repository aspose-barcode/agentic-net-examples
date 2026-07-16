// Title: Generate DotCode barcodes from a text file in batch
// Description: This example reads each line from a text file and creates a DotCode barcode image for it, saving the results to an output folder.
// Category-Description: Demonstrates batch processing of barcode generation using Aspose.BarCode for .NET. It showcases the BarcodeGenerator class with EncodeTypes.DotCode, file I/O, and image saving, typical for scenarios where multiple barcodes need to be produced automatically from a data source. Developers often use this pattern to integrate barcode creation into scripts, services, or build pipelines.
// Prompt: Create a batch script that reads lines from a text file and generates corresponding DotCode images.
// Tags: dotcode, barcode, generation, image, aspose.barcodes, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Batch processes a text file to generate DotCode barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads input lines, generates barcodes, and saves them as PNG files.
    /// </summary>
    /// <param name="args">Optional command‑line arguments. The first argument can specify the input file path.</param>
    static void Main(string[] args)
    {
        // Determine input file path: use first argument if provided, otherwise default to "input.txt".
        string inputPath = args.Length > 0 ? args[0] : "input.txt";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists; create it if necessary.
        string outputDir = "output";
        Directory.CreateDirectory(outputDir);

        const int maxItems = 10; // Safety cap to limit the number of barcodes generated in a single run.
        int processed = 0;       // Counter for successfully processed lines.

        // Open the input file for reading line by line.
        using (var reader = new StreamReader(inputPath))
        {
            string line;
            // Continue reading until end of file or the maximum item count is reached.
            while ((line = reader.ReadLine()) != null && processed < maxItems)
            {
                // Skip empty or whitespace‑only lines.
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Trim the line to obtain the barcode text.
                string codeText = line.Trim();

                // Build the output file path, naming files sequentially.
                string outputPath = Path.Combine(outputDir, $"dotcode_{processed + 1}.png");

                // Create a BarcodeGenerator for DotCode symbology and save the image.
                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
                {
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated: {outputPath}");
                processed++;
            }
        }

        Console.WriteLine($"Processing complete. {processed} barcode(s) generated.");
    }
}