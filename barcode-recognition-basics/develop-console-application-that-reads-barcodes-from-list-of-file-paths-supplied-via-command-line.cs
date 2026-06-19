using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a sample barcode (if no input files are provided) and reading barcodes from image files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts file paths as command‑line arguments, generates a sample barcode if none are supplied,
    /// and attempts to read and display barcode information from each file.
    /// </summary>
    /// <param name="args">Array of file paths supplied via the command line.</param>
    static void Main(string[] args)
    {
        // Collect file paths from command line or use a generated sample.
        List<string> filePaths = new List<string>();

        if (args.Length > 0)
        {
            // Use the paths provided by the user.
            filePaths.AddRange(args);
        }
        else
        {
            // No arguments supplied – generate a temporary barcode image to demonstrate reading.
            string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
            try
            {
                // Create a barcode generator for Code128 with sample text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Save the generated barcode as a PNG file.
                    generator.Save(tempImagePath, BarCodeImageFormat.Png);
                }

                // Add the generated image to the list of files to process.
                filePaths.Add(tempImagePath);
                Console.WriteLine($"Generated sample barcode image at: {tempImagePath}");
            }
            catch (Exception ex)
            {
                // Report generation failure but continue execution (no files to process).
                Console.WriteLine($"Failed to generate sample barcode: {ex.Message}");
            }
        }

        // Process each file in the collection.
        foreach (string path in filePaths)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            try
            {
                // Initialize a barcode reader that supports all barcode types.
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    bool anyFound = false;

                    // Iterate through all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"File: {path}");
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                        Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                    }

                    // If no barcodes were found, inform the user.
                    if (!anyFound)
                    {
                        Console.WriteLine($"No barcodes detected in file: {path}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while reading the file.
                Console.WriteLine($"Error reading file '{path}': {ex.Message}");
            }
        }
    }
}