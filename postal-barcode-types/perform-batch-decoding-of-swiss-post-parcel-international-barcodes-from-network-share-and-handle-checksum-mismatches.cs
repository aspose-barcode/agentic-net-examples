using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch processing of image files to detect Swiss Post Parcel barcodes
/// using Aspose.BarCode library. The program scans a specified folder (or a default
/// network share) for supported image types, reads up to a limited number of files,
/// and outputs barcode data along with checksum validation information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments. If provided, the first argument is used as the
    /// input folder path; otherwise a default UNC path is used.
    /// </param>
    static void Main(string[] args)
    {
        // Determine the input folder: use first argument if supplied, otherwise a default UNC path.
        string inputFolder = args.Length > 0 ? args[0] : @"\\server\share\Barcodes";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Define the set of image file extensions that will be processed.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };

        // Retrieve all files in the folder (filtering will be applied later).
        string[] files = Directory.GetFiles(inputFolder);

        int processed = 0;                     // Counter for processed files.
        const int maxFiles = 10;               // Upper limit to keep the demo execution short.

        // Iterate over each file found in the directory.
        foreach (string filePath in files)
        {
            // Stop processing once the maximum number of files has been reached.
            if (processed >= maxFiles) break;

            // Check if the file has a supported image extension.
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0) continue;

            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

            // Ensure the file still exists (it could have been removed after the initial listing).
            if (!File.Exists(filePath))
            {
                Console.WriteLine("  File not found, skipping.");
                continue;
            }

            try
            {
                // Initialize the barcode reader for Swiss Post Parcel barcodes.
                using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
                {
                    // Enable checksum validation; barcodes with an invalid checksum will be ignored.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the barcode detection.
                    var results = reader.ReadBarCodes();

                    // If no barcodes were found, inform the user.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("  No Swiss Post Parcel barcode detected.");
                    }
                    else
                    {
                        // Iterate over each detected barcode and display its details.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  CodeText: {result.CodeText}");

                            // Attempt to retrieve the checksum from extended parameters, if available.
                            string checksum = result.Extended?.OneD?.CheckSum;
                            if (!string.IsNullOrEmpty(checksum))
                            {
                                // Simple validation: compare the checksum with the last character of the CodeText.
                                bool matches = result.CodeText.EndsWith(checksum);
                                Console.WriteLine($"  Checksum: {checksum} (match: {matches})");

                                // Warn the user if the checksum does not match.
                                if (!matches)
                                {
                                    Console.WriteLine("  Warning: Checksum mismatch detected.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("  Checksum information not available.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the current file.
                Console.WriteLine($"  Error processing file: {ex.Message}");
            }

            processed++; // Increment the processed file counter.
        }

        Console.WriteLine("Batch processing completed.");
    }
}