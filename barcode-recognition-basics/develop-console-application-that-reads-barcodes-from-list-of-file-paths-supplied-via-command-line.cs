// Title: Barcode Reader Console Example
// Description: Demonstrates reading barcodes from image files supplied via command line, generating sample images when none are provided.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the BarCodeReader class to decode all supported symbologies. Typical use cases include batch processing of scanned documents or image files to extract embedded data. Developers often need to iterate over file collections, handle missing files, and output decoded information, which this sample illustrates.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode, reading, console, batch, aspose.barcode, decode, all-supported-types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads barcodes from image files provided via command line.
/// Generates sample barcode images if no arguments are supplied.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts file paths as arguments, reads barcodes using BarCodeReader, and writes results to console.
    /// </summary>
    /// <param name="args">Array of file paths to process.</param>
    static void Main(string[] args)
    {
        string[] filePaths;

        // If no command‑line arguments are provided, generate sample barcode images.
        if (args.Length == 0)
        {
            // Create a folder for sample images in the current working directory.
            string sampleDir = Path.Combine(Directory.GetCurrentDirectory(), "SampleBarcodes");
            Directory.CreateDirectory(sampleDir);

            // Prepare an array to hold the generated sample file paths.
            string[] samples = new string[3];

            // ---- Sample Code128 barcode ----
            string code128Path = Path.Combine(sampleDir, "code128.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(code128Path);
            }
            samples[0] = code128Path;

            // ---- Sample QR code ----
            string qrPath = Path.Combine(sampleDir, "qr.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generator.Save(qrPath);
            }
            samples[1] = qrPath;

            // ---- Sample DataMatrix barcode ----
            string dmPath = Path.Combine(sampleDir, "datamatrix.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
            {
                generator.Save(dmPath);
            }
            samples[2] = dmPath;

            // Use the generated samples as the input file list.
            filePaths = samples;
        }
        else
        {
            // Use the command‑line arguments as the input file list.
            filePaths = args;
        }

        // Process each file path in the list.
        foreach (var path in filePaths)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Open the image with BarCodeReader, requesting all supported barcode types.
            using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the image.
                var results = reader.ReadBarCodes();

                // Output the results to the console.
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {path}");
                }
                else
                {
                    Console.WriteLine($"Barcodes found in file: {path}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}