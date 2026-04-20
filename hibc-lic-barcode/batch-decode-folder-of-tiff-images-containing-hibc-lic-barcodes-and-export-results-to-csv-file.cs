using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

namespace BatchDecodeHIBC
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define input folder (relative to the executable) and output CSV file
            string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputTiff");
            string outputCsv = Path.Combine(Directory.GetCurrentDirectory(), "HIBC_DecodeResults.csv");

            // Ensure the input folder exists
            if (!Directory.Exists(inputFolder))
            {
                Directory.CreateDirectory(inputFolder);
                Console.WriteLine($"Input folder created at: {inputFolder}");
                Console.WriteLine("Place TIFF images containing HIBC LIC barcodes into this folder and rerun the program.");
                return;
            }

            // Get TIFF files (limit to first 10 for safety)
            string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
            if (tiffFiles.Length == 0)
            {
                Console.WriteLine("No TIFF files found in the input folder.");
                return;
            }

            // Prepare list to hold CSV rows
            List<string> csvLines = new List<string>();
            csvLines.Add("FileName,BarcodeType,CodeText"); // Header

            // Process each TIFF file
            int processedCount = 0;
            foreach (string filePath in tiffFiles)
            {
                if (processedCount >= 10) break; // safety limit
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found (skipped): {filePath}");
                    continue;
                }

                try
                {
                    // Initialize BarCodeReader for all supported types
                    using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes in the image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcode found – record empty entry
                            string line = $"{Path.GetFileName(filePath)},,";
                            csvLines.Add(line);
                        }
                        else
                        {
                            // Record each detected barcode
                            foreach (BarCodeResult result in results)
                            {
                                // Escape commas and quotes in code text
                                string escapedCodeText = result.CodeText?.Replace("\"", "\"\"") ?? string.Empty;
                                if (escapedCodeText.Contains(",") || escapedCodeText.Contains("\""))
                                {
                                    escapedCodeText = $"\"{escapedCodeText}\"";
                                }

                                string line = $"{Path.GetFileName(filePath)},{result.CodeTypeName},{escapedCodeText}";
                                csvLines.Add(line);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Record error information in CSV
                    string errorInfo = $"Error: {ex.Message}".Replace("\"", "\"\"");
                    string line = $"{Path.GetFileName(filePath)},Error,\"{errorInfo}\"";
                    csvLines.Add(line);
                }

                processedCount++;
            }

            // Write all collected lines to the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                foreach (string line in csvLines)
                {
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Decoding completed. Results saved to: {outputCsv}");
        }
    }
}