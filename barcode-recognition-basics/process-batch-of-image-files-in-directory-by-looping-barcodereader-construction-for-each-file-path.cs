// Title: Batch barcode reading from image files
// Description: Generates sample Code128 barcodes as PNG images, then reads each image to extract barcode data.
// Category-Description: This example demonstrates combined barcode generation and recognition using Aspose.BarCode. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, a common workflow for batch processing of scanned documents, inventory images, or automated data entry systems. Developers often need to loop through files, construct readers per image, and handle multiple symbologies efficiently.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: code128, batch-processing, png, barcodegenerator, barcodereader, decode, encode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a set of barcode images and then read them in a batch
/// using Aspose.BarCode's <see cref="BarcodeGenerator"/> and <see cref="BarCodeReader"/> classes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample barcode PNG files, then iterates over each file,
    /// constructs a <see cref="BarCodeReader"/> for it, and outputs the decoded information.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a folder to store generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Generate a few sample Code128 barcode images (self‑contained example)
        // --------------------------------------------------------------------
        for (int i = 1; i <= 5; i++)
        {
            string fileName = $"barcode{i}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // Retrieve all PNG files from the folder for processing
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            // Verify that the file still exists before attempting to read it
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // ----------------------------------------------------------------
            // Create a BarCodeReader for the current image and decode all supported types
            // ----------------------------------------------------------------
            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}