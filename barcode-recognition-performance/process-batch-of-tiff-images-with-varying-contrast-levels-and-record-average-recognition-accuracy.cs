// Title: Batch TIFF Barcode Recognition with Quality Assessment
// Description: Demonstrates processing multiple TIFF images, detecting barcodes of any symbology, and calculating average reading quality per image and overall.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: barcode, tiff, batch processing, quality, aspose.barcode, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that processes a batch of TIFF images, detects barcodes of any supported symbology,
/// and records the average reading quality per image and overall.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for TIFF files, reads barcodes using Aspose.BarCode,
    /// and reports average reading quality metrics.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains the TIFF images (adjust the path as needed)
        string imagesFolder = "Images";

        // Verify that the specified folder exists
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve up to 5 TIFF files from the folder for safe sample processing
        string[] tiffFiles = Directory.GetFiles(imagesFolder, "*.tif")
                                      .Take(5)
                                      .ToArray();

        // Ensure that at least one TIFF file was found
        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF files found in the specified folder.");
            return;
        }

        // List to store the average reading quality for each processed image
        var perImageAverages = new List<double>();

        // Process each TIFF file individually
        foreach (string filePath in tiffFiles)
        {
            // Skip the file if it cannot be found (defensive check)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the barcode reader for the current file, allowing detection of any supported symbology
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply high‑quality settings to improve detection on low‑contrast images
                reader.QualitySettings = QualitySettings.HighQuality;

                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // If no barcodes were detected, report and move to the next image
                if (results.Length == 0)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: No barcodes detected.");
                    continue;
                }

                // Accumulate the reading quality values from all detected barcodes
                double sumQuality = 0;
                foreach (var result in results)
                {
                    // ReadingQuality is a double representing detection quality (0‑100)
                    sumQuality += result.ReadingQuality;
                }

                // Compute the average reading quality for the current image
                double avgQuality = sumQuality / results.Length;
                perImageAverages.Add(avgQuality);

                // Output per‑image statistics
                Console.WriteLine($"{Path.GetFileName(filePath)}: Detected {results.Length} barcode(s), Average ReadingQuality = {avgQuality:F2}");
            }
        }

        // After processing all images, calculate and display the overall average reading quality
        if (perImageAverages.Count > 0)
        {
            double overallAverage = perImageAverages.Average();
            Console.WriteLine($"Overall average ReadingQuality across processed images: {overallAverage:F2}");
        }
        else
        {
            Console.WriteLine("No barcode data collected to compute overall average.");
        }
    }
}