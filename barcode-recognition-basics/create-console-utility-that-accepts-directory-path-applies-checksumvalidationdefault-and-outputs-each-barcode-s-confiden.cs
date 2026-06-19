using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Scans a directory for image files and attempts to read any barcodes present using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional directory path argument; if omitted, uses the current working directory.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the directory to scan.</param>
    static void Main(string[] args)
    {
        // Determine the directory to scan: use the first argument or fallback to the current directory.
        string directoryPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Verify that the specified directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Define the set of image file extensions that will be considered for barcode scanning.
        string[] supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tiff", ".tif", ".gif" };

        // Retrieve all files in the target directory (non‑recursive).
        string[] files = Directory.GetFiles(directoryPath);

        // Iterate over each file and process only those with supported image extensions.
        foreach (string filePath in files)
        {
            // Skip files whose extensions are not in the supported list.
            if (Array.IndexOf(supportedExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Ensure the file still exists (it could have been removed after the initial enumeration).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a barcode reader for the current image, configured to detect all supported barcode types.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Use the default checksum validation setting.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                bool anyFound = false; // Tracks whether any barcode was detected in the current file.

                // Read all barcodes present in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"{Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Confidence: {result.Confidence}");
                }

                // If no barcodes were found, output a corresponding message.
                if (!anyFound)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)} | No barcode detected.");
                }
            }
        }
    }
}