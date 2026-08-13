// Title: Batch barcode recognition with FNC stripping
// Description: Demonstrates how to generate multiple GS1-128 barcode images and then recognize them in a batch while stripping FNC symbols.
// Category-Description: This example belongs to the Aspose.BarCode batch processing and barcode recognition category. It showcases the use of BarcodeGenerator for image creation, BarCodeReader for decoding, and the StripFNC setting to remove function characters from GS1 barcodes. Developers often need to process large sets of barcode images and require clean data without control characters.
// Prompt: Create a batch process that reads multiple images with StripFNC true to strip FNC symbols.
// Tags: gs1code128, stripfnc, batch-processing, barcode-recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and recognition of GS1-128 barcodes with FNC characters stripped.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, then reads each image with StripFNC enabled.
    /// </summary>
    static void Main()
    {
        // Define folder for sample barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Sample code text containing FNC characters (GS1 format)
        string sampleCodeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate a few barcode images
        for (int i = 1; i <= 3; i++)
        {
            string imagePath = Path.Combine(folderPath, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleCodeText))
            {
                generator.Save(imagePath);
            }
        }

        // Retrieve all generated PNG files
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found to process.");
            return;
        }

        // Process each image, stripping FNC characters during recognition
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Enable stripping of FNC characters
                reader.BarcodeSettings.StripFNC = true;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in {Path.GetFileName(file)}.");
                    continue;
                }

                // Output each recognized barcode with FNC stripped
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | CodeText (FNC stripped): {result.CodeText}");
                }
            }
        }
    }
}