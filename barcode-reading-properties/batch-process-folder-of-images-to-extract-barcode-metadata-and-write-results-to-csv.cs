// Title: Batch barcode extraction to CSV
// Description: Demonstrates generating sample barcode images, reading them, and exporting metadata to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes to create, recognize, and export barcode information. Typical use cases include bulk scanning of images, inventory automation, and data collection where developers need to extract barcode details and store them in structured formats like CSV.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode, batch processing, csv, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation, recognition, and CSV export of various barcode types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them, and writes metadata to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch processing
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold paths of generated barcode images
        var imageFiles = new List<string>();

        // -------------------- Generate sample barcode images --------------------

        // Sample 1: Code128
        string file1 = Path.Combine(batchFolder, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(file1, BarCodeImageFormat.Png);
        }
        imageFiles.Add(file1);

        // Sample 2: QR
        string file2 = Path.Combine(batchFolder, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(file2, BarCodeImageFormat.Png);
        }
        imageFiles.Add(file2);

        // Sample 3: DataMatrix
        string file3 = Path.Combine(batchFolder, "datamatrix.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(file3, BarCodeImageFormat.Png);
        }
        imageFiles.Add(file3);

        // Sample 4: Aztec
        string file4 = Path.Combine(batchFolder, "aztec.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "AztecTest"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(file4, BarCodeImageFormat.Png);
        }
        imageFiles.Add(file4);

        // Sample 5: PDF417
        string file5 = Path.Combine(batchFolder, "pdf417.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "PDF417Test"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(file5, BarCodeImageFormat.Png);
        }
        imageFiles.Add(file5);

        // -------------------- Prepare CSV output --------------------
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,CodeType,CodeText,Confidence,ReadingQuality,Angle");

        // -------------------- Process each image file --------------------
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Warning: File not found - {imagePath}");
                continue;
            }

            try
            {
                // Initialize reader for all supported barcode types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        string fileName = Path.GetFileName(imagePath);
                        string codeType = result.CodeTypeName ?? "";
                        string codeText = result.CodeText ?? "";
                        string confidence = result.Confidence.ToString();
                        string readingQuality = result.ReadingQuality.ToString();
                        string angle = result.Region.Angle.ToString();

                        // Escape commas in text fields to preserve CSV format
                        codeText = codeText.Replace(",", " ");

                        // Append a line with the extracted metadata
                        csvBuilder.AppendLine($"{fileName},{codeType},{codeText},{confidence},{readingQuality},{angle}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Warning: Unable to load image '{imagePath}'. {ex.Message}");
            }
        }

        // -------------------- Write CSV file --------------------
        string csvPath = Path.Combine(batchFolder, "BarcodeResults.csv");
        File.WriteAllText(csvPath, csvBuilder.ToString());

        Console.WriteLine($"Batch processing completed. Results saved to: {csvPath}");
    }
}