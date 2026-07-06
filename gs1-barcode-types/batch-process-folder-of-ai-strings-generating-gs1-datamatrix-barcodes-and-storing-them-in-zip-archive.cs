// Title: Batch generate GS1 DataMatrix barcodes from AI strings and zip them
// Description: Reads AI strings from text files, creates GS1 DataMatrix barcodes, and stores the PNG images in a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use BarcodeGenerator with EncodeTypes.GS1DataMatrix for batch processing. It shows typical steps such as reading input data, configuring barcode parameters, rendering images, and packaging results into a ZIP file—common tasks for developers automating barcode creation workflows.
// Prompt: Batch process a folder of AI strings, generating GS1 DataMatrix barcodes and storing them in a ZIP archive.
// Tags: gs1, datamatrix, barcode, generation, batch processing, zip, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of AI strings to generate GS1 DataMatrix barcodes
/// and archive the resulting PNG images into a ZIP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts optional command‑line arguments for the input folder
    /// and output ZIP file path, then performs the barcode generation and archiving workflow.
    /// </summary>
    /// <param name="args">
    /// args[0] – input folder containing AI string files (default: "InputAIs").
    /// args[1] – output ZIP file path (default: "Barcodes.zip").
    /// </param>
    static void Main(string[] args)
    {
        // Determine input folder; fall back to default if not supplied.
        string inputFolder = args.Length > 0 ? args[0] : "InputAIs";

        // Determine output ZIP file path; fall back to default if not supplied.
        string outputZipPath = args.Length > 1 ? args[1] : "Barcodes.zip";

        // Ensure the input folder exists; if missing, create sample AI files for demonstration.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);

            // Sample GS1 AI strings (must be wrapped in parentheses).
            string[] sampleAIs = new string[]
            {
                "(01)01234567890123",
                "(01)09876543210987",
                "(01)12345678901234",
                "(01)56789012345678",
                "(01)00011122233344"
            };

            // Write each sample AI string to a separate text file.
            for (int i = 0; i < sampleAIs.Length; i++)
            {
                string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
                File.WriteAllText(filePath, sampleAIs[i]);
            }
        }

        // Retrieve all files (any extension) from the input folder.
        string[] files = Directory.GetFiles(inputFolder);
        if (files.Length == 0)
        {
            Console.WriteLine("No AI files found in the input folder.");
            return;
        }

        // Create the ZIP archive that will hold the generated barcode images.
        using (FileStream zipToCreate = new FileStream(outputZipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
        {
            // Process each file individually.
            foreach (string file in files)
            {
                // Read the AI string from the file and trim any surrounding whitespace.
                string aiString = File.ReadAllText(file).Trim();

                // Skip empty files and continue with the next iteration.
                if (string.IsNullOrEmpty(aiString))
                {
                    Console.WriteLine($"Skipping empty file: {Path.GetFileName(file)}");
                    continue;
                }

                // Initialize the barcode generator for GS1 DataMatrix using the AI string.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, aiString))
                {
                    // Set a reasonable X-dimension (pixel size) for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;

                    // Enable automatic sizing using interpolation mode.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Render the barcode to a memory stream in PNG format.
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for copying.

                        // Create a ZIP entry named after the source file but with a .png extension.
                        string entryName = Path.GetFileNameWithoutExtension(file) + ".png";
                        ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Copy the PNG data into the ZIP entry.
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(file)}");
            }
        }

        Console.WriteLine($"All barcodes have been saved to '{outputZipPath}'.");
    }
}