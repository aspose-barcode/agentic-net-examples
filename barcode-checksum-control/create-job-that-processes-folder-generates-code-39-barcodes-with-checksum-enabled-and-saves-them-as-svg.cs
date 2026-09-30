// Title: Generate Code 39 Barcodes with Checksum and Save as SVG from Folder
// Description: The example reads text files from a temporary input folder, creates Code 39 barcodes with checksum enabled, and writes the barcodes as SVG files to an output folder.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to process multiple source files, configure barcode parameters (such as checksum), and export barcodes in vector format. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for SVG output—common tasks for developers automating barcode creation in batch workflows.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, checksum, svg, barcode generation, aspose.barcode, file processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code 39 barcodes with checksum enabled,
/// reading source data from text files and saving the barcodes as SVG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample input files, generates barcodes, and saves them as SVG.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary input folder and populate it with sample files
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodeInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // Sample data to encode
        var samples = new List<string> { "ABC123", "CODE39", "HELLO", "12345", "TEST" };
        for (int i = 0; i < samples.Count; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
            File.WriteAllText(filePath, samples[i]);
        }

        // Create a dedicated temporary output folder for SVG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Process each .txt file in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.txt");
        foreach (string file in files)
        {
            try
            {
                // Read the code text from the file and trim whitespace
                string codeText = File.ReadAllText(file).Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping empty file: {Path.GetFileName(file)}");
                    continue;
                }

                // Initialize the barcode generator for Code39 with full ASCII support
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
                {
                    // Enable checksum calculation for the barcode
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Determine the output SVG file path
                    string outputFileName = Path.GetFileNameWithoutExtension(file) + ".svg";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the generated barcode as an SVG image
                    try
                    {
                        generator.Save(outputPath, BarCodeImageFormat.Svg);
                        Console.WriteLine($"Generated barcode: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to save SVG for {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }

        // Cleanup: optionally delete temporary folders (commented out to allow inspection)
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}