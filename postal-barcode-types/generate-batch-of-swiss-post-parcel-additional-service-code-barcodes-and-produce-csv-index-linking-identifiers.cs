using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Swiss Post Parcel barcodes and creating a CSV index file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for a set of service codes,
    /// saves them as PNG files, and writes an index CSV file listing each barcode.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample additional service codes for Swiss Post Parcel.
        List<string> serviceCodes = new List<string>
        {
            "A1", "A2", "A3", "A4", "A5"
        };

        // Prepare the CSV lines collection; start with the header row.
        List<string> csvLines = new List<string>();
        csvLines.Add("Id,CodeText,FileName");

        int id = 1; // Initialize a sequential identifier for each barcode.

        // Iterate over each service code to generate a corresponding barcode.
        foreach (string codeText in serviceCodes)
        {
            // Construct the file name and full path for the barcode image.
            string fileName = $"barcode_{id}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate the barcode using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                // Set the resolution to 300 DPI (optional configuration).
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode image to the specified file path.
                generator.Save(filePath);
            }

            // Add a line to the CSV index for the current barcode.
            csvLines.Add($"{id},{codeText},{fileName}");

            id++; // Increment the identifier for the next barcode.
        }

        // Write all CSV lines to the index file in the output directory.
        string csvPath = Path.Combine(outputDir, "index.csv");
        File.WriteAllLines(csvPath, csvLines);

        // Inform the user about the generation results.
        Console.WriteLine($"Generated {serviceCodes.Count} barcodes in '{outputDir}'.");
        Console.WriteLine($"CSV index created at '{csvPath}'.");
    }
}