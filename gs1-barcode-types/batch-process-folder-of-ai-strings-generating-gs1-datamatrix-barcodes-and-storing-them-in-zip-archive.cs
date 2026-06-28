using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates GS1 DataMatrix barcodes from AI string files and packages them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: input folder path and output ZIP file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine input folder (default: "Input") and output ZIP path (default: "Barcodes.zip")
        string inputFolder = args.Length > 0 ? args[0] : "Input";
        string outputZipPath = args.Length > 1 ? args[1] : "Barcodes.zip";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create or overwrite the ZIP archive
        using (var zipFileStream = new FileStream(outputZipPath, FileMode.Create, FileAccess.ReadWrite))
        {
            using (var zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                // Iterate over each file in the input folder
                foreach (string filePath in Directory.GetFiles(inputFolder))
                {
                    // Extract file name without extension for later use
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string codeText;

                    // Read the AI string from the file, trimming whitespace
                    try
                    {
                        codeText = File.ReadAllText(filePath).Trim();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to read file '{filePath}': {ex.Message}");
                        continue; // Skip to next file on read error
                    }

                    // Skip empty files
                    if (string.IsNullOrEmpty(codeText))
                    {
                        Console.WriteLine($"File '{filePath}' is empty. Skipping.");
                        continue;
                    }

                    // Generate GS1 DataMatrix barcode using Aspose.BarCode
                    using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                    {
                        // Set desired resolution (dots per inch)
                        generator.Parameters.Resolution = 300f;

                        // Render barcode to an in‑memory PNG image
                        using (var imageStream = new MemoryStream())
                        {
                            try
                            {
                                generator.Save(imageStream, BarCodeImageFormat.Png);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed to generate barcode for '{filePath}': {ex.Message}");
                                continue; // Skip to next file on generation error
                            }

                            // Reset stream position before copying
                            imageStream.Position = 0;

                            // Create a new entry in the ZIP archive for the PNG image
                            var entry = zipArchive.CreateEntry($"{fileName}.png", CompressionLevel.Optimal);
                            using (var entryStream = entry.Open())
                            {
                                // Copy the PNG data into the ZIP entry
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }

                    // Log successful processing of the current file
                    Console.WriteLine($"Processed '{filePath}' -> '{fileName}.png'");
                }
            }
        }

        // Inform the user that all barcodes have been saved
        Console.WriteLine($"All barcodes have been saved to '{outputZipPath}'.");
    }
}