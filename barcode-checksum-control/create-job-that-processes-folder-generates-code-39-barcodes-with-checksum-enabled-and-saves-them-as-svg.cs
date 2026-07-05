// Title: Generate Code 39 Barcodes with Checksum and Save as SVG
// Description: The program reads text files from an input folder, creates Code 39 barcodes with checksum enabled, and writes the barcodes as SVG files to an output folder.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, barcode, checksum, svg, aspose.barcode, file-processing, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate Code 39 barcodes (with checksum) from text files
/// and save the resulting images as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes each file in the input folder,
    /// creates a barcode, and stores it in the output folder.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the input folder exists; create it if missing.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the input folder is empty, create a sample text file to demonstrate functionality.
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "Sample1.txt");
            File.WriteAllText(samplePath, "ABC-123");
        }

        // Ensure the output folder exists; create it if missing.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over each file in the input folder and generate a corresponding barcode.
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            try
            {
                // Derive the barcode file name from the source file name (without extension).
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);

                // Read the file content to be encoded; trim whitespace to avoid empty codes.
                string codeText = File.ReadAllText(filePath).Trim();

                // Skip processing if the file contains no usable text.
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping empty file: {filePath}");
                    continue;
                }

                // Initialise the barcode generator for Code39FullASCII with the provided text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
                {
                    // Enable checksum calculation for the barcode.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Construct the full output path for the SVG file.
                    string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}.svg");

                    // Save the generated barcode as an SVG image.
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Generated barcode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing an individual file.
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        // End of processing – the program exits automatically.
    }
}