// Title: Batch decode Planet barcodes and generate CSV report
// Description: Demonstrates generating Planet barcode images, decoding them in batch, and creating a CSV summary report.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode multiple images, and standard .NET file I/O to produce a CSV file. Developers working with bulk barcode processing, inventory systems, or reporting often need to generate barcodes, read them from files, and export results for analysis.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, batch, decoding, csv, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Planet barcodes, decodes them from PNG files,
/// and writes a CSV report containing file name, decoded text, and reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPlanet_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data that will be encoded into Planet barcodes
        List<string> codes = new List<string>
        {
            "123456",
            "9876543210",
            "5555555555",
            "0000012345",
            "9999999999"
        };

        // Generate PNG images for each barcode value
        List<string> imageFiles = new List<string>();
        foreach (string code in codes)
        {
            string filePath = Path.Combine(tempFolder, $"Planet_{code}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, code))
            {
                // Set the X-dimension (module width) to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare the CSV header line
        List<string> csvLines = new List<string>();
        csvLines.Add("FileName,CodeText,ReadingQuality");

        // Decode each generated image and collect results
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Filter only Planet symbology results
                        if (result.CodeTypeName == "Planet")
                        {
                            string line = $"{Path.GetFileName(file)},{result.CodeText},{result.ReadingQuality}";
                            csvLines.Add(line);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be loaded as a barcode image
                Console.WriteLine($"Skipping file due to load error: {file}. Message: {ex.Message}");
            }
        }

        // Write the collected data to a CSV file in the temporary folder
        string reportPath = Path.Combine(tempFolder, "PlanetBarcodesReport.csv");
        File.WriteAllLines(reportPath, csvLines);
        Console.WriteLine($"CSV report generated at: {reportPath}");
    }
}