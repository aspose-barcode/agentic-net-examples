using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program to generate GS1 DataMatrix barcodes from AI strings and save them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates PNG files for a list of GS1 AI strings.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample list of GS1 AI strings (fallback if no arguments are provided)
        List<string> aiStrings = new List<string>
        {
            "(01)12345678901231",
            "(01)98765432109876",
            "(01)55555555555555",
            "(01)11111111111111",
            "(01)22222222222222"
        };

        // Determine output directory for generated PNG files
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "GS1DataMatrixOutput");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each AI string in parallel to improve performance
        Parallel.ForEach(aiStrings, aiString =>
        {
            // Create a safe file name by removing characters illegal in file names
            string safeFileName = aiString.Replace("(", "").Replace(")", "").Replace(" ", "_") + ".png";

            // Combine the output directory with the safe file name
            string outputPath = Path.Combine(outputDir, safeFileName);

            // Generate the GS1 DataMatrix barcode for the current AI string
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, aiString))
            {
                // Optional: set resolution or other parameters if needed
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode as a PNG file
                generator.Save(outputPath);
            }

            // Inform the user that the file has been generated
            Console.WriteLine($"Generated: {outputPath}");
        });

        // Indicate that all conversions have finished
        Console.WriteLine("Batch conversion completed.");
    }
}