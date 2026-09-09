// Title: Batch generation of DotCode barcodes from a text file
// Description: The example reads each line from an input text file and creates a DotCode barcode image for each non‑empty line, storing the PNG files in a temporary folder.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode to produce barcode images in bulk. Typical use cases include automating barcode creation for inventory lists, product catalogs, or any batch processing scenario where multiple codes need to be rendered quickly. Developers often need to read data sources, configure barcode parameters, and save images in common formats such as PNG.
// Prompt: Create a batch script that reads lines from a text file and generates corresponding DotCode images.
// Tags: dotcode, barcode generation, batch processing, png, aspose.barcode, csharp

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
    /// Entry point. Reads input.txt (creates a sample if missing) and generates PNG barcodes for each line.
    /// </summary>
    static void Main()
    {
        // Define the path to the input file located in the current working directory.
        string inputFile = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        // If the input file does not exist, create a sample file with example lines.
        if (!File.Exists(inputFile))
        {
            string[] sampleLines = new string[]
            {
                "HelloWorld",
                "Aspose",
                "DotCode123",
                "Sample Text",
                "1234567890"
            };
            File.WriteAllLines(inputFile, sampleLines);
            Console.WriteLine($"Sample input file created at: {inputFile}");
        }

        // Create a unique temporary folder to store the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Read all lines from the input file.
        string[] lines = File.ReadAllLines(inputFile);
        for (int i = 0; i < lines.Length; i++)
        {
            string codeText = lines[i];

            // Skip empty or whitespace-only lines.
            if (string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Line {i + 1} is empty, skipping.");
                continue;
            }

            // Build the output file path for the current barcode image.
            string outputPath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");
            try
            {
                // Initialize the barcode generator for DotCode with the current line's text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
                {
                    // Set a reasonable X dimension (pixel size) for the barcode modules.
                    generator.Parameters.Barcode.XDimension.Pixels = 5f;

                    // Save the generated barcode as a PNG image.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated barcode for line {i + 1}: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation.
                Console.WriteLine($"Failed to generate barcode for line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}