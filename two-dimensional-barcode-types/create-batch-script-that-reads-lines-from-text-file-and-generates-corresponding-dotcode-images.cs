// Title: Generate DotCode barcodes from a text file in batch
// Description: This example reads each line from a given text file and creates a DotCode barcode image for each line, storing the PNG files in a designated output folder.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. It showcases how to read data from a file, loop through entries, and generate DotCode symbols with the BarcodeGenerator class (EncodeTypes.DotCode) and save them as PNG images (BarCodeImageFormat.Png). Typical scenarios include mass‑producing barcodes for inventory, shipping labels, or product catalogs where data is supplied in a simple text list.
// Prompt: Create a batch script that reads lines from a text file and generates corresponding DotCode images.
// Tags: dotcode, barcode generation, batch processing, file input, png output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of DotCode barcode images from a text file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads input file path and optional output folder from arguments,
    /// generates up to 10 DotCode PNG images (one per line), and writes status messages to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] input file path (optional), [1] output folder (optional).</param>
    static void Main(string[] args)
    {
        // Resolve the input file path: use argument if provided, otherwise create a temporary sample file.
        string inputPath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputPath = args[0];
        }
        else
        {
            // Create a temporary directory for the sample file.
            string tempDir = Path.Combine(Path.GetTempPath(), "DotCodeBatchSample");
            Directory.CreateDirectory(tempDir);
            inputPath = Path.Combine(tempDir, "sample.txt");

            // Populate the sample file with a few example lines if it does not already exist.
            if (!File.Exists(inputPath))
            {
                string[] sampleLines = { "Hello", "Aspose", "DotCode123", "Sample Text" };
                File.WriteAllLines(inputPath, sampleLines);
            }
        }

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Resolve the output folder: use argument if provided, otherwise create a unique temporary folder.
        string outputFolder;
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputFolder = args[1];
        }
        else
        {
            outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBatch_" + Guid.NewGuid().ToString("N"));
        }
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the input file.
        string[] lines = File.ReadAllLines(inputPath);
        // Limit the batch size to a maximum of 10 items for safety.
        int maxItems = Math.Min(lines.Length, 10);

        // Process each line and generate a corresponding DotCode barcode image.
        for (int i = 0; i < maxItems; i++)
        {
            string text = lines[i];
            string fileName = $"barcode_{i + 1}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialize the barcode generator for DotCode with the current line's text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, text))
                {
                    // Optional: set the module size (pixel dimension) for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 5f;
                    // Save the generated barcode as a PNG file.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}