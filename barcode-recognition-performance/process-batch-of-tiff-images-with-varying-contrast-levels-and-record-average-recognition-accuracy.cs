using System;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch barcode recognition on TIFF images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for TIFF files,
    /// reads barcodes from each image, and reports confidence statistics.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains the TIFF images (adjust path as needed)
        string imagesFolder = "Images";

        // Verify that the folder exists before proceeding
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve all .tif files from the specified folder
        string[] tiffFiles = Directory.GetFiles(imagesFolder, "*.tif");

        // If no TIFF files are found, inform the user and exit
        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF files found.");
            return;
        }

        double totalConfidence = 0; // Accumulates numeric confidence values
        int totalBarcodes = 0;      // Counts total barcodes processed

        // Process each TIFF file individually
        foreach (string filePath in tiffFiles)
        {
            // Ensure the file still exists (it might have been removed meanwhile)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File missing: {filePath}");
                continue;
            }

            // Load the image and create a barcode reader for all supported types
            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Apply high‑quality settings to improve recognition on low‑contrast images
                reader.QualitySettings = QualitySettings.HighQuality;

                // Attempt to read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, report and move to the next file
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(filePath)}");
                    continue;
                }

                // Report the number of barcodes found in the current image
                Console.WriteLine($"Processing {Path.GetFileName(filePath)} - {results.Length} barcode(s) found");

                // Iterate through each detected barcode
                foreach (var result in results)
                {
                    // Convert the enum confidence to a numeric value for aggregation
                    int numericConfidence = ConfidenceToNumeric(result.Confidence);
                    totalConfidence += numericConfidence;
                    totalBarcodes++;

                    // Output details of the barcode
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {numericConfidence}");
                }
            }
        }

        // After processing all files, calculate and display the average confidence
        if (totalBarcodes > 0)
        {
            double averageConfidence = totalConfidence / totalBarcodes;
            Console.WriteLine($"\nAverage recognition confidence across all barcodes: {averageConfidence:F2}");
        }
        else
        {
            Console.WriteLine("\nNo barcodes were recognized in the batch.");
        }
    }

    /// <summary>
    /// Converts a <see cref="BarCodeConfidence"/> value to a numeric representation.
    /// </summary>
    /// <param name="confidence">The confidence enum value returned by the reader.</param>
    /// <returns>Numeric confidence (Strong=100, Moderate=80, None=0).</returns>
    static int ConfidenceToNumeric(BarCodeConfidence confidence)
    {
        if (confidence == BarCodeConfidence.Strong) return 100;
        if (confidence == BarCodeConfidence.Moderate) return 80;
        return 0; // BarCodeConfidence.None
    }
}