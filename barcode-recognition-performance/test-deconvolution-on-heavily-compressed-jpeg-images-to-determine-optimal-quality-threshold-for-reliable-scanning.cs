// Title: Deconvolution Test on JPEG Barcodes
// Description: Generates Code128 barcodes saved as JPEG, then reads them using different deconvolution modes to evaluate scanning reliability on heavily compressed images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create barcode images, apply JPEG compression, and use the Deconvolution quality settings during recognition. It showcases the BarcodeGenerator, BarCodeReader, and DeconvolutionMode classes—common tools for developers who need to optimize barcode scanning under adverse image conditions.
// Prompt: Test deconvolution on heavily compressed JPEG images to determine optimal quality threshold for reliable scanning.
// Tags: code128, deconvolution, jpeg, barcode generation, barcode recognition, quality settings, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, JPEG compression, and deconvolution‑based recognition
/// to find the optimal quality threshold for reliable scanning of heavily compressed images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, saves them as JPEG,
    /// and reads them back using various deconvolution modes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Generate sample Code128 barcode images (saved as JPEG with default quality)
        // --------------------------------------------------------------------
        string[] barcodeTexts = { "CODE128-123", "CODE128-456", "CODE128-789" };
        for (int i = 0; i < barcodeTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{i + 1}.jpg");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeTexts[i]))
            {
                // Save the barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }

        // --------------------------------------------------------------------
        // Define deconvolution modes to be tested during recognition
        // --------------------------------------------------------------------
        DeconvolutionMode[] modes = { DeconvolutionMode.Fast, DeconvolutionMode.Normal, DeconvolutionMode.Slow };

        // --------------------------------------------------------------------
        // Locate all generated JPEG images for processing
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No JPEG images found for processing.");
            return;
        }

        // --------------------------------------------------------------------
        // Process each image with each deconvolution mode and output results
        // --------------------------------------------------------------------
        foreach (string imageFile in imageFiles)
        {
            Console.WriteLine($"Processing image: {Path.GetFileName(imageFile)}");
            foreach (DeconvolutionMode mode in modes)
            {
                using (BarCodeReader reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
                {
                    // Apply the current deconvolution mode to the reader's quality settings
                    reader.QualitySettings.Deconvolution = mode;

                    // Attempt to read barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    Console.WriteLine($"  Deconvolution: {mode}");
                    if (results.Length == 0)
                    {
                        Console.WriteLine("    No barcode detected.");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            // ReadingQuality indicates confidence percentage of the detection
                            double quality = result.ReadingQuality;
                            string text = string.IsNullOrEmpty(result.CodeText) ? "<empty>" : result.CodeText;
                            Console.WriteLine($"    CodeText: {text}");
                            Console.WriteLine($"    ReadingQuality: {quality:F2}%");
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}