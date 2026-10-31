// Title: Batch generate GS1 Code 128 barcodes from CSV data
// Description: Demonstrates how to read a CSV file containing GS1 Code 128 AI strings, generate corresponding barcodes, and save them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.GS1Code128. It illustrates typical batch processing scenarios where developers need to create multiple barcodes from data sources such as CSV files, configure barcode parameters, and export images. Useful for inventory, logistics, and retail applications that require GS1-compliant barcodes.
// Prompt: Batch generate GS1 Code 128 barcodes from a CSV list of AI strings and output PNG files.
// Tags: gs1 code 128, batch generation, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads GS1 Code 128 AI strings from a CSV file,
/// generates barcodes for each entry, and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the input CSV and output PNG files
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a sample CSV file containing GS1 Code 128 AI strings
        string csvPath = Path.Combine(tempFolder, "input.csv");
        string[] sampleData = new string[]
        {
            "(01)12345678901231(21)ABC123",
            "(01)00123456789012(10)LOT001",
            "(01)00012345678901(17)250101",
            "(01)98765432109876(30)9876",
            "(01)12345678901231(21)XYZ"
        };
        File.WriteAllLines(csvPath, sampleData);

        // Verify that the CSV file was created successfully
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        // Create an output folder where the generated PNG files will be stored
        string outputFolder = Path.Combine(tempFolder, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Read each line from the CSV and generate a barcode image
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;
        foreach (string rawLine in lines)
        {
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty lines

            string outputPath = Path.Combine(outputFolder, $"barcode_{index}.png");
            try
            {
                // Initialize the barcode generator with GS1 Code 128 symbology and the AI string
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Set the X-dimension (module width) to 2 pixels for better readability
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated barcode {index}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for line {index}: {ex.Message}");
            }
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
        Console.WriteLine("Output folder: " + outputFolder);
    }
}