// Title: Process batch of TIFF barcodes with contrast variations and compute accuracy
// Description: Demonstrates generating barcode images, creating contrast‑inverted copies, and measuring recognition accuracy across the batch.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It shows how to use BarcodeGenerator to create barcodes, manipulate images with Aspose.Drawing, and employ BarCodeReader with QualitySettings to evaluate decoding performance. Typical use cases include testing scanner robustness, batch processing of scanned documents, and benchmarking recognition algorithms. Developers often need to generate test data, adjust image properties, and collect statistical results.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: barcode, tiff, contrast, batch-processing, recognition-accuracy, aspose.barcode, generation, recognition, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes, creating contrast‑modified TIFF images,
/// and evaluating recognition accuracy using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, modifies contrast,
    /// reads them, and reports average recognition accuracy.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type and text)
        var samples = new List<(BaseEncodeType Encode, string Text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345")
        };

        // Map each generated file path to its expected barcode text
        var expectedMap = new Dictionary<string, string>();

        // Generate barcodes and create contrast‑modified copies
        int index = 0;
        foreach (var (encode, text) in samples)
        {
            // Base file name and path for the original barcode image
            string baseFileName = $"barcode_{index}.tif";
            string baseFilePath = Path.Combine(tempFolder, baseFileName);

            // Generate barcode image and save as TIFF
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(baseFilePath, BarCodeImageFormat.Tiff);
            }

            // Record expected text for the original image
            expectedMap[baseFilePath] = text;

            // File name and path for the contrast‑inverted version
            string contrastFileName = $"barcode_{index}_contrast.tif";
            string contrastFilePath = Path.Combine(tempFolder, contrastFileName);

            // Load the original image, invert its colors, and save as a new TIFF
            using (var bitmap = new Bitmap(baseFilePath))
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color original = bitmap.GetPixel(x, y);
                        Color inverted = Color.FromArgb(255 - original.R, 255 - original.G, 255 - original.B);
                        bitmap.SetPixel(x, y, inverted);
                    }
                }
                bitmap.Save(contrastFilePath, ImageFormat.Tiff);
            }

            // Record expected text for the contrast‑modified image
            expectedMap[contrastFilePath] = text;

            index++;
        }

        // Process all generated TIFF files and evaluate recognition accuracy
        int totalFiles = expectedMap.Count;
        int successfulReads = 0;

        foreach (var kvp in expectedMap)
        {
            string filePath = kvp.Key;

            // Verify the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a reader that supports all barcode types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Apply high‑quality settings to improve decoding reliability
                reader.QualitySettings = QualitySettings.HighQuality;

                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Determine if a valid barcode was read
                bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                if (success)
                {
                    successfulReads++;
                }

                // Output the result for the current file
                Console.WriteLine($"File: {Path.GetFileName(filePath)} - Read: {(success ? "Success" : "Failure")}");
            }
        }

        // Calculate and display the average recognition accuracy
        double averageAccuracy = totalFiles > 0 ? (double)successfulReads / totalFiles * 100.0 : 0.0;
        Console.WriteLine($"Processed {totalFiles} TIFF images.");
        Console.WriteLine($"Average recognition accuracy: {averageAccuracy:F2}%");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors to avoid crashing the program
        }
    }
}