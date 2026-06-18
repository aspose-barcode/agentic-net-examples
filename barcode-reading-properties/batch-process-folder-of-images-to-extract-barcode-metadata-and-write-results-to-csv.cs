using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Scans a folder for image files, reads any barcodes found, and writes the results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional folder path argument; defaults to "Barcodes" if none provided.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the folder to scan. Use a default if none provided.
        string folderPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Prepare CSV output file path.
        string csvPath = Path.Combine(folderPath, "barcode_results.csv");

        // Open a StreamWriter for the CSV file (overwrite if it already exists).
        using (var writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header line.
            writer.WriteLine("FileName,BarcodeType,CodeText,Confidence,ReadingQuality,Angle,RegionX,RegionY,RegionWidth,RegionHeight");

            // Define supported image extensions to search for.
            string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tiff", ".tif" };
            var imageFiles = new List<string>();

            // Collect all image files with the supported extensions in the target folder (non‑recursive).
            foreach (var ext in extensions)
            {
                imageFiles.AddRange(Directory.GetFiles(folderPath, "*" + ext, SearchOption.TopDirectoryOnly));
            }

            // Limit processing to a safe sample size (e.g., first 5 images) to avoid long runs.
            int processedCount = 0;
            const int maxSamples = 5;

            // Iterate over each discovered image file.
            foreach (var imagePath in imageFiles)
            {
                // Stop if the maximum sample count has been reached.
                if (processedCount >= maxSamples)
                    break;

                // Ensure the file still exists (it could have been removed meanwhile).
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                // Read barcodes from the image using all supported barcode types.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Process each barcode detected in the current image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Extract relevant metadata from the barcode result.
                        string fileName = Path.GetFileName(imagePath);
                        string barcodeType = result.CodeTypeName ?? "";
                        string codeText = result.CodeText ?? "";
                        int confidence = (int)result.Confidence;
                        double readingQuality = result.ReadingQuality;
                        double angle = result.Region.Angle;

                        // Extract region rectangle and round values to integers.
                        var rect = result.Region.Rectangle;
                        int regionX = (int)Math.Round((double)rect.X);
                        int regionY = (int)Math.Round((double)rect.Y);
                        int regionWidth = (int)Math.Round((double)rect.Width);
                        int regionHeight = (int)Math.Round((double)rect.Height);

                        // Simple CSV escaping: wrap in quotes and double any internal quotes.
                        string Escape(string s) => $"\"{s.Replace("\"", "\"\"")}\"";

                        // Write a CSV line with the extracted data.
                        writer.WriteLine($"{Escape(fileName)},{Escape(barcodeType)},{Escape(codeText)},{confidence},{readingQuality},{angle},{regionX},{regionY},{regionWidth},{regionHeight}");
                    }
                }

                // Increment the processed image counter.
                processedCount++;
            }
        }

        // Inform the user that processing is complete and where to find the results.
        Console.WriteLine($"Barcode extraction completed. Results saved to: {csvPath}");
    }
}