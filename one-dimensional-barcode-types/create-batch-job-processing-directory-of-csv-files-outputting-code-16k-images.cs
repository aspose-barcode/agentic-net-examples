using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Entry point for the barcode batch generation utility.
/// Reads CSV files from an input directory and creates Code 16K barcode images
/// for each non‑empty line, saving them to an output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the processing of CSV files and barcode generation.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – input directory path (defaults to "InputCsv"),
    /// args[1] – output directory path (defaults to "Barcodes").
    /// </param>
    static void Main(string[] args)
    {
        // Determine input directory (use provided argument or fallback to "InputCsv")
        string inputDir = args.Length > 0 ? args[0] : "InputCsv";

        // Determine output directory (use provided argument or fallback to "Barcodes")
        string outputDir = args.Length > 1 ? args[1] : "Barcodes";

        // Verify that the input directory exists; abort if it does not
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure the output directory exists (creates it if necessary)
        Directory.CreateDirectory(outputDir);

        // Retrieve all CSV files in the input directory
        string[] csvFiles = Directory.GetFiles(inputDir, "*.csv");

        // Process each CSV file individually
        foreach (string csvPath in csvFiles)
        {
            Console.WriteLine($"Processing file: {Path.GetFileName(csvPath)}");

            // Read all lines from the CSV; each line represents barcode text
            string[] lines = File.ReadAllLines(csvPath);
            int lineNumber = 0;

            // Iterate through each line, ignoring empty or whitespace‑only entries
            foreach (string rawLine in lines)
            {
                string codeText = rawLine.Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                lineNumber++;

                // Create a file‑system‑safe fragment from the barcode text
                string safeCode = MakeFileNameSafe(codeText);

                // Construct a unique image file name using the CSV name, row number, and safe code
                string imageFileName = $"{Path.GetFileNameWithoutExtension(csvPath)}_Row{lineNumber}_{safeCode}.png";
                string imagePath = Path.Combine(outputDir, imageFileName);

                // Generate the Code 16K barcode and save it as a PNG image
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Optional: adjust aspect ratio if required
                    // generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                    generator.Save(imagePath);
                }

                Console.WriteLine($"  Saved barcode for row {lineNumber} to {imageFileName}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    /// <summary>
    /// Converts a string into a file‑system‑safe name by replacing invalid characters
    /// and truncating the result to a reasonable length.
    /// </summary>
    /// <param name="text">The original text to sanitize.</param>
    /// <returns>A sanitized string suitable for use in a file name.</returns>
    private static string MakeFileNameSafe(string text)
    {
        // Replace each invalid file name character with an underscore
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            text = text.Replace(c, '_');
        }

        // Truncate to 20 characters to avoid excessively long file names
        return text.Length > 20 ? text.Substring(0, 20) : text;
    }
}