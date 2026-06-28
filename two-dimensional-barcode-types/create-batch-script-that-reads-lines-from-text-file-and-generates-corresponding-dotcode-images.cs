using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates DotCode barcodes from a list of codes read from a text file.
/// Each code is saved as a PNG image in the specified output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Reads codes from "codes.txt", creates a PNG barcode for each non‑empty line,
    /// and stores the images in the "DotCodeImages" folder.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path to the input text file containing one code per line.
        string inputPath = "codes.txt";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Directory where generated barcode images will be saved.
        string outputDir = "DotCodeImages";

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from the input file into an array.
        string[] lines = File.ReadAllLines(inputPath);
        int lineNumber = 0;

        // Process each line from the input file.
        foreach (string rawLine in lines)
        {
            lineNumber++;
            // Trim whitespace to obtain the actual code text.
            string codeText = rawLine.Trim();

            // Skip empty lines and report the omission.
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Line {lineNumber}: empty, skipped.");
                continue;
            }

            // Build a safe output file name using the line number.
            string outputPath = Path.Combine(outputDir, $"dotcode_{lineNumber}.png");

            try
            {
                // Initialize the barcode generator for DotCode with the current text.
                using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
                {
                    // Optional: set any DotCode‑specific parameters here if needed.
                    generator.Save(outputPath);
                }

                // Report successful generation.
                Console.WriteLine($"Line {lineNumber}: generated {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during barcode generation.
                Console.WriteLine($"Line {lineNumber}: error generating barcode - {ex.Message}");
            }
        }
    }
}