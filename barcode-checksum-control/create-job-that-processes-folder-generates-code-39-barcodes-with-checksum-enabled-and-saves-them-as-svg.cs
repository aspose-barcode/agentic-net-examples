using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation utility.
/// Scans a folder for files, uses each file name (without extension) as the barcode text,
/// and generates an SVG barcode image for each file.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the barcode generation process.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify the input folder.</param>
    static void Main(string[] args)
    {
        // Determine the folder to process: use first argument or default to a subfolder named "Input"
        string folderPath = args.Length > 0
            ? args[0]
            : Path.Combine(Directory.GetCurrentDirectory(), "Input");

        // Verify that the input folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Create an output folder for SVG files (will be created if it does not exist)
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all files in the input folder
        string[] files = Directory.GetFiles(folderPath);
        if (files.Length == 0)
        {
            Console.WriteLine($"No files found in folder: {folderPath}");
            return;
        }

        // Use Code39FullASCII symbology (the only Code39 variant supported)
        BaseEncodeType encodeType = EncodeTypes.Code39FullASCII;

        // Process each file individually
        foreach (string filePath in files)
        {
            // Use the file name (without extension) as the barcode text
            string codeText = Path.GetFileNameWithoutExtension(filePath);
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Skipping empty filename for path: {filePath}");
                continue;
            }

            // Build the output SVG file path
            string outputFileName = $"{codeText}.svg";
            string outputPath = Path.Combine(outputFolder, outputFileName);

            // Generate the barcode using Aspose.BarCode
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Enable checksum and display it in the human‑readable text
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the barcode as an SVG file (evaluation license supports Code39 for SVG)
                try
                {
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Generated SVG: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save SVG for '{codeText}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}