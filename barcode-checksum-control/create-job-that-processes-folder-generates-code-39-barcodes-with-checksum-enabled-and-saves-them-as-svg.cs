// Title: Generate Code 39 Barcodes with Checksum and Save as SVG
// Description: The example creates temporary input files, generates Code 39 full ASCII barcodes with checksum enabled for each file name, and saves the barcodes as SVG images.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce barcodes from file names. Typical use cases include batch processing of documents, inventory labeling, and automated report generation where each item needs a unique barcode. Developers often need to enable checksums for data integrity and export barcodes to vector formats like SVG for scalable rendering.
// Prompt: Create a job that processes a folder, generates Code 39 barcodes with checksum enabled, and saves them as SVG.
// Tags: code39, checksum, svg, barcode generation, aspose.barcode, file processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates processing a folder of files, generating Code 39 barcodes with checksum enabled,
/// and saving each barcode as an SVG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample input files, generates barcodes, and writes SVG output.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary input folder and create sample text files.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodesInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"File{i}.txt");
            File.WriteAllText(filePath, $"SampleContent{i}");
        }

        // --------------------------------------------------------------------
        // Set up a temporary output folder where SVG barcode images will be saved.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Process each file in the input folder: generate a barcode from the file name.
        // --------------------------------------------------------------------
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string file in files)
        {
            // Use the file name (without extension) as the barcode text.
            string codeText = Path.GetFileNameWithoutExtension(file);
            string outputPath = Path.Combine(outputFolder, $"{codeText}.svg");

            // Initialize the barcode generator with Code39FullASCII symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum calculation for data integrity.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                try
                {
                    // Save the generated barcode as an SVG file.
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Generated SVG for '{codeText}' at '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during barcode generation or saving.
                    Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}