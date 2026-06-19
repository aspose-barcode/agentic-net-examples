using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading Code 39 barcodes from image files in a specified folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for image files,
    /// reads Code 39 barcodes, and outputs the decoded text and checksum status.
    /// </summary>
    static void Main()
    {
        // Folder containing Code 39 barcode images
        string folderPath = "Code39Images";

        // Verify that the folder exists before proceeding
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all files in the folder (filter later by image extensions)
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        int processed = 0;
        const int maxSamples = 10; // safe sample size for the runner

        // Iterate through each file found in the directory
        foreach (string filePath in files)
        {
            // Determine the file extension and skip non‑image files
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                continue; // skip non‑image files

            // Double‑check that the file still exists (in case of race conditions)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Load the image and create a barcode reader configured for Code 39
            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader(bitmap, DecodeType.Code39))
            {
                // Enable checksum validation for optional checksums
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Attempt to read any barcodes present in the image
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user
                if (results.Length == 0)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: No barcode detected.");
                }
                else
                {
                    // Output each detected barcode's text and checksum validity
                    foreach (var result in results)
                    {
                        // For Code 39 the checksum validity is available in OneD.CheckSum
                        Console.WriteLine($"{Path.GetFileName(filePath)}: CodeText=\"{result.CodeText}\", CheckSumValid={result.Extended.OneD.CheckSum}");
                    }
                }
            }

            // Increment the processed count and stop if the maximum sample size is reached
            processed++;
            if (processed >= maxSamples)
                break; // limit processing to a safe number of items
        }

        Console.WriteLine("Processing completed.");
    }
}