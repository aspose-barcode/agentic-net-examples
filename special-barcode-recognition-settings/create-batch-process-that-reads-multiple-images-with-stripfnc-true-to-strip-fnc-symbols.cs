// Title: Batch barcode image processing with StripFNC enabled
// Description: Demonstrates how to read multiple barcode images in a folder while stripping FNC symbols from the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader to decode various symbologies. It highlights the StripFNC setting, which removes Function Code (FNC) characters from the result—useful when clean data is required. Developers working with bulk barcode scanning, image preprocessing, or data sanitization will find this pattern common.
// Prompt: Create a batch process that reads multiple images with StripFNC true to strip FNC symbols.
// Tags: barcode symbology, strip fnc, text output, barcodereader, barcoderesult

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple batch processor that scans a folder of images,
/// decodes any barcodes found, and strips Function Code (FNC) symbols
/// from the resulting text using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over image files in the
    /// specified folder, decodes barcodes with StripFNC enabled, and
    /// writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Folder containing barcode images
        string imagesFolder = "Images";

        // Verify that the folder exists before proceeding
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve all files in the folder (any extension) – filtering will be applied later
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
        int processed = 0;
        const int maxFiles = 10; // Limit processing to a safe sample size

        // Process each file until the maximum count is reached
        foreach (string filePath in imageFiles)
        {
            if (processed >= maxFiles) break;

            // Ensure the file still exists (it could have been removed externally)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Accept only common image formats
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp")
            {
                Console.WriteLine($"Unsupported file type: {filePath}");
                continue;
            }

            // Initialize the barcode reader for the current image
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Enable stripping of FNC symbols from decoded text
                reader.BarcodeSettings.StripFNC = true;

                // Perform the recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the results
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in: {Path.GetFileName(filePath)}");
                }
                else
                {
                    Console.WriteLine($"Barcodes in {Path.GetFileName(filePath)} (FNC stripped):");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }

            processed++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}