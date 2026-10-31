// Title: Batch TIFF Barcode Generation and Recognition Accuracy Evaluation
// Description: Generates a set of Code128 barcodes saved as TIFF files, reads them back, and calculates the average recognition success rate.
// Category-Description: Demonstrates Aspose.BarCode's barcode generation and recognition workflow for image files. This example shows how to create barcode images using BarcodeGenerator, adjust barcode parameters, save as TIFF, and then use BarCodeReader with quality settings to decode them. Typical use cases include batch processing of scanned documents, quality testing of barcode readability, and automated verification pipelines. Developers often need to work with EncodeTypes, BarCodeImageFormat, DecodeType, and QualitySettings classes.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: code128, barcode generation, barcode recognition, tiff, batch processing, accuracy, aspose.barcode, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates processing a batch of TIFF barcode images and computing average recognition accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcode TIFFs, reads them, and reports recognition statistics.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        int sampleCount = 5;                     // Number of barcode images to generate
        List<string> imageFiles = new List<string>();

        // -------------------------------------------------
        // Generate sample barcode TIFF images
        // -------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "Sample" + i;
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.tif");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional parameter adjustments for visual appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Save the barcode as a TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            // Keep track of successfully created files
            if (File.Exists(filePath))
                imageFiles.Add(filePath);
        }

        int successes = 0;   // Counter for successful recognitions

        // -------------------------------------------------
        // Process each TIFF image and evaluate recognition
        // -------------------------------------------------
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
                continue;

            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Use a high-performance preset for speed; adjust as needed
                reader.QualitySettings = QualitySettings.HighPerformance;

                BarCodeResult[] results = reader.ReadBarCodes();

                // Count as success if at least one barcode is decoded with non‑empty text
                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    successes++;
                }
            }
        }

        // Calculate average recognition accuracy
        double averageAccuracy = imageFiles.Count > 0 ? (double)successes / imageFiles.Count : 0.0;
        Console.WriteLine($"Processed {imageFiles.Count} images.");
        Console.WriteLine($"Successful recognitions: {successes}");
        Console.WriteLine($"Average recognition accuracy: {averageAccuracy:P2}");

        // -------------------------------------------------
        // Cleanup temporary files and folder
        // -------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program exit
        }
    }
}