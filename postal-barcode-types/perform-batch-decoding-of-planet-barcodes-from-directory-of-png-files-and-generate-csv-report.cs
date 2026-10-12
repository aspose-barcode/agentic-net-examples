// Title: Batch decode Planet barcodes and export results to CSV
// Description: Demonstrates generating Planet barcode images, decoding them in bulk, and writing a CSV report with the decoded data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for batch decoding, and standard .NET I/O for reporting. Developers often need to process multiple barcode images automatically, extract their contents, and store results in a structured format such as CSV for further analysis or integration.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, batch decoding, csv, report, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and decoding of Planet barcodes, producing a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Planet barcode images, decodes them, and writes results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string imagesFolder = Path.Combine(Path.GetTempPath(), "BatchPlanet_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(imagesFolder);

        // Sample Planet barcode texts
        List<string> sampleTexts = new List<string>
        {
            "123456",
            "987654321",
            "5555555555",
            "0000012345",
            "9999999999"
        };

        // Generate PNG files for each sample text
        List<string> imageFiles = new List<string>();
        foreach (string text in sampleTexts)
        {
            string filePath = Path.Combine(imagesFolder, $"Planet_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, text))
            {
                // Set barcode module size
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV report file (separate from images folder)
        string reportPath = Path.Combine(Path.GetTempPath(), "PlanetBatchReport_" + Guid.NewGuid().ToString("N") + ".csv");
        using (StreamWriter writer = new StreamWriter(reportPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeText,CodeTypeName,ReadingQuality");

            // Decode each image and write results
            foreach (string imageFile in imageFiles)
            {
                if (!File.Exists(imageFile))
                {
                    Console.WriteLine($"File not found: {imageFile}");
                    continue;
                }

                try
                {
                    BaseDecodeType decodeType = DecodeType.Planet;
                    using (BarCodeReader reader = new BarCodeReader(imageFile, decodeType))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        if (results.Length == 0)
                        {
                            // No barcode detected – write empty fields
                            writer.WriteLine($"{Path.GetFileName(imageFile)},,,");
                            Console.WriteLine($"No barcode detected in {Path.GetFileName(imageFile)}");
                        }
                        else
                        {
                            // Write each detected barcode to the CSV
                            foreach (BarCodeResult result in results)
                            {
                                string line = $"{Path.GetFileName(imageFile)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)},{result.ReadingQuality}";
                                writer.WriteLine(line);
                                Console.WriteLine($"Decoded {Path.GetFileName(imageFile)}: {result.CodeText} ({result.CodeTypeName})");
                            }
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed or unsupported format
                    Console.WriteLine($"Error processing {Path.GetFileName(imageFile)}: {ex.Message}");
                    writer.WriteLine($"{Path.GetFileName(imageFile)},Error loading image,,");
                }
            }
        }

        Console.WriteLine($"CSV report generated at: {reportPath}");
    }

    /// <summary>
    /// Escapes a CSV field by surrounding it with quotes if it contains commas, quotes, or newlines.
    /// </summary>
    /// <param name="field">The field value to escape.</param>
    /// <returns>The escaped field.</returns>
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}