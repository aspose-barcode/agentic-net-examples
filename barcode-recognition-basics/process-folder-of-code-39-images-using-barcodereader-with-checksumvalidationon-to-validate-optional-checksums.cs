// Title: Process Code 39 Images with Checksum Validation Using BarCodeReader
// Description: Demonstrates generating Code 39 barcode images (with and without checksum) and reading them back while validating optional checksums.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create 1D barcodes and BarCodeReader with ChecksumValidation.On to verify checksum integrity. Typical use cases include batch processing of barcode images, quality assurance of scanned data, and automated validation pipelines. Developers often need to combine these APIs to ensure data accuracy when working with Code 39 and other symbologies.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, checksum, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Sample program that creates Code 39 barcode images (with and without checksum)
/// and then reads them back using <see cref="BarCodeReader"/> with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode files, reads them with checksum validation, and outputs the results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder to store generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Prepare a list that will hold the full paths of the generated files.
        // --------------------------------------------------------------------
        List<string> files = new List<string>();

        // --------------------------------------------------------------------
        // 3. Define sample data: one barcode with checksum enabled, one without.
        // --------------------------------------------------------------------
        var samples = new[]
        {
            new { FileName = "Code39_WithChecksum.png", CodeText = "ABC123", EnableChecksum = EnableChecksum.Yes },
            new { FileName = "Code39_WithoutChecksum.png", CodeText = "XYZ789", EnableChecksum = EnableChecksum.No }
        };

        // --------------------------------------------------------------------
        // 4. Generate barcode images using BarcodeGenerator.
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, sample.CodeText))
            {
                // Set image resolution (pixel size of the smallest bar).
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Enable or disable checksum according to the sample definition.
                generator.Parameters.Barcode.IsChecksumEnabled = sample.EnableChecksum;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 5. Read each image with checksum validation turned on.
        // --------------------------------------------------------------------
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialise the reader for Code 39 symbology.
                using (var reader = new BarCodeReader(file, DecodeType.Code39))
                {
                    // Enable checksum validation – the reader will reject barcodes with invalid checksums.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the read operation.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                    }
                    else
                    {
                        // Output details for each detected barcode.
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)}");
                            Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                            Console.WriteLine($"  1D Value: {result.Extended.OneD.Value}");
                            Console.WriteLine($"  1D CheckSum: {result.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be loaded (e.g., unsupported format).
                Console.WriteLine($"Failed to load image {Path.GetFileName(file)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch‑all for any other processing errors.
                Console.WriteLine($"Error processing {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // 6. Clean up the temporary folder (optional).
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors that occur during cleanup (e.g., files in use).
        }
    }
}