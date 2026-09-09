// Title: Barcode Confidence Level Reader with Checksum Validation
// Description: Demonstrates reading barcode images from a directory, applying default checksum validation, and printing each barcode's confidence (reading quality).
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarCodeGenerator to create sample barcodes and BarCodeReader to decode them, configuring ChecksumValidation for reliable results. Developers often need to batch‑process images, validate checksums, and retrieve reading quality for quality‑control or analytics purposes.
// Prompt: Create a console utility that accepts a directory path, applies ChecksumValidation.Default, and outputs each barcode's confidence level.
// Tags: barcode, checksumvalidation, reading, confidence, console, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console utility that reads barcode images from a directory, applies checksum validation,
/// and outputs each barcode's type, text, and reading quality (confidence level).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses the input directory, generates sample barcodes if needed,
    /// and processes each PNG file to display barcode information.
    /// </summary>
    /// <param name="args">Command‑line arguments; expects an optional directory path.</param>
    static void Main(string[] args)
    {
        // Determine the directory to process: use the provided path if valid,
        // otherwise create a temporary folder and generate sample barcodes.
        string inputDir;
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            inputDir = args[0];
        }
        else
        {
            inputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(inputDir);
            GenerateSampleBarcodes(inputDir);
            Console.WriteLine($"No valid directory provided. Generated sample barcodes in: {inputDir}");
        }

        // Retrieve all PNG files in the target directory.
        string[] files = Directory.GetFiles(inputDir, "*.png");
        if (files.Length == 0)
        {
            Console.WriteLine("No barcode images found in the directory.");
            return;
        }

        // Process each file individually.
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types.
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply checksum validation (default behavior).
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                    }
                    else
                    {
                        // Output details for each detected barcode.
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine(
                                $"File:{Path.GetFileName(file)} " +
                                $"Type:{result.CodeTypeName} " +
                                $"Text:{result.CodeText} " +
                                $"Quality:{result.ReadingQuality}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode image.
                Console.WriteLine($"Failed to read file {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Generates a set of sample barcode images in the specified folder.
    /// </summary>
    /// <param name="folder">The directory where sample images will be saved.</param>
    private static void GenerateSampleBarcodes(string folder)
    {
        // Define sample barcodes: type, text, and output file name.
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            (EncodeTypes.Code39, "123456", "Code39.png"),
            (EncodeTypes.Code11, "123456", "Code11.png"),
            (EncodeTypes.Code128, "Aspose123", "Code128.png")
        };

        // Create each barcode image using the generator.
        foreach (var sample in samples)
        {
            string path = Path.Combine(folder, sample.file);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Set a modest X‑dimension for better visibility.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(path, BarCodeImageFormat.Png);
            }
        }
    }
}