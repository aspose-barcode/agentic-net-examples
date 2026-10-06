// Title: Batch Barcode Metadata Extraction to CSV
// Description: Demonstrates how to generate sample barcode images, read them in bulk, extract detailed barcode metadata, and export the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader for recognizing them across multiple images. Typical scenarios include inventory scanning, document processing, and analytics where developers need to extract barcode type, text, confidence, and region data in bulk. The code illustrates common patterns for handling image folders, iterating over results, and writing structured output for downstream consumption.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode, batch processing, csv, metadata, aspose.barcode, barcodereader, barcodegenerator, symbology, image recognition

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates sample barcode images, reads them,
/// extracts metadata, and writes the results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs barcode generation,
    /// batch reading, and CSV export.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string imageFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(imageFolder);

        // List to hold generated image file paths
        List<string> imageFiles = new List<string>();

        // Define sample barcodes with their symbology, text, and output file name
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AZTEC", "aztec.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE", "pdf417.png")
        };

        // Generate sample barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(imageFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV output path in a separate temporary folder
        string resultFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodesResult_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(resultFolder);
        string csvPath = Path.Combine(resultFolder, "BarcodeResults.csv");

        // Write CSV header and process each image file
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("FileName,CodeType,CodeText,ReadingQuality,Confidence,RegionX,RegionY,RegionWidth,RegionHeight,Angle");

            // Iterate over generated image files
            foreach (string file in imageFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    // Initialize reader for all supported barcode types
                    using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcode detected; write an empty entry
                            writer.WriteLine($"{Path.GetFileName(file)},,,0,0,0,0,0,0,0");
                            continue;
                        }

                        // Write a CSV line for each detected barcode
                        foreach (var result in results)
                        {
                            var region = result.Region.Rectangle;
                            string line = string.Format(
                                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                Path.GetFileName(file),
                                result.CodeTypeName,
                                result.CodeText?.Replace(",", " "), // escape commas in text
                                result.ReadingQuality,
                                result.Confidence,
                                region.X,
                                region.Y,
                                region.Width,
                                region.Height,
                                result.Region.Angle);
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed; skip file
                    Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // General exception handling
                    Console.WriteLine($"Error processing file {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Barcode processing completed. Results saved to: {csvPath}");
    }
}