using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading PDF417 barcodes (including Macro PDF417) from image files in a specified folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for image files, reads PDF417 barcodes,
    /// and outputs segment information for Macro PDF417 barcodes.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains PDF417 barcode images. Adjust the path as needed.
        string imagesFolder = "pdf417_images";

        // Verify that the folder exists; if not, inform the user and exit.
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve all files in the folder (non‑recursive). Filtering by extension will be done later.
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found in the folder.");
            return;
        }

        // Iterate over each file found in the folder.
        foreach (string filePath in imageFiles)
        {
            // Determine the file extension and process only supported image types.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".tif" && ext != ".tiff" && ext != ".bmp")
                continue; // Skip unsupported file types.

            // Double‑check that the file still exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a BarCodeReader configured to decode PDF417 barcodes.
            using (var reader = new BarCodeReader(filePath, DecodeType.Pdf417))
            {
                // Read all barcodes present in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract Macro PDF417 (structured‑append) information.
                    int segmentId = result.Extended.Pdf417.MacroPdf417SegmentID;
                    int segmentsCount = result.Extended.Pdf417.MacroPdf417SegmentsCount;

                    // SegmentID is zero‑based; display as 1‑based for readability.
                    Console.WriteLine($"{Path.GetFileName(filePath)}: Segment {segmentId + 1} of {segmentsCount}");
                }
            }
        }
    }
}