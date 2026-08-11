// Title: Batch processing CSV files to generate Code 16K barcode images
// Description: Demonstrates how to read CSV files from a directory, extract the first column as barcode data, and generate Code 16K barcode images saved as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating batch barcode creation from data sources. It uses the BarcodeGenerator class to configure Code 16K parameters, Aspose.Drawing for image handling, and typical file I/O for processing multiple CSV files. Developers often need to automate barcode production for inventory, shipping, or document labeling, and this pattern shows how to integrate Aspose.BarCode into such workflows.
// Prompt: Create batch job processing directory of CSV files, outputting Code 16K images.
// Tags: code16k, barcode, generation, png, csv, batch, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads CSV files from an input folder,
/// generates Code 16K barcodes for each row, and saves the images as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs directory setup, sample CSV creation,
    /// and batch barcode generation.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputCsv");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure the input and output directories exist
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Seed a sample CSV file if the input folder is empty
        string[] csvFiles = Directory.GetFiles(inputFolder, "*.csv");
        if (csvFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "Sample.csv");
            File.WriteAllLines(samplePath, new[]
            {
                "ABC123,Some other data",
                "XYZ789,More data",
                "CODE16K,Example"
            });
            csvFiles = new[] { samplePath };
        }

        // Process each CSV file found in the input folder
        foreach (string csvFile in csvFiles)
        {
            // Read all lines from the current CSV file
            string[] lines = File.ReadAllLines(csvFile);
            for (int i = 0; i < lines.Length; i++)
            {
                // Split the line by commas and take the first column as the barcode text
                string[] parts = lines[i].Split(',');
                if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0]))
                    continue;

                string codeText = parts[0].Trim();

                // Create a barcode generator configured for Code 16K
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set Code 16K specific parameters (aspect ratio and quiet zones)
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f; // default aspect ratio
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10; // minimum allowed
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1; // minimum allowed

                    // Optional: adjust module size (X dimension) and image resolution
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Resolution = 300f;

                    // Generate the barcode image as a bitmap
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        // Build the output file name using the CSV base name and row index
                        string baseName = Path.GetFileNameWithoutExtension(csvFile);
                        string outFile = Path.Combine(outputFolder, $"{baseName}_{i + 1}.png");

                        // Save the bitmap as a PNG file using Aspose.Drawing.Imaging.ImageFormat
                        bitmap.Save(outFile, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}