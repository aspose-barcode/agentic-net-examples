// Title: Process batch of TIFF barcodes and compute average confidence
// Description: Demonstrates generating sample Code128 barcodes as TIFF images, reading them, and calculating the average recognition confidence across the batch.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for performance tuning. Developers often need to process multiple images, adjust image properties, and evaluate recognition reliability, making this pattern useful for batch processing and quality assessment scenarios.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: barcode, code128, tiff, batch-processing, confidence, generation, recognition, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a set of Code128 barcodes saved as TIFF files,
/// reads them back using Aspose.BarCode, and computes the average confidence of
/// the recognition results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcode images (if missing),
    /// processes each TIFF file, extracts confidence values, and prints the average
    /// recognition confidence.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode TIFF images will be stored.
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            // Create the folder when it does not exist.
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Generate sample barcode images (only if they are not already present)
        // --------------------------------------------------------------------
        int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string fileName = $"barcode_{i}.tif";
            string filePath = Path.Combine(folderPath, fileName);
            if (!File.Exists(filePath))
            {
                // Create a new barcode generator for Code128 with a unique value.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
                {
                    // Set simple black on white colors for high contrast.
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Save the generated barcode as a TIFF image.
                    generator.Save(filePath, BarCodeImageFormat.Tiff);
                }
            }
        }

        // --------------------------------------------------------------
        // Read each TIFF image, decode barcodes, and collect confidence data
        // --------------------------------------------------------------
        List<int> confidenceValues = new List<int>();
        string[] tiffFiles = Directory.GetFiles(folderPath, "*.tif");
        foreach (string tiffFile in tiffFiles)
        {
            if (!File.Exists(tiffFile))
            {
                Console.WriteLine($"File not found: {tiffFile}");
                continue;
            }

            // Initialize a barcode reader for Code128 barcodes.
            using (var reader = new BarCodeReader(tiffFile, DecodeType.Code128))
            {
                // Apply a high‑performance quality preset to speed up processing.
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Iterate over all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // BarCodeConfidence enum values can be cast to int (0, 80, 100).
                    confidenceValues.Add((int)result.Confidence);
                }
            }
        }

        // ------------------------------
        // Compute and display the average
        // ------------------------------
        if (confidenceValues.Count > 0)
        {
            double averageConfidence = 0.0;
            foreach (int val in confidenceValues)
            {
                averageConfidence += val;
            }
            averageConfidence /= confidenceValues.Count;

            Console.WriteLine($"Processed {confidenceValues.Count} barcode results.");
            Console.WriteLine($"Average recognition confidence: {averageConfidence:F2}");
        }
        else
        {
            Console.WriteLine("No barcode results were found.");
        }
    }
}