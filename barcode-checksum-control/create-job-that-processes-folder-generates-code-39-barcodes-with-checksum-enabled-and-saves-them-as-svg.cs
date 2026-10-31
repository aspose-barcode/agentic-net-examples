// Title: Generate Code 39 barcodes with checksum and save as SVG from folder
// Description: Demonstrates processing a folder of text files, creating Code 39 barcodes with checksum enabled, and saving each as an SVG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code39FullASCII, configure checksum, and export barcodes to SVG format. Typical use cases include batch barcode creation from data files, automated report generation, and integration into document workflows. Developers often need to read input data, configure barcode parameters, and save images in various formats, which this snippet showcases.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, checksum, svg, barcode generation, aspose.barcode, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code 39 barcodes with checksum enabled, saving each as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary input and output folders, writes sample data, reads each file,
    /// generates a barcode, and saves it as SVG.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a unique temporary input folder and populate it with sample text files
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodesInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        var sampleData = new Dictionary<string, string>
        {
            { "Item1.txt", "CODE39A" },
            { "Item2.txt", "12345" },
            { "Item3.txt", "HELLO-123" }
        };

        var inputFiles = new List<string>();
        foreach (var kvp in sampleData)
        {
            // Write each sample text to a separate file
            string filePath = Path.Combine(inputFolder, kvp.Key);
            File.WriteAllText(filePath, kvp.Value);
            inputFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Create a unique temporary output folder for the generated SVG files
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Process each input file: read text, generate barcode, and save as SVG
        // --------------------------------------------------------------------
        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Read and trim the barcode text from the file
            string codeText = File.ReadAllText(filePath).Trim();
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Empty code text in file: {filePath}");
                continue;
            }

            // Determine the output SVG file path
            string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".svg";
            string outputPath = Path.Combine(outputFolder, outputFileName);

            try
            {
                // Initialize the barcode generator with Code39FullASCII symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
                {
                    // Enable checksum calculation for the barcode
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the generated barcode as an SVG image
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                }

                Console.WriteLine($"Generated SVG: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}