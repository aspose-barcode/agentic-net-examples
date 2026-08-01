// Title: Barcode generation, recognition, and logging example
// Description: Demonstrates creating barcode images, reading them, and logging quality metrics to a file.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to generate barcodes, recognize multiple symbologies, and extract reading quality using BarcodeGenerator, BarCodeReader, and related classes. Developers often need to batch‑process barcode images, monitor directories, and log results for quality assurance or analytics, making this pattern useful for automation scripts and services.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode generation, barcode recognition, quality metrics, file monitoring, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating sample barcode images, reading them, and logging quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, reads them, and writes log entries.
    /// </summary>
    static void Main()
    {
        // Define the folder to store and read barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        // Path for the log file
        string logFilePath = Path.Combine(folderPath, "barcode_log.txt");

        // Sample barcode data to generate
        string[] sampleCodes = new string[]
        {
            "CODE128-12345",
            "QR-HELLO",
            "DATAMATRIX-987654321"
        };

        // Generate sample barcode images
        foreach (string code in sampleCodes)
        {
            // Choose symbology based on code prefix
            BaseEncodeType encodeType;
            if (code.StartsWith("CODE128"))
                encodeType = EncodeTypes.Code128;
            else if (code.StartsWith("QR"))
                encodeType = EncodeTypes.QR;
            else
                encodeType = EncodeTypes.DataMatrix;

            string fileName = $"{code}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // Create and save the barcode image as PNG
            using (var generator = new BarcodeGenerator(encodeType, code))
            {
                generator.Save(filePath);
            }
        }

        // Process each PNG file in the folder
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // Initialize the barcode reader for all supported types
            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Prepare log entry with file name, barcode type, text, and quality
                    string logEntry = $"File: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText} | Quality: {result.ReadingQuality}";

                    // Output to console for immediate feedback
                    Console.WriteLine(logEntry);

                    // Append the log entry to the log file
                    try
                    {
                        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to write log: {ex.Message}");
                    }
                }
            }
        }

        // Indicate completion of processing
        Console.WriteLine("Barcode processing completed.");
    }
}