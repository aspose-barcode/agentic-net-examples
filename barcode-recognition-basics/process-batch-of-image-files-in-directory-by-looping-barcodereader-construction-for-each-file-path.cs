// Title: Batch barcode generation and reading example
// Description: Demonstrates generating multiple barcode images, storing them in a temporary folder, and reading each file with BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to use BarcodeGenerator to create barcodes and BarCodeReader to decode them from files. Typical use cases include batch processing of scanned documents, automated inventory checks, and bulk verification of barcode data. Developers often need to loop over file paths, handle various symbologies, and manage temporary resources.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: barcode generation, barcode reading, batch processing, csharp, aspose.barcode, code128, qr, datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch creation of barcode images and subsequent reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a list to hold the generated barcode image file paths
        List<string> barcodeFiles = new List<string>();

        // Sample data for barcode generation (different symbologies)
        var samples = new[]
        {
            new { Encode = EncodeTypes.Code128, Text = "Sample001" },
            new { Encode = EncodeTypes.QR, Text = "https://example.com" },
            new { Encode = EncodeTypes.DataMatrix, Text = "DM12345" }
        };

        // Generate barcode images into the temporary folder
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{sample.Encode}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Process each barcode image file using BarCodeReader
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize reader for the current file, supporting all barcode types
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}