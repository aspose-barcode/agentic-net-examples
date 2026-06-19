using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeProcessing
{
    /// <summary>
    /// Simple data holder for barcode information extracted from an image.
    /// </summary>
    class BarcodeInfo
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public int Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public double Angle { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode processing console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Scans image files in a folder, extracts barcode data, and writes the results to a CSV file.
        /// </summary>
        static void Main()
        {
            // Folder containing image files to process (adjust as needed)
            string imagesFolder = "Images";

            // Output CSV file (simulating database storage)
            string outputCsv = "barcode_results.csv";

            // Define supported image extensions
            string[] supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
            var imageFiles = new List<string>();

            // Verify the images folder exists and collect matching files
            if (Directory.Exists(imagesFolder))
            {
                foreach (var ext in supportedExtensions)
                {
                    // Add files with the current extension (non‑recursive)
                    imageFiles.AddRange(Directory.GetFiles(imagesFolder, "*" + ext, SearchOption.TopDirectoryOnly));
                }
            }
            else
            {
                Console.WriteLine($"Folder not found: {imagesFolder}");
                return;
            }

            var results = new List<BarcodeInfo>();

            // Process each image file
            foreach (var filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Load the image and create a barcode reader for all supported types
                using (var bitmap = new Bitmap(filePath))
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Iterate over all detected barcodes in the current image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Populate a BarcodeInfo instance with the detection details
                        var info = new BarcodeInfo
                        {
                            FileName = Path.GetFileName(filePath),
                            CodeType = result.CodeTypeName,
                            CodeText = result.CodeText,
                            Confidence = (int)result.Confidence,
                            ReadingQuality = result.ReadingQuality,
                            Angle = result.Region.Angle
                        };

                        results.Add(info);
                        Console.WriteLine($"Detected {info.CodeType} in {info.FileName}: {info.CodeText}");
                    }
                }
            }

            // Write the collected results to a CSV file (placeholder for real DB storage)
            using (var writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("FileName,CodeType,CodeText,Confidence,ReadingQuality,Angle");

                // Write each result as a CSV line, escaping fields as needed
                foreach (var r in results)
                {
                    string line = $"{Escape(r.FileName)},{Escape(r.CodeType)},{Escape(r.CodeText)},{r.Confidence},{r.ReadingQuality},{r.Angle}";
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Processing complete. Results written to {outputCsv}");

            // NOTE: In a real application you would insert the collected data into a database table
            // using an appropriate data access library (e.g., System.Data.SqlClient, Microsoft.Data.Sqlite, etc.).
        }

        /// <summary>
        /// Escapes a CSV field by surrounding it with quotes and doubling internal quotes if necessary.
        /// </summary>
        /// <param name="field">The field value to escape.</param>
        /// <returns>The escaped field suitable for CSV output.</returns>
        static string Escape(string field)
        {
            // If the field contains commas, quotes, or newlines, it must be quoted
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                // Double any existing quotes
                field = field.Replace("\"", "\"\"");
                // Surround the field with quotes
                return $"\"{field}\"";
            }

            // No escaping needed
            return field;
        }
    }
}