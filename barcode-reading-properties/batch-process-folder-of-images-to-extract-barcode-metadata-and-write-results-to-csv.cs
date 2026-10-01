// Title: Batch barcode extraction to CSV
// Description: Demonstrates generating sample barcode images, reading them, and writing extracted metadata to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to detect and read them, and standard .NET I/O to export results. Typical use cases include bulk processing of scanned documents, inventory audits, or data migration where barcode data must be extracted and stored in a structured format such as CSV. Developers often need to combine generation, recognition, and file handling APIs to automate such workflows.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode, batch processing, csv, generation, recognition, aspnet, aspose.barcode, code128, qr, datamatrix

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example that generates sample barcode images, reads them, and writes extracted metadata to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that orchestrates barcode generation, recognition, and CSV export.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the full paths of generated images
        List<string> imageFiles = new List<string>();

        // Generate sample barcode images of different symbologies
        GenerateSampleBarcode(EncodeTypes.Code128, "ABC123", "code128.png", tempFolder, imageFiles);
        GenerateSampleBarcode(EncodeTypes.QR, "https://example.com", "qr.png", tempFolder, imageFiles);
        GenerateSampleBarcode(EncodeTypes.DataMatrix, "DM12345", "datamatrix.png", tempFolder, imageFiles);

        // Prepare CSV output file (outside the image folder)
        string csvPath = Path.Combine(Path.GetTempPath(), "BarcodeResults_" + Guid.NewGuid().ToString("N") + ".csv");

        // Write CSV header
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("FileName,CodeText,Symbology");
        }

        // Process each image file and append results to CSV
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                // Initialize barcode reader for the current image
                using (var reader = new BarCodeReader(imagePath))
                {
                    // Iterate over all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Build CSV line with escaped fields
                        string line = $"{Path.GetFileName(imagePath)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)}";

                        // Append the line to the CSV file
                        using (var writer = new StreamWriter(csvPath, true, Encoding.UTF8))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping file due to load error: {imagePath} ({ex.Message})");
            }
            catch (Exception ex)
            {
                // General safety catch
                Console.WriteLine($"Error processing file {imagePath}: {ex.Message}");
            }
        }

        Console.WriteLine($"Barcode extraction completed. Results saved to: {csvPath}");
    }

    // Generates a barcode image and records its path
    private static void GenerateSampleBarcode(BaseEncodeType encodeType, string codeText, string fileName, string folder, List<string> list)
    {
        string fullPath = Path.Combine(folder, fileName);
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save as PNG
            generator.Save(fullPath, BarCodeImageFormat.Png);
        }
        list.Add(fullPath);
    }

    // Escapes CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";
        return field;
    }
}