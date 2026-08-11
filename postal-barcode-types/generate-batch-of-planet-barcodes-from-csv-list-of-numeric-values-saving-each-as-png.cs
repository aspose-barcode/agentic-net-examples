// Title: Generate Planet Barcodes from CSV Values
// Description: Demonstrates creating Planet symbology barcodes from a comma‑separated list of numeric strings and saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Planet to produce barcodes. Typical use cases include batch barcode creation from data sources such as CSV files, where each value is rendered as an image for printing or digital distribution. Developers often need to automate image output, manage file naming, and handle directory creation, which this snippet illustrates.
// Prompt: Generate a batch of Planet barcodes from a CSV list of numeric values, saving each as PNG.
// Tags: planet, barcode, generation, csv, png, aspose.barcode, encode-types, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads numeric values from a CSV string,
/// generates a Planet barcode for each value, and saves the barcodes as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Sample CSV data containing numeric values
        string csvData = "12345,67890,112233,445566,778899";

        // Split the CSV string into individual values, ignoring empty entries
        string[] values = csvData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Prepare the output directory for the generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "PlanetBarcodes");
        Directory.CreateDirectory(outputDir);

        // Iterate over each numeric value and generate a corresponding Planet barcode
        foreach (string rawValue in values)
        {
            // Trim whitespace and skip empty entries
            string value = rawValue.Trim();
            if (string.IsNullOrEmpty(value))
                continue;

            // Construct a safe file name for the barcode image
            string fileName = $"planet_{value}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Create and configure the barcode generator for Planet symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet))
            {
                generator.CodeText = value; // Set the data to encode

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the generated file
            Console.WriteLine($"Generated Planet barcode for value {value} -> {filePath}");
        }

        // Indicate that the batch process has finished
        Console.WriteLine("Barcode generation completed.");
    }
}