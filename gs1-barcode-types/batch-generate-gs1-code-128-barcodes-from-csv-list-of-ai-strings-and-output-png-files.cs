// Title: Batch generate GS1 Code 128 barcodes from CSV
// Description: Demonstrates reading a CSV of GS1 AI strings, generating corresponding GS1 Code 128 barcodes, and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1Code128. It illustrates typical use cases such as batch processing of product identifiers, creating barcode images for inventory or labeling systems, and handling temporary file management. Developers often need to read data sources, configure barcode parameters, and export images in common formats.
// Prompt: Batch generate GS1 Code 128 barcodes from a CSV list of AI strings and output PNG files.
// Tags: barcode, gs1code128, csv, batch, png, generation, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads GS1 AI strings from a CSV file,
/// generates GS1 Code 128 barcodes for each entry, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string tempRoot = Path.Combine(Path.GetTempPath(), "Gs1Batch_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(tempRoot, "Output");
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file with GS1 Code 128 codetexts
        string csvPath = Path.Combine(tempRoot, "input.csv");
        string[] sampleLines = new string[]
        {
            "(01)01234567890128",                                 // GTIN-14 only
            "(01)01234567890128(21)ABC123",                       // GTIN + serial
            "(01)01234567890128(10)LOT2023(21)XYZ789"             // GTIN + lot + serial
        };
        File.WriteAllLines(csvPath, sampleLines, Encoding.UTF8);

        // Read the CSV lines
        string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);
        int index = 1;
        foreach (string rawLine in lines)
        {
            // Trim whitespace and skip empty lines
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            try
            {
                // Initialize the barcode generator for GS1 Code 128 with the current AI string
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Optional visual settings: set X-dimension and place human‑readable text below the barcode
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                    // Build the output file path and save the barcode as a PNG image
                    string outputPath = Path.Combine(outputFolder, $"barcode_{index}.png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode {index}: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for this line
                Console.WriteLine($"Failed to generate barcode {index} for text '{codeText}': {ex.Message}");
            }

            index++;
        }

        // Cleanup note (optional): the temporary folder can be deleted after use
        // Directory.Delete(tempRoot, true);
    }
}