// Title: Deconvolution Test on JPEG Barcodes
// Description: Demonstrates testing different deconvolution modes on heavily compressed JPEG images to determine the optimal quality threshold for reliable barcode scanning.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: barcode, deconvolution, jpeg, quality, aspose.barcode, aspose.drawing, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that evaluates deconvolution settings on JPEG images containing barcodes to find the best quality configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans each JPEG in the Images folder with various deconvolution modes and reports detection results.
    /// </summary>
    static void Main()
    {
        // Folder containing JPEG images with barcodes.
        string imagesFolder = "Images";

        // Verify that the folder exists.
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Retrieve all JPEG files in the folder.
        string[] jpegFiles = Directory.GetFiles(imagesFolder, "*.jpg");
        if (jpegFiles.Length == 0)
        {
            Console.WriteLine($"No JPEG files found in folder: {imagesFolder}");
            return;
        }

        // Define the deconvolution modes that will be tested.
        DeconvolutionMode[] deconvolutionModes = new[]
        {
            DeconvolutionMode.Fast,
            DeconvolutionMode.Normal,
            DeconvolutionMode.Slow
        };

        // Process each JPEG file individually.
        foreach (string filePath in jpegFiles)
        {
            // Ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

            // Test each deconvolution mode on the current image.
            foreach (DeconvolutionMode mode in deconvolutionModes)
            {
                // Create a barcode reader for the image, supporting all symbologies.
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Apply a high‑quality preset to improve detection on low‑quality images.
                    reader.QualitySettings = QualitySettings.HighQuality;

                    // Set the current deconvolution mode.
                    reader.QualitySettings.Deconvolution = mode;

                    // Perform barcode recognition.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output the results for the current mode.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"  Deconvolution: {mode} – No barcode detected.");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            // ReadingQuality is a double representing the confidence percentage.
                            double quality = result.ReadingQuality;
                            string codeText = result.CodeText ?? "<null>";
                            Console.WriteLine($"  Deconvolution: {mode} – Detected: {codeText}, ReadingQuality: {quality:F2}%");
                        }
                    }
                }
            }

            Console.WriteLine(); // Blank line between files.
        }

        // Indicate that processing has finished.
        Console.WriteLine("Processing completed.");
    }
}