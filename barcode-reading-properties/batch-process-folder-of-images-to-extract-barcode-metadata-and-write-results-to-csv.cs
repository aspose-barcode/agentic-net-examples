// Title: Batch barcode image processing and CSV export
// Description: Demonstrates generating sample barcode images, scanning a folder for multiple barcode symbologies, and writing the extracted metadata (type, text, confidence, reading quality) to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for image creation and BarCodeReader for multi‑symbology recognition. Typical scenarios include inventory automation, document scanning, and bulk data extraction where developers need to read many images and store results in a structured format such as CSV. The key API classes are BarcodeGenerator, BarCodeReader, and BarCodeResult.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode, batch processing, csv, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation, recognition, and CSV export of barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them, and writes results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the paths of generated barcode images
        List<string> imageFiles = new List<string>();

        // Generate sample barcode images of different symbologies
        GenerateBarcodeImage(EncodeTypes.Code128, "ABC123", Path.Combine(tempFolder, "code128.png"));
        GenerateBarcodeImage(EncodeTypes.QR, "https://example.com", Path.Combine(tempFolder, "qr.png"));
        GenerateBarcodeImage(EncodeTypes.DataMatrix, "DM12345", Path.Combine(tempFolder, "datamatrix.png"));

        // Populate the list with the created file paths
        imageFiles.Add(Path.Combine(tempFolder, "code128.png"));
        imageFiles.Add(Path.Combine(tempFolder, "qr.png"));
        imageFiles.Add(Path.Combine(tempFolder, "datamatrix.png"));

        // Prepare CSV output file
        string csvPath = Path.Combine(tempFolder, "results.csv");
        using (var writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeType,CodeText,Confidence,ReadingQuality");

            // Process each image file
            foreach (string filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Initialize BarCodeReader for the required symbologies
                    using (var reader = new BarCodeReader(
                        filePath,
                        DecodeType.Code128,
                        DecodeType.QR,
                        DecodeType.DataMatrix,
                        DecodeType.Pdf417,
                        DecodeType.RM4SCC))
                    {
                        // Iterate over all detected barcodes in the image
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Build a CSV line with escaped values
                            string line = $"{Path.GetFileName(filePath)},{EscapeCsv(result.CodeTypeName)},{EscapeCsv(result.CodeText)},{result.Confidence},{result.ReadingQuality}";
                            writer.WriteLine(line);
                            Console.WriteLine(line);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handle image loading failures or unsupported formats
                    Console.WriteLine($"Skipping file due to error: {filePath}. Message: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"CSV results written to: {csvPath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified encode type and text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to encode.</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <param name="outputPath">File path where the image will be saved.</param>
    static void GenerateBarcodeImage(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use default settings; image size adapts to content
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Escapes a string for safe inclusion in a CSV file.
    /// </summary>
    /// <param name="input">The raw string value.</param>
    /// <returns>Escaped CSV string.</returns>
    static string EscapeCsv(string input)
    {
        if (input == null) return "";
        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}