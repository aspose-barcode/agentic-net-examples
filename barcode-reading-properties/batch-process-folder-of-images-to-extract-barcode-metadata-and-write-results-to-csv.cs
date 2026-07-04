// Title: Batch Barcode Extraction to CSV
// Description: Processes all images in a folder, reads any barcodes present, and writes their metadata to a CSV file.
// Prompt: Batch process a folder of images to extract barcode metadata and write results to CSV.
// Tags: barcode, extraction, csv, batch, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to batch‑process a directory of images, extract barcode information,
/// and export the results to a CSV file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional folder path argument; otherwise defaults to a folder named "Images".
    /// Scans supported image files, reads any barcodes, and writes details to a CSV file.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the folder to process.</param>
    static void Main(string[] args)
    {
        // Determine the folder to process. Use argument if provided, otherwise default to "Images".
        string folderPath = args.Length > 0 ? args[0] : "Images";

        // Verify that the target folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Prepare CSV output file path inside the target folder.
        string csvPath = Path.Combine(folderPath, "barcode_results.csv");

        // Open a StreamWriter for the CSV file (UTF‑8 encoding, overwrite if exists).
        using (var csvWriter = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            // Write CSV header line.
            csvWriter.WriteLine("FileName,CodeType,CodeText,Confidence,ReadingQuality,RegionX,RegionY,RegionWidth,RegionHeight");

            // Define supported image file extensions.
            string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };

            // Retrieve all files in the folder (filtering later by extension).
            var imageFiles = Directory.GetFiles(folderPath);

            // Iterate over each file in the directory.
            foreach (var file in imageFiles)
            {
                // Skip files that do not have a supported image extension.
                if (Array.IndexOf(extensions, Path.GetExtension(file).ToLowerInvariant()) < 0)
                    continue;

                // Ensure the file still exists before processing.
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found (skipped): {file}");
                    continue;
                }

                // Load the image using Aspose.Drawing.Bitmap.
                using (var bitmap = new Bitmap(file))
                {
                    // Initialize a barcode reader that attempts to decode all supported types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the current image.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Extract the bounding rectangle of the detected barcode region.
                            var rect = result.Region.Rectangle;

                            // Build a CSV line with the required fields.
                            var line = new StringBuilder();
                            line.Append(Path.GetFileName(file));
                            line.Append(',');
                            line.Append(result.CodeTypeName);
                            line.Append(',');
                            // Replace commas in the code text to avoid CSV column misalignment.
                            line.Append(result.CodeText?.Replace(",", " "));
                            line.Append(',');
                            line.Append(result.Confidence);
                            line.Append(',');
                            line.Append(result.ReadingQuality);
                            line.Append(',');
                            line.Append(rect.X);
                            line.Append(',');
                            line.Append(rect.Y);
                            line.Append(',');
                            line.Append(rect.Width);
                            line.Append(',');
                            line.Append(rect.Height);

                            // Write the constructed line to the CSV file.
                            csvWriter.WriteLine(line.ToString());
                        }
                    }
                }
            }
        }

        // Inform the user that processing is complete and provide the CSV location.
        Console.WriteLine($"Barcode extraction completed. Results saved to: {csvPath}");
    }
}