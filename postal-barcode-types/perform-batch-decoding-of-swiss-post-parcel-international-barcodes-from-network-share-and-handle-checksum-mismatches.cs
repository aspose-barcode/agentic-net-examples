// Title: Batch decode Swiss Post Parcel barcodes from network share
// Description: Demonstrates how to read Swiss Post Parcel international barcodes in a batch from a shared folder, validating checksums and handling mismatches.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.SwissPostParcel, checksum validation, and file system iteration. Developers working with postal barcode processing, bulk image handling, or network shares can reference this pattern for extracting barcode data and detecting errors.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, barcode, batch, decoding, checksum, network share, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that batch processes image files from a network share,
/// decodes Swiss Post Parcel barcodes, and validates their checksums.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a folder for supported image files, reads Swiss Post Parcel barcodes,
    /// and outputs barcode details including checksum information.
    /// </summary>
    /// <param name="args">Optional first argument specifying the input folder path.</param>
    static void Main(string[] args)
    {
        // Determine the input folder: use first argument or default network share path.
        string inputFolder = args.Length > 0 ? args[0] : @"\\localhost\share\barcodes";

        // Verify that the folder exists before proceeding.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Define supported image extensions for barcode scanning.
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".tif", ".tiff", ".bmp" };
        string[] files = Directory.GetFiles(inputFolder);
        int processed = 0;
        const int maxFiles = 10; // Safety cap to limit batch size.

        // Iterate over each file in the folder.
        foreach (string filePath in files)
        {
            // Stop processing once the maximum file count is reached.
            if (processed >= maxFiles) break;

            // Skip files with unsupported extensions.
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0) continue;

            // Ensure the file still exists (it might have been removed).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for Swiss Post Parcel symbology.
                using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
                {
                    // Enable checksum validation to detect mismatches.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    bool anyFound = false;

                    // Read all barcodes found in the current image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");

                        // Attempt to retrieve checksum information if available.
                        try
                        {
                            string checksum = result.Extended.OneD.CheckSum;
                            if (!string.IsNullOrEmpty(checksum))
                            {
                                Console.WriteLine($"  CheckSum: {checksum}");
                            }
                        }
                        catch
                        {
                            // Extended.OneD may not be applicable for this symbology; ignore silently.
                        }
                    }

                    // Inform the user if no barcode was detected in the file.
                    if (!anyFound)
                    {
                        Console.WriteLine($"No Swiss Post Parcel barcode detected in file: {Path.GetFileName(filePath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the file.
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }

            processed++;
        }

        Console.WriteLine("Batch decoding completed.");
    }
}