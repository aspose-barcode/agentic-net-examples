// Title: Batch barcode extraction from images to CSV
// Description: Demonstrates how to scan a folder of image files, read all supported barcodes, and write their metadata to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating the use of BarCodeReader for bulk barcode recognition, BarcodeGenerator for creating sample images, and standard .NET I/O for result export. Developers often need to automate barcode scanning across multiple files and store results in a structured format such as CSV for reporting or downstream processing.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode symbology, batch processing, csv output, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of image files to extract barcode metadata and export results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, scans each image, and writes detection details to a CSV file.
    /// </summary>
    static void Main()
    {
        // Define working directories and CSV output path
        string baseDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_results.csv");

        // Ensure the barcode folder exists
        if (!Directory.Exists(baseDir))
        {
            Directory.CreateDirectory(baseDir);
        }

        // Remove any existing CSV file to start fresh
        if (File.Exists(csvPath))
        {
            File.Delete(csvPath);
        }

        // Generate a few sample barcode images (self‑contained example)
        GenerateSampleBarcodes(baseDir);

        // Write CSV header line
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("FileName,CodeType,CodeText,RegionX,RegionY,RegionWidth,RegionHeight");
        }

        // Define file patterns to search for supported image types
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };

        // Iterate over each pattern and process matching files
        foreach (string pattern in patterns)
        {
            foreach (string filePath in Directory.GetFiles(baseDir, pattern))
            {
                // Verify the file still exists before processing
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Use BarCodeReader to detect all supported barcode types in the image
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Extract the bounding rectangle of the detected barcode region
                        var rect = result.Region.Rectangle;

                        // Build a CSV line with escaped text fields
                        string line = string.Format(
                            "{0},{1},{2},{3},{4},{5},{6}",
                            Path.GetFileName(filePath),
                            result.CodeType,
                            EscapeCsv(result.CodeText),
                            rect.X,
                            rect.Y,
                            rect.Width,
                            rect.Height);

                        // Append the line to the CSV file
                        File.AppendAllText(csvPath, line + Environment.NewLine, Encoding.UTF8);
                    }
                }
            }
        }

        Console.WriteLine($"Barcode extraction completed. Results saved to: {csvPath}");
    }

    // Generates a small set of sample barcode images for demonstration purposes
    private static void GenerateSampleBarcodes(string folder)
    {
        // Sample data for different symbologies
        var samples = new (BaseEncodeType type, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecCode", "aztec.png")
        };

        // Create each barcode image and save it as PNG
        foreach (var (type, text, fileName) in samples)
        {
            string filePath = Path.Combine(folder, fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(type, text))
            {
                // Optional: set common visual parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.FilledBars = true;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }

    // Escapes CSV fields that may contain commas, quotes, or line breaks
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return field;
    }
}