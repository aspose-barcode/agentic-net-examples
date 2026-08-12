// Title: Batch generation of DotCode barcodes from a text file
// Description: Demonstrates reading each line from a text file and creating a corresponding DotCode barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DotCode to produce PNG images in bulk. Typical use cases include automating barcode creation for inventory lists, product catalogs, or data migration tasks where each record requires its own barcode. Developers often need to read input data, configure symbology parameters, and save images efficiently.
// Prompt: Create a batch script that reads lines from a text file and generates corresponding DotCode images.
// Tags: dotcode, batch, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that reads a text file line‑by‑line and generates a DotCode barcode image for each entry.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional argument specifying the input file path; otherwise creates a sample file.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is the path to the input text file.</param>
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default sample file)
        string inputFile = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "DotCodeInput.txt");

        // If the input file does not exist, create a sample file with a few lines
        if (!File.Exists(inputFile))
        {
            string[] sampleLines = { "HelloWorld", "1234567890", "Aspose.BarCode", "DotCodeExample" };
            File.WriteAllLines(inputFile, sampleLines);
            Console.WriteLine($"Sample input file created at: {inputFile}");
        }

        // Read all non‑empty lines from the file
        string[] lines = File.ReadAllLines(inputFile);
        if (lines.Length == 0)
        {
            Console.WriteLine("Input file is empty. No barcodes to generate.");
            return;
        }

        // Create a dedicated output folder for the generated images
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Process each line and generate a DotCode image
        for (int i = 0; i < lines.Length; i++)
        {
            string text = lines[i].Trim();
            if (string.IsNullOrEmpty(text))
                continue; // skip empty lines

            string outputPath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            try
            {
                // Initialize the generator with DotCode symbology and the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, text))
                {
                    // Set only Columns; let the encoder decide the required rows
                    generator.Parameters.Barcode.DotCode.Columns = 20;

                    // Save the barcode as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for \"{text}\" -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Log the error and continue with the next line
                Console.WriteLine($"Failed to generate barcode for \"{text}\": {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}