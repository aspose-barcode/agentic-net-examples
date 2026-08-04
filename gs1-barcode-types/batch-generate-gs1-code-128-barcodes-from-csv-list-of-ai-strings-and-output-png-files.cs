// Title: Batch generate GS1 Code 128 barcodes from CSV and save as PNG
// Description: Demonstrates reading a CSV file of GS1 AI strings, generating a GS1 Code 128 barcode for each entry, and saving the images as PNG files.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode for .NET. It shows how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to create barcodes from data sources, configure visual parameters, and export to image formats. Developers often need to batch‑process data files to produce barcodes for labeling, inventory, or shipping applications.
// Prompt: Batch generate GS1 Code 128 barcodes from a CSV list of AI strings and output PNG files.
// Tags: gs1code128, batch, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates GS1 Code 128 barcodes from a list of AI strings stored in a CSV file
/// and saves each barcode as a PNG image in an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Handles CSV input, creates sample data if needed,
    /// configures barcode generation settings, and writes PNG files.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV path (argument or default)
        string csvPath = args.Length > 0 ? args[0] : "sample.csv";

        // If CSV does not exist, create a small sample file with GS1 AI strings
        if (!File.Exists(csvPath))
        {
            string[] sampleData = new string[]
            {
                "(01)00123456789012",
                "(01)12345678901231",
                "(01)00012345678905",
                "(01)98765432109876",
                "(01)11111111111111"
            };
            File.WriteAllLines(csvPath, sampleData);
            Console.WriteLine($"Sample CSV created at {Path.GetFullPath(csvPath)}");
        }

        // Prepare output folder for generated barcode images
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Read each line (AI string) from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;
        foreach (string rawLine in lines)
        {
            // Trim whitespace and skip empty lines
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Build output file name (e.g., barcode_1.png)
            string outputFile = Path.Combine(outputFolder, $"barcode_{index}.png");

            // Create a barcode generator for GS1 Code 128 with the current AI string
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Do not throw an exception if the code text has minor format issues
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Optional visual settings: filled bars and X-dimension
                generator.Parameters.Barcode.FilledBars = true;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode image; the file extension determines the format (PNG)
                generator.Save(outputFile);
            }

            Console.WriteLine($"Generated: {outputFile}");
            index++;
        }
    }
}