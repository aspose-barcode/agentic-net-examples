// Title: Batch generate GS1 Code 128 barcodes from CSV and save as PNG
// Description: Demonstrates how to read a CSV file containing GS1 Code 128 AI strings, generate barcodes using Aspose.BarCode, and save each as a PNG image in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.GS1Code128. It illustrates typical batch processing scenarios where developers need to create multiple barcodes from data sources such as CSV files, configure barcode parameters (e.g., X‑Dimension), and output images in common formats like PNG. Ideal for inventory, logistics, and retail applications that require automated barcode creation.
// Prompt: Batch generate GS1 Code 128 barcodes from a CSV list of AI strings and output PNG files.
// Tags: barcode, gs1code128, csv, batch, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads GS1 Code 128 AI strings from a CSV file,
/// generates corresponding barcodes, and saves each barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample CSV content: each line is a GS1 Code128 AI string
        string[] csvLines = new[]
        {
            "(01)12345678901231(21)ITEM001",
            "(01)00123456789012(21)ITEM002",
            "(01)00012345678901(21)ITEM003",
            "(01)98765432109876(21)ITEM004",
            "(01)12345098765432(21)ITEM005"
        };

        // Write the sample CSV to a file in the batch folder
        string csvPath = Path.Combine(batchFolder, "input.csv");
        File.WriteAllLines(csvPath, csvLines);

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        // Process each non‑empty line and generate a barcode image
        foreach (string rawLine in lines)
        {
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Determine the output file path for the current barcode
            string outputPath = Path.Combine(batchFolder, $"barcode_{index}.png");
            try
            {
                // Initialize the barcode generator with GS1 Code 128 symbology and the AI string
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Set the X‑Dimension (module width) to 2 pixels for better readability
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {index}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for line {index}: {ex.Message}");
            }

            index++;
        }

        Console.WriteLine($"Batch processing completed. Files are located in: {batchFolder}");
    }
}