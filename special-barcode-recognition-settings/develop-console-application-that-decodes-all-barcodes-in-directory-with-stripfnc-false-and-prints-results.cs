using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Scans a directory for image files and reads any barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Optional command‑line arguments. The first argument, if present, specifies the directory to scan.</param>
    static void Main(string[] args)
    {
        // Determine the directory to scan: use first argument or fallback to a sample folder.
        string directoryPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Define the set of image file extensions that will be processed.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };

        // Retrieve all files in the target directory.
        string[] files = Directory.GetFiles(directoryPath);
        if (files.Length == 0)
        {
            Console.WriteLine($"No files found in directory: {directoryPath}");
            return;
        }

        // Iterate over each file and attempt barcode detection on supported image types.
        foreach (string filePath in files)
        {
            // Skip files whose extensions are not in the supported list.
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Double‑check that the file still exists (it may have been removed concurrently).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a barcode reader for the current image, configured to detect all supported types.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Ensure that FNC characters are preserved in the decoded text.
                reader.BarcodeSettings.StripFNC = false;

                // Perform the barcode detection.
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were found, report and move to the next file.
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in: {Path.GetFileName(filePath)}");
                    continue;
                }

                // Output the detected barcodes for the current image.
                Console.WriteLine($"Barcodes in {Path.GetFileName(filePath)}:");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}