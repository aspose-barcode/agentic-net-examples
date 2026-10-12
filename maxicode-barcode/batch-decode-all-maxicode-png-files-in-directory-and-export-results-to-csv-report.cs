// Title: Batch decode MaxiCode PNG files and export results to CSV
// Description: Demonstrates generating sample MaxiCode barcodes, decoding them from PNG images, and writing the decoded text to a CSV report.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating MaxiCode symbols, BarCodeReader for batch decoding, and standard .NET I/O for exporting results. Developers often need to process multiple barcode images automatically and produce structured reports, such as CSV files, for downstream analysis or integration.
// Prompt: Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report.
// Tags: maxicode, batch, decode, csv, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates sample MaxiCode PNG files, decodes them, and writes a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them, and produces a CSV summary.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample MaxiCode PNG files and collect their paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"maxicode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, $"Sample{i}"))
            {
                // Set image resolution (pixels per module)
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare the CSV report file
        string csvPath = Path.Combine(tempFolder, "MaxiCodeReport.csv");
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeText");

            // Iterate over each generated barcode image
            foreach (string file in barcodeFiles)
            {
                try
                {
                    // Initialize a reader for MaxiCode symbology
                    using (BarCodeReader reader = new BarCodeReader(file, DecodeType.MaxiCode))
                    {
                        // Read all barcodes found in the image
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            // Write file name and decoded text as a CSV line
                            string line = $"{Path.GetFileName(file)},{result.CodeText}";
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Log files that cannot be processed (e.g., unsupported format)
                    Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        // Inform the user where the report was generated
        Console.WriteLine($"CSV report generated at: {csvPath}");
    }
}