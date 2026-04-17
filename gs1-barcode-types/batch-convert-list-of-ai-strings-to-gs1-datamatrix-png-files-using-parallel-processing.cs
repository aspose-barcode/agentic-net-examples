using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Sample list of GS1 AI strings (AI format with parentheses)
        List<string> aiStrings = new List<string>
        {
            "(01)12345678901231(10)ABC123",
            "(01)98765432109876(21)XYZ789",
            "(01)55555555555555(17)210101",
            "(01)11111111111111(3103)001500",
            "(01)22222222222222(3102)002000"
        };

        // Output folder for PNG files
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "GS1DataMatrixOutputs");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each AI string in parallel
        Parallel.ForEach(aiStrings, aiString =>
        {
            try
            {
                // Create a barcode generator for GS1 DataMatrix
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, aiString))
                {
                    // Optional: set image resolution or size if needed
                    // generator.Parameters.Resolution = 300;

                    // Build a safe file name from the AI string (remove illegal characters)
                    string safeFileName = GetSafeFileName(aiString) + ".png";
                    string outputPath = Path.Combine(outputFolder, safeFileName);

                    // Save the barcode as PNG
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for '{aiString}': {ex.Message}");
            }
        });
    }

    // Helper to create a file-system‑safe name from the AI string
    private static string GetSafeFileName(string input)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }
        // Replace parentheses to keep the AI information readable
        return input.Replace('(', '_').Replace(')', '_');
    }
}